using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Configuration;

namespace mynb.Models
{
    static public class MySQL
    {
        static private MySqlConnection con;
        static public string error { get; private set; }
        static public string query { get; private set; }
        static MySQL()
        {   // строка подключения к БД
            // con = new MySqlConnection("server=localhost; UserID=root;Password=mysql;database=STORY;Charset=utf8");
            //создаем подключение
            try
            {
                error = "";
                query = "OPEN CONNECTION TO MYSQL";
                con = new MySqlConnection(
                    WebConfigurationManager.ConnectionStrings["conn"].ConnectionString);
                con.Open();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                con = null;
            }
        }
        static public DataTable Select(string myquery)
        {
            if (IsError()) return null;
            try
            {

                query = myquery;
                // подготовили таблицу для хранения извлеченных данных 
                DataTable table = new DataTable();
                // объект для выполнения запроса myquery по адресу соn
                MySqlCommand cmd = new MySqlCommand(myquery, con);
                // reader хранит экспликацию из БД по запросу myquere
                MySqlDataReader reader = cmd.ExecuteReader();
                // переформат в таблицу
                table.Load(reader);
                return table;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }
        static public bool IsError()
        {
            return error != "";
        }




    }
}