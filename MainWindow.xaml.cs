using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SqlInjectionWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string connectionString = "Data Source=DESKTOP-EDEVO0N\\SQLEXPRESS;Initial Catalog=SqlInjection;Integrated Security=True";
        private static SqlConnection connection = new SqlConnection(connectionString);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Server connect = new Server();
            connect.ConnectDataBase();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Server select = new Server();
            select.SelectFromDataBase();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Server insert = new Server();
            string password = textbox2.Text;
            string mail = textbox1.Text;
            if (!insert.CheckIfExist(mail))
            {
                insert.InsertToDataBase(mail, password);
            }
            else
                MessageBox.Show("This Mail Is Already Exist in db");
       

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
         
        Server update = new Server();

            string mail = textbox1.Text;
            string password = textbox2.Text;
            string new_mail = newMail.Text;
            if (update.CheckIfExist(mail))
            {
                update.UpdateData(mail,new_mail, password);
            }
            else
                MessageBox.Show("This Mail Is Already Exist in db");

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Server delete = new Server();
            string mail = DeleteTextBox.Text;
            if (delete.CheckIfExist(mail))
            {
                delete.DeleteDataFromDB(mail);
                MessageBox.Show("This User Details has been deleted");
                DeleteTextBox.Clear();
            }
            else
                MessageBox.Show("Not Valid Mail");
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

            //connection.Open();
            //DataSet dataSet = new DataSet();
            //using (SqlCommand cmd = new SqlCommand("sp_Select_users", connection))
            //{
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //    MessageBox.Show("your here");

            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.Fill(dataSet);
            //    dataGrid1.DataContext = dataSet;
            DataTable dt = new DataTable();
            string mail, password;
            connection.Open();
            SqlCommand myCmd = new SqlCommand("sp_Select_users", connection);
            SqlDataAdapter da = new SqlDataAdapter(myCmd);
            SqlDataReader reader = myCmd.ExecuteReader();
            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    mail = reader.GetString(reader.GetOrdinal("Mail"));
                    password = reader.GetString(1);
                    da.Fill(dt);
                    MessageBox.Show(mail, password);

                }

            }

            myCmd.CommandType = CommandType.StoredProcedure;
            myCmd.ExecuteNonQuery();
           
            da.Fill(dt);
            
            //dataGrid1.DataSource = ds.Tables["Student"].DefaultView;
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    }

