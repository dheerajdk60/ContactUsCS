using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ContactUs.Models;
using MySql.Data.MySqlClient;

namespace ContactUs.Controllers
{
    public class AdminController : Controller
    {
        // GET
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Check()
        {
            Console.WriteLine("Add method called");
            string url = @"server=localhost;userid=root;password=password;database=contactus";
            var connection = new MySqlConnection(url);
            connection.Open();
            string n = Request.Params["name"];
            string p = Request.Params["pass"];
            string query = "select * from admins where nname='"+n +"' and password='"+ p+"'";
            MySqlCommand command = new MySqlCommand(query,connection);
            MySqlDataReader reader=command.ExecuteReader();
            
            bool ValidUser = false;
            while (reader.Read())
            {
                ValidUser = true;
            }
            connection.Close();
            if (ValidUser)
            {
                connection.Open();
                query = "select * from feedback";
                command = new MySqlCommand(query,connection);
                reader=command.ExecuteReader();
                List<Message> messages = new List<Message>();
                while (reader.Read())
                {
                    Message message = new Message();
                    message.Id = Convert.ToInt32(reader[0]);
                    message.name = Convert.ToString(reader[1]);
                    message.email = Convert.ToString(reader[2]);
                    message.message = Convert.ToString(reader[3]);
                    message.status = Convert.ToBoolean(reader[4]);
                    messages.Add(message);
                }
                connection.Close();
                return View("Dashboard",messages);
            }
            else
            {
                connection.Close();
                return RedirectToAction("Index","Home");
            }
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Status( int id )
        {
            //int messageId = Convert.ToInt32(Request.Params["messageId"]);
            Console.WriteLine("status is run"+id);
            string url = @"server=localhost;userid=root;password=password;database=contactus";
            var connection = new MySqlConnection(url);
            connection.Open();
            string query = "select status from feedback where id="+id;
            MySqlCommand command = new MySqlCommand(query,connection);
            MySqlDataReader reader=command.ExecuteReader();
            bool currentStatus = false;
            while (reader.Read())
            {
                currentStatus=Convert.ToBoolean(reader[0]);
            }
            Console.WriteLine("currentstatus is "+currentStatus);
            connection.Close();
            currentStatus = !currentStatus;
            query = "update  feedback set status='"+currentStatus+"' where id="+id;
            connection.Open();
             command = new MySqlCommand(query,connection);
             command.ExecuteReader();
             connection.Close();
             query = "select * from feedback";
             connection.Open();
             command = new MySqlCommand(query,connection);
             reader=command.ExecuteReader();
            List<Message> messages = new List<Message>();
            while (reader.Read())
            {
                Message message = new Message();
                message.Id = Convert.ToInt32(reader[0]);
                message.name = Convert.ToString(reader[1]);
                message.email = Convert.ToString(reader[2]);
                message.message = Convert.ToString(reader[3]);
                message.status = Convert.ToBoolean(reader[4]);
                messages.Add(message);
            }
            connection.Close();
            return View("Dashboard",messages);
        }
        
    }
}