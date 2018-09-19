using System;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Azure.Documents.Client;

namespace Ingredients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        string cs = ConfigurationManager.ConnectionStrings["SQL_CONNECTION"].ConnectionString;

        // GET api/ingredients
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/ingredients/get
        [HttpGet("get")]
        public List<SmartChef_API.Models.Ingredients> Get(string method)
        {
            List<SmartChef_API.Models.Ingredients> ingredients = new List<SmartChef_API.Models.Ingredients>();

            System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(cs);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                CommandText = "EXEC [SmartChef].[GetIngredients]",
                Connection = sqlConn
            };

            try
            {
                sqlConn.Open();
                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SmartChef_API.Models.Ingredients newitem = new SmartChef_API.Models.Ingredients();
                    newitem.Name = (string)reader["Name"];
                    newitem.AllergicType = (string)reader["AllergicType"];
                    newitem.Status = (string)reader["Status"];
                    ingredients.Add(newitem);
                }

                reader.Close();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection ! ");
                Console.WriteLine(ex.Message);
            }

            return ingredients;
        }

        // POST api/ingredients/update
        [HttpGet("update")]
        public ActionResult<string> Post(string name, string status)
        {
            string result = "fail";

            //UNIT TEST INPUTS
            name = "Salmon";
            status = "No";

            try
            {
                System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(cs);
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SmartChef.UpdateIngredient", sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 255).Value = name;
                cmd.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar, 255).Value = status;

                sqlConn.Open();
                cmd.ExecuteNonQuery();
                sqlConn.Close();

                result = "success";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection ! ");
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        // DELETE api/ingredients/delete
        [HttpGet("delete")]
        public ActionResult<string> Delete(string name)
        {
            string result = "fail";

            //UNIT TEST INPUTS
            name = "Bacon";
            try
            {
                System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(cs);
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SmartChef.DeleteIngredient", sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 255).Value = name;  

                sqlConn.Open();
                cmd.ExecuteNonQuery();
                sqlConn.Close();

                result = "success";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection ! ");
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        // INSERT api/ingredients/insert
        [HttpGet("insert")]
        public ActionResult<string> Insert(string name, string allergictype, string status)
        {
            string result = "fail";

            //UNIT TEST INPUTS
            name = "Bacon";
            allergictype = "Vegan";
            status = "Yes";

            try
            {
                System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(cs);
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SmartChef.InsertIngredient", sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 255).Value = name;
                cmd.Parameters.Add("@AllergicType", System.Data.SqlDbType.NVarChar, 255).Value = allergictype;
                cmd.Parameters.Add("@Status", System.Data.SqlDbType.NVarChar, 255).Value = status;

                sqlConn.Open();
                cmd.ExecuteNonQuery();
                sqlConn.Close();

                result = "success";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection ! ");
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
