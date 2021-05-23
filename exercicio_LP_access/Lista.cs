using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace exercicio_LP_access
{
    public partial class Lista : Form
    {
        public Lista()
        {
            InitializeComponent();
        }

        private void criar_Click(object sender, EventArgs e)
        {
            var formulario = new Formulario(this, 0);
            formulario.Show();
            this.Hide();
        }

        private void consultar(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\yasmin.sandri.sales\source\repos\exercicio_LP_access\agendaContatos.mdb");
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "Select * from agendaContatos";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable agendaContatos = new DataTable();
            da.Fill(agendaContatos);
            dataGridView1.DataSource = agendaContatos;
            con.Close();
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            // Pegar célula selecionada do Datagrid 
            var selectedCells = this.dataGridView1.SelectedCells;
            if (selectedCells.Count == 0)
            {
                return;
            }

            var selectedRowIndex = selectedCells[0].RowIndex;
            var rowData = this.dataGridView1.Rows[selectedRowIndex];
            var id = (int)rowData.Cells[0].Value;

            var formulario = new Formulario(this, id);
            formulario.Show();
            this.Hide();
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\yasmin.sandri.sales\source\repos\exercicio_LP_access\agendaContatos.mdb");
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            var selectedCells = this.dataGridView1.SelectedCells;
            if (selectedCells.Count == 0)
            {
                return;
            }

            var selectedRowIndex = selectedCells[0].RowIndex;
            var rowData = this.dataGridView1.Rows[selectedRowIndex];
            var id = (int)rowData.Cells[0].Value;
            cmd.CommandText = "DELETE from agendaContatos WHERE id = " + id;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            int rowAffected = cmd.ExecuteNonQuery();
            if (rowAffected == 0)
            {
                MessageBox.Show("Nenhuma linha encontrada.");
            }
            else
            {
                MessageBox.Show("Dados excluidos com sucesso");
            }

            consultar(null, null);
            con.Close();
        }
    }
}
