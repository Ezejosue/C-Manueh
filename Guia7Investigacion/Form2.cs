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
    public partial class Form2 : Form
    {

        private SqlConnection conn;
        private SqlDataAdapter da;
        private SqlDataReader dr;
        private string sCn;

        OleDbConnection cnn = new OleDbConnection();

        public Form2()
        {
            InitializeComponent();

            cnn.ConnectionString = @"PROVIDER=SQLOLEDB;Server=AEZEQ;Database=BD_AA200509;Uid=sa;Pwd=123456";

            Conexion cn = new Conexion();

            cn.conec();

            sCn = cn.cadena;
            conn = new SqlConnection(sCn);
        }

        private void btnmaterias_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Adios :3");
            Application.Exit();
        }



        private DataTable tabla(string sql)
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
            SqlParameter prm = new SqlParameter("CodigoAlumno", SqlDbType.VarChar);
            prm.Value = txtCodA.Text;
            da.SelectCommand.Parameters.Add(prm);
            dr = da.SelectCommand.ExecuteReader();
            conn.Close();

            MessageBox.Show("Eliminado");
            dataGridView1.DataSource = tabla("SELECT * FROM Alumno");

        }


        private void Insertar(string id, string nombre, string segundonombre, string apellido, string segundoapellido, string edad, string direccion)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO Alumno VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}')", 
            new string[] { id, nombre, segundonombre, apellido, segundoapellido, edad, direccion}), conn);
            int filas = cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Ingresado");
            dataGridView1.DataSource = tabla("SELECT * FROM Alumno");
        }

        private void Actualizar(string id, string nombre, string segundonombre, string apellido, string segundoapellido, string edad, string direccion)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(string.Format("UPDATE Alumno SET PrimerNombre = '{0}', SegundoNombre = '{1}', PrimerApellido = '{2}', SegundoApellido = '{3}', " +
                "Edad = {4}, Direccion = '{5}' WHERE CodigoAlumno = '{6}'",
            new string[] { nombre, segundonombre, apellido, segundoapellido, edad, direccion, id }), conn);

            int filas = cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Actualizado");
            dataGridView1.DataSource = tabla("SELECT * FROM Alumno");
        }



        private void btnmostrar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tabla("SELECT * FROM Alumno");
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            Insertar(txtCodA.Text, txtNombreA.Text, txtNombre1.Text, txtApellidoA.Text, txtApellido1.Text, txtEdad.Text, txtDireccion.Text);
        }

        private void txtCodA_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreA_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtCodA.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtNombreA.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtApellidoA.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtNombre1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtApellido1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtEdad.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtDireccion.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

        }

        private void btnactualizar_Click(object sender, EventArgs e)
        {
            Actualizar(txtCodA.Text, txtNombreA.Text, txtNombre1.Text, txtApellidoA.Text, txtApellido1.Text, txtEdad.Text, txtDireccion.Text);

        }

        private void Clear()
        {
            txtCodA.Text = "";
            txtNombreA.Text = "";
            txtNombre1.Text = "";
            txtApellidoA.Text = "";
            txtApellido1.Text = "";
            txtEdad.Text = "";
            txtDireccion.Text = "";

            dataGridView1.DataSource = "";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tabla("SELECT * FROM Alumno WHERE CodigoAlumno = '" +txtCodA.Text + "'");

        }

        private void btneliminar_Click(object sender, EventArgs e)
        {

            string eliminar1 = "DELETE Alumno WHERE CodigoAlumno = '" + txtCodA.Text + "'";
            Eliminar(eliminar1);

        }
    }
}
