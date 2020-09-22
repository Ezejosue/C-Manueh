using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Guia7Investigacion
{
    public partial class Form1 : Form
    {

        private SqlConnection conn;
        private SqlDataAdapter da;
        private SqlDataReader dr;
        private string sCn;

        OleDbConnection cnn = new OleDbConnection();

        public Form1()
        {
            InitializeComponent();

            cnn.ConnectionString = @"PROVIDER=SQLOLEDB;Server=AEZEQ;Database=BD_AA200509;Uid=sa;Pwd=123456";

            Conexion cn = new Conexion();

            cn.conec();

            sCn = cn.cadena;
            conn = new SqlConnection(sCn);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tabla("SELECT * FROM Materia WHERE CodigoMateria = '" + txtCodM.Text + "'");
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnmostrar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tabla("SELECT * FROM Materia");
        }

        private DataTable  tabla (string sql)
        {
            conn.Open();

            da = new SqlDataAdapter(sql, conn);

            DataTable table = new DataTable();
            DataSet ds = new DataSet();
            da.SelectCommand = new SqlCommand(sql, conn);
            da.Fill(ds);
            table = ds.Tables[0];
            conn.Close();
            return table;
        }

        private void Eliminar(string sql1)
        {
            conn.Open();
            da = new SqlDataAdapter(sql1, conn);
            SqlParameter prm = new SqlParameter("CodigoMateria", SqlDbType.VarChar);
            prm.Value = txtCodM.Text;
            da.SelectCommand.Parameters.Add(prm);
            dr = da.SelectCommand.ExecuteReader();
            conn.Close();

            MessageBox.Show("Eliminado");
            dataGridView1.DataSource = tabla("SELECT * FROM Materia");

        }

        private void Insertar(string id, string nombre, string uv, string requisitos)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO Materia VALUES ('{0}', '{1}', {2}, '{3}')", new string[] {id, nombre, uv, requisitos }), conn);
            int filas = cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Ingresado");
            dataGridView1.DataSource = tabla("SELECT * FROM Materia");
        }

        private void Actualizar(string id, string nombre, string uv, string requisitos)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(string.Format("UPDATE Materia SET NombreMateria = '{0}', UV = {1}, Prerequisitos = '{2}' WHERE CodigoMateria = '{3}'", 
            new string[] {nombre, uv, requisitos, id}), conn);

            int filas = cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Actualizado");
            dataGridView1.DataSource = tabla("SELECT * FROM Materia");
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtCodM.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtNombreM.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtUV.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtPre.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }


        private void Clear()
        {
            txtCodM.Text = "";
            txtNombreM.Text = "";
            txtUV.Text = "";
            txtPre.Text = "";
            dataGridView1.DataSource = "";
        }

        private void btnactualizar_Click(object sender, EventArgs e)
        {
            Actualizar(txtCodM.Text, txtNombreM.Text, txtUV.Text, txtPre.Text);
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            string eliminar1 = "DELETE Materia WHERE CodigoMateria = '"+txtCodM.Text+"'";
            Eliminar(eliminar1); 
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            Insertar(txtCodM.Text, txtNombreM.Text, txtUV.Text, txtPre.Text);
        }

        private void btnalumnos_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
            this.Hide();
        }

        private void txtNombreM_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtUV_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
