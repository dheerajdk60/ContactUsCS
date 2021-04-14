using System;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ContactUs.Controllers
{
    public class FeedbackController : Controller
    {
        // GET
        public ActionResult Add()
        {
            Console.WriteLine("Add method called");
            string url = @"server=localhost;userid=root;password=password;database=contactus";
            var connection = new MySqlConnection(url);
            connection.Open();
            string n = Request.Params["name"];
            string e = Request.Params["email"];
            string m = Request.Params["message"];
            string query = "insert into feedback (nname,email,message) values('"+n+"','"+e+"','"+m+"')";
            MySqlCommand command = new MySqlCommand(query,connection);
            command.ExecuteReader();
            Console.WriteLine("inserted");
            connection.Close();
            return View();
            // return View();
        }
    }
}