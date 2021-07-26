using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PriceTable.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace PriceTable.Controllers
{
    public class HomeController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<TimeP> times = new List<TimeP>();


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString = PriceTable.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            FetchData();
            return View(times);
        }
        private void FetchData()
        {
            if (times.Count > 0)
            {
                times.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [Time],[Price] FROM [Elering].[dbo].[Data]";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    times.Add(new TimeP() { Time = dr["Time"].ToString()
                    , Price = dr["Price"].ToString()});
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
