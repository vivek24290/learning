using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace WeatherForecast.Services
{
    public class WeatherForecastService
    {
        protected string _connectionString = "";

        public WeatherForecastService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public Object GetEmployeeDetails(int badge)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM employee WHERE Id = " + badge;
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                var columns = Enumerable.Range(0,rdr.FieldCount).Select(rdr.GetName).ToList();
                List<IDictionary<string,Object>> obj = new List<IDictionary<string, object>>();
                if(rdr.HasRows)
                {
                    while(rdr.Read())
                    {
                        IDictionary<string,Object> row = new Dictionary<string,Object>();
                        foreach(string col in columns)
                        {
                            row.Add(col, rdr[col].ToString());
                        }
                        obj.Add(row);
                    }
                    rdr.Close();
                }
                connection.Close();
                return JsonSerializer.Serialize(obj);
            }
        }

        public void AddEmployeeDetail(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO employee (Name) VALUES (\""+name+"\")";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                // var columns = Enumerable.Range(0,rdr.FieldCount).Select(rdr.GetName).ToList();
                // List<IDictionary<string,Object>> obj = new List<IDictionary<string, object>>();
                // if(rdr.HasRows)
                // {
                //     while(rdr.Read())
                //     {
                //         IDictionary<string,Object> row = new Dictionary<string,Object>();
                //         foreach(string col in columns)
                //         {
                //             row.Add(col, rdr[col].ToString());
                //         }
                //         obj.Add(row);
                //     }
                    rdr.Close();
                // }
                connection.Close();
                // return sql;
                // return JsonSerializer.Serialize(obj);
            }
        }

       
    }
}