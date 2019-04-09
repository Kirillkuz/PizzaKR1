using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaKR1
{
    public partial class Form1 : Form
    {
        string PizzaAddress1="";
        string Pizza1 = "";
        string PizzaAddress2;
        string PizzaDelivery2;
        SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
            
        }
        public async void repeat()
        {
            
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();

            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\God\source\repos\PizzaKR1\PizzaKR1\PizzaDB.mdf; Integrated Security = True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command22 = new SqlCommand("SELECT * FROM [PizzaAddress_Pizza]", sqlConnection);
            SqlCommand command11 = new SqlCommand("SELECT * FROM [PizzaAddress_PizzaDelivery]", sqlConnection);
            SqlCommand command33 = new SqlCommand("SELECT PizzaAddress_PizzaDelivery.PizzaAddress AS PizzaAddress, PizzaAddress_Pizza.Pizza AS Pizza, PizzaAddress_PizzaDelivery.PizzaDelivery AS PizzaDelivery FROM [PizzaAddress_PizzaDelivery],[PizzaAddress_Pizza] WHERE PizzaAddress_PizzaDelivery.PizzaAddress=PizzaAddress_Pizza.PizzaAddress", sqlConnection);
            sqlReader = await command22.ExecuteReaderAsync();
            
            while (await sqlReader.ReadAsync())
            {
                dataGridView1.Rows.Add(Convert.ToString(sqlReader["PizzaAddress"]), Convert.ToString(sqlReader["Pizza"]));
               
            }
            sqlReader.Close();
            sqlReader = await command11.ExecuteReaderAsync();
            while (await sqlReader.ReadAsync())
            {
                dataGridView2.Rows.Add(Convert.ToString(sqlReader["PizzaAddress"]), Convert.ToString(sqlReader["PizzaDelivery"]));
            }
            sqlReader.Close();
            sqlReader = await command33.ExecuteReaderAsync();
            while (await sqlReader.ReadAsync())
            {
                dataGridView3.Rows.Add(Convert.ToString(sqlReader["PizzaAddress"]), 
                    Convert.ToString(sqlReader["Pizza"]), 
                    Convert.ToString(sqlReader["PizzaDelivery"]));
            }
            sqlReader.Close();

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\God\source\repos\PizzaKR1\PizzaKR1\PizzaDB.mdf; Integrated Security = True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command2 = new SqlCommand("SELECT * FROM [PizzaAddress_Pizza]", sqlConnection);
            SqlCommand command1 = new SqlCommand("SELECT * FROM [PizzaAddress_PizzaDelivery]", sqlConnection);
            try
            {
                
                sqlReader = await command2.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    //listBox1.Items.Add(Convert.ToString(sqlReader["PizzaAddress"]) + "   "+ Convert.ToString(sqlReader["Pizza"]));
                    dataGridView1.Rows.Add(Convert.ToString(sqlReader["PizzaAddress"]), Convert.ToString(sqlReader["Pizza"]));
                   
                }
                sqlReader.Close();
                sqlReader = await command1.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    //listBox2.Items.Add(Convert.ToString(sqlReader["PizzaAddress"]) + "   " + Convert.ToString(sqlReader["PizzaDelivery"]));
                    dataGridView2.Rows.Add(Convert.ToString(sqlReader["PizzaAddress"]), Convert.ToString(sqlReader["PizzaDelivery"]));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [PizzaAddress_Pizza](PizzaAddress,Pizza)VALUES(@PizzaAddress,@Pizza)",sqlConnection);
            command.Parameters.AddWithValue("PizzaAddress", textBox1.Text);
            command.Parameters.AddWithValue("Pizza", textBox2.Text);
            await command.ExecuteNonQueryAsync();
            repeat();
            
        }

       

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label1.Text = "ЖОПА";
            int RowNum = dataGridView1.CurrentCell.RowIndex;
            label1.Text += Convert.ToString(RowNum);
            textBox1.Text=Convert.ToString(dataGridView1.Rows[RowNum].Cells[0].Value);
            textBox2.Text = Convert.ToString(dataGridView1.Rows[RowNum].Cells[1].Value);
            PizzaAddress1 = Convert.ToString(dataGridView1.Rows[RowNum].Cells[0].Value);
            Pizza1 = Convert.ToString(dataGridView1.Rows[RowNum].Cells[1].Value);
            label1.Text += Pizza1;

        }

        public async void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE [PizzaAddress_Pizza] SET [PizzaAddress]=@PizzaAddress, [Pizza]=@Pizza WHERE [PizzaAddress]=@PizzaAddressOld AND [Pizza]=@PizzaOld", sqlConnection);
            command.Parameters.AddWithValue("PizzaAddress", textBox1.Text);
            command.Parameters.AddWithValue("Pizza", textBox2.Text);
            command.Parameters.AddWithValue("PizzaAddressOld", PizzaAddress1);
            command.Parameters.AddWithValue("PizzaOld", Pizza1);
            await command.ExecuteNonQueryAsync();
            repeat();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("DELETE FROM [PizzaAddress_Pizza] WHERE [PizzaAddress]=@PizzaAddressOld AND [Pizza]=@PizzaOld", sqlConnection);
            command.Parameters.AddWithValue("PizzaAddressOld", PizzaAddress1);
            command.Parameters.AddWithValue("PizzaOld", Pizza1);
            await command.ExecuteNonQueryAsync();
            repeat();
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [PizzaAddress_PizzaDelivery](PizzaAddress,PizzaDelivery)VALUES(@PizzaAddress,@PizzaDelivery)", sqlConnection);
            command.Parameters.AddWithValue("PizzaAddress", textBox4.Text);
            command.Parameters.AddWithValue("PizzaDelivery", textBox3.Text);
            await command.ExecuteNonQueryAsync();
            repeat();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE [PizzaAddress_PizzaDelivery] SET [PizzaAddress]=@PizzaAddress, [PizzaDelivery]=@PizzaDelivery WHERE [PizzaAddress]=@PizzaAddressOld AND [PizzaDelivery]=@PizzaDeliveryOld", sqlConnection);
            command.Parameters.AddWithValue("PizzaAddress", textBox4.Text);
            command.Parameters.AddWithValue("PizzaDelivery", textBox3.Text);
            command.Parameters.AddWithValue("PizzaAddressOld", PizzaAddress2);
            command.Parameters.AddWithValue("PizzaDeliveryOld", PizzaDelivery2);
            await command.ExecuteNonQueryAsync();
            repeat();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("DELETE FROM [PizzaAddress_PizzaDelivery] WHERE [PizzaAddress]=@PizzaAddressOld AND [PizzaDelivery]=@PizzaDeliveryOld", sqlConnection);
            command.Parameters.AddWithValue("PizzaAddressOld", PizzaAddress2);
            command.Parameters.AddWithValue("PizzaDeliveryOld", PizzaDelivery2);
            await command.ExecuteNonQueryAsync();
            repeat();
        }

        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label2.Text = "ЖОПА";
            int RowNum1 = dataGridView2.CurrentCell.RowIndex;
            label2.Text += Convert.ToString(RowNum1);
            textBox4.Text = Convert.ToString(dataGridView2.Rows[RowNum1].Cells[0].Value);
            textBox3.Text = Convert.ToString(dataGridView2.Rows[RowNum1].Cells[1].Value);
            PizzaAddress2 = Convert.ToString(dataGridView2.Rows[RowNum1].Cells[0].Value);
            PizzaDelivery2 = Convert.ToString(dataGridView2.Rows[RowNum1].Cells[1].Value);
            label2.Text += Pizza1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            repeat();
        }
    }
}
