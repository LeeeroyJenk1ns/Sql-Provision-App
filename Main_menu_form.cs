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

namespace SQL_Provision
{
    public partial class Main_menu_form : Form
    {
        SqlConnection sqlConnection;

        async void SelectData()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Products]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id"] + "."));
                    listBox2.Items.Add(Convert.ToString(sqlReader["title"]));
                    listBox3.Items.Add(Convert.ToString(sqlReader["price"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
        }

        public Main_menu_form()
        {
            InitializeComponent();
        }

        private async void Main_menu_form_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Desktop\SQL Provision\SQL_Provision\Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();


            SelectData();
        }

        private void Main_menu_form_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (label9.Visible)
            {
                label9.Visible = false;
            }

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Products] (title, price)VALUES (@title, @price)", sqlConnection);
                command.Parameters.AddWithValue("title", textBox1.Text);
                command.Parameters.AddWithValue("price", textBox2.Text);

                try
                {
                    await command.ExecuteNonQueryAsync();
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                textBox1.Clear();
                textBox2.Clear();

                SelectData();

            } else
            {
                label9.Visible = true;
                label9.Text = "Заполните все поля";
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label10.Visible)
            {
                label10.Visible = false;
            }

            if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text)
                && !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text)
                && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [Products] SET [title]=@title, [price]=@price WHERE [id]=@id", sqlConnection);

                command.Parameters.AddWithValue("id", textBox5.Text);
                command.Parameters.AddWithValue("title", textBox4.Text);
                command.Parameters.AddWithValue("price", textBox3.Text);

                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();

                SelectData();

            }
            else
            {
                label10.Visible = true;
                label10.Text = "Заполните все поля";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label11.Visible)
            {
                label11.Visible = false;
            }

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Products] WHERE [id]=@id", sqlConnection);

                command.Parameters.AddWithValue("id", textBox6.Text);

                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                textBox6.Clear();

                SelectData();

            }
            else
            {
                label11.Visible = true;
                label11.Text = "Укажите id";
            }
        }
    }
}
