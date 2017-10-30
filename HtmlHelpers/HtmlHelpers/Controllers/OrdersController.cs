using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using HtmlHelpers.Models;


namespace HtmlHelpers.Controllers
{
    public class OrdersController : Controller
    {
        string connectionString = "DataSource=.\\SQLEXPRESS; AttachDbFilename =SampleDB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
        public OrdersController()
        {
        }
        // GET: Orders
        public ActionResult Index()
        {
            var s = DBConnectionHelper.FillDataTable("Select * from orders");
            var viewmodel =s.ConvertDataTable<OrderSummaryViewModel>();

            return View("OrderSummary",viewmodel);
        }
    }

    public class Extention
    {
        //public List<OrderSummaryViewModel> ToViewModel(DataTable dt)
        //{
        //    var s= dt.
        //}

    }
}
   public static class DBConnectionHelper
    {

        public static List<T> ConvertDataTable<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    public static void CreateCommand(string queryString,
    string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static DataTable FillDataTable(string query)
        {
            string cons =@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+AppDomain.CurrentDomain.BaseDirectory+@"App_Data\SampleDB.mdf;Integrated Security=True";
            SqlConnection sqlConn = new SqlConnection(cons);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlConn.Close();
            return dt;
        }
    }

    
