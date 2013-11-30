using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class UserDal
    {
        /// <summary>
        /// Creates a new User and addes it to the UserInfo Table
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="lname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Int64 CreateNewUser(String fname, String lname, String email, String password)
        {
            // Data Source=.\SQLEXPRESS;AttachDbFilename=C:\LuisJr\USDSenior1\Software_Engineering\Test_Sites\App_Data\Database.mdf;Integrated Security=True;User Instance=True
            String connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            
            // This will be -1 if user was not added or > 0 if the user was added
            Int64 userID = 0;
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {

                String insertCommand = "INSERT INTO UserInfo (first_name, last_name, email, password) VALUES (" +
                                            "'" + fname + "', " +
                                            "'" + lname + "', " +
                                            "'" + email + "', " +
                                            "'" + password + "')";

                SqlCommand command = new SqlCommand(insertCommand, sqlConn);

                sqlConn.Open();
                // Execute scalar returns the element at the 1st column of the data inserted( which should be the ID)
                userID = (Int64) command.ExecuteScalar();
                sqlConn.Close();
            }

            return userID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static User GetUserByEmail(String email)
        {
            //Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\acer\Documents\GitHub\software-engineering\group\Sample_Web_Site\App_Data\Database.mdf;Integrated Security=True;User Instance=True
            String connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

            User newUser;
            // This will be -1 if user was not added or > 0 if the user was added
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {

                String selectCommand = "SELECT * FROM UserInfo WHERE email = '" + email + "'";

                SqlCommand command = new SqlCommand(selectCommand, sqlConn);

                sqlConn.Open();
                // Execute scalar returns the element at the 1st column of the data inserted( which should be the ID)
                SqlDataReader dr = command.ExecuteReader();
                dr.GetSqlInt64(0);
                newUser = new User();
                //newUser.ID = dr.getSQLI();
                sqlConn.Close();
            }

            return newUser;
        }
    }
    

}

