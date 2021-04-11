using System;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace netcoreWebapi.Controllers
{
    [Serializable]
    public class testcl
    {
        public string _cmd = "calc.exe";
        public testcl(string cmd)
        {
            if (cmd != "calc.exe")
                Console.WriteLine("Invalid command");
            else
                _cmd = cmd;
        }
        public testcl()
        {
            if (_cmd != "calc.exe")
                Console.WriteLine("Invalid command");
        }

        public void Run()
        {
            Process.Start(_cmd);
        }
    }


    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        // GET api/customers/4343
        [HttpGet("{id:int}")]
        public JsonResult Get(int id)
        {
            XmlSerializer ser_xml;
            var customers = DataBuilder.CreateCustomers();
            foreach(var c in customers)
            {
                if (c.ClientId == id)
                {
                    var custList = new List<Customer> { c };
                    return Json(custList);
                }
            }
            return Json("Not found");
        }

        [HttpGet("Add")]
        public JsonResult Get(string sql)
        {
            const string connection = @"Data Source=MyData;Initial Catalog=Product;Trusted_Connection=true";
            var conn = new SqlConnection(connection);
            string query = "INSERT INTO customers " + sql;
            var command = new SqlCommand(query, conn);
            int result = command.ExecuteNonQuery();
            return Json(string.Format("Result: {0}", result));
        }

        // POST api/customers
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/customer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
