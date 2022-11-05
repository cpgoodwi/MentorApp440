using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using MentorApp440.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
namespace MentorApp440.Helpers;

public class SqlConnection
{

    private static MySqlConnection connection;
    private static MySqlCommand cmd = null;
    private static DataTable dt;
    private static MySqlDataAdapter da;
    private static MySqlDataReader dr;


    // This class builds a string connection and uses it to connect to the database.
    // It returns a string that tells you if the connect is sucessful and send it to the controller
    public static string EstablishConnection()
    {
        string ConnectionResult;
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        builder.Server = "107.180.1.16";
        builder.UserID = "440fall20224";
        builder.Password = "440fall20224";
        builder.Database = "440fall20224";
        builder.SslMode = MySqlSslMode.None;
        connection = new MySqlConnection(builder.ToString());
        connection.Open();
        if (connection.State == ConnectionState.Open)
        {
            ConnectionResult = " Database Connection Successful";
        }
        else
        {
            ConnectionResult = "Connection Failed";
        }

        return ConnectionResult;
                
    }

    public static List<User> RunUserQuery()
    {
        Models.User userViewModel = new Models.User();
        List<User> userViewModelList = new List<User>();

        return userViewModelList;

    }
}