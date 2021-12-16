using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SqlInjectionWPF
{
    class Server
    {
       
       
        public static string ConfigurationManager { get; private set; }

        public static string connectionString = "Data Source=DESKTOP-EDEVO0N\\SQLEXPRESS;Initial Catalog=SqlInjection;Integrated Security=True";
        public static SqlConnection connection = new SqlConnection(connectionString);


        public bool CheckIfExist(string mail)
        {

            connection.Open();
            string query = "SELECT count(*) FROM MEMBERS WHERE [Mail] = '" + mail + "'";
            bool exist = false;
            SqlCommand checkMail = new SqlCommand(query, connection);
            exist = (int)checkMail.ExecuteScalar() > 0;
            if (exist)
            {
                connection.Close();
                return true;
               
            }
            connection.Close();
            return false;

        }

        public void ConnectDataBase()
        {


            connection.Open();
            MessageBox.Show("ok, your connect to DataBase...");
        }
        public void SelectFromDataBase()
        {
            string query = "Select * from MEMBERS";
            SqlCommand cmd = new SqlCommand(query, connection);
            string mail, password;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    mail = reader.GetString(reader.GetOrdinal("Mail"));
                    password = reader.GetString(1);
                    
                    MessageBox.Show(mail, password);

                }

            }
            connection.Close();
        }

        public void InsertToDataBase(string mail, string pass)
        {
            connection.Open();
            string query = "INSERT INTO MEMBERS (Mail, Password) VALUES (@Mail, @Password)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Mail", System.Data.SqlDbType.NVarChar);
            command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar);
            command.Parameters["@Mail"].Value = mail;
            command.Parameters["@Password"].Value = pass;

            int RowsAffected = command.ExecuteNonQuery();
            MessageBox.Show("Insert Succes" + RowsAffected);

            connection.Close();
        }
        public void UpdateData(string mail,string newmail, string pass)
        {
            connection.Open();
            using (SqlCommand cmd =
                new SqlCommand("UPDATE MEMBERS SET Mail=@Mail, Password=@Password" +
                    " WHERE Mail= '" + mail + "'", connection))
            {
                
                //add whatever parameters you required to update here
                cmd.Parameters.Add("@Mail", System.Data.SqlDbType.NVarChar);
                cmd.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar);
                cmd.Parameters["@Mail"].Value = newmail;
                cmd.Parameters["@Password"].Value = pass;

                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows+"-rows affected");
                
            }
        }
        public void DeleteDataFromDB(string mail)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM MEMBERS " +
                     " WHERE Mail= '" + mail + "'", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

      
    }
}

