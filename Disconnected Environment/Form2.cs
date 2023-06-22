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

namespace Disconnected_Environment
{
    public partial class Form2 : Form
    {
        private string connectionstring = "data source = OKTAVIKO\\YEELZY;database=DisconnectedEnvironment;MultipleActiveResultSets=True;User ID = sa; password = 123";
        private SqlConnection koneksi;
        public Form2()
        {
            InitializeComponent();
            koneksi = new SqlConnection(connectionstring);
            refreshform();
        }
        private void refreshform()
        {
            nmp.Text = "";
            nmp.Enabled = false;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select id_prodi, nama_prodi from dbo.prodi";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nmp.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string nmProdi = nmp.Text;
            if (nmProdi == "")
            {
                MessageBox.Show("Masukkan Nama prodi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo. prodi (id_prodi)" + "values(@id_prodi)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("id_prodi", nmProdi));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information );
                dataGridView();   
                refreshform();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnOpen.Enabled = false;
        }
        private void Form2_LoadFormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 hu = new Form1();
            hu.Show();
            this.Hide();
        }
    }
}
