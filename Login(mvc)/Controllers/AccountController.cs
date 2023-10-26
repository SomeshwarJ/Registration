using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login_mvc_.Models;
using System.Data.SqlClient;
namespace Login_mvc_.Controllers
{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        [HttpGet]
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "data source = ANANTHIJOTHI; database = c#; integrated security = SSPI;";
        }
        [HttpPost]
        public ActionResult Verify(Account acc) 
        {
            Account a = new Account
            {
                Name = acc.Name,
                Email = acc.Email,
                Phone = acc.Phone,
            };
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "INSERT INTO Login (name, email, phone) VALUES (@Value1, @Value2, @Value3)";
            com.Parameters.AddWithValue("@Value1", acc.Name);
            com.Parameters.AddWithValue("@Value2", acc.Email);
            com.Parameters.AddWithValue("@Value3", acc.Phone);
            try
            {
                int rowsAffected = com.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data inserted successfully.");
                    return View("Create", a);
                }
                else
                {
                    Console.WriteLine("Data insertion failed.");
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return View("Error");
        }
    }
}