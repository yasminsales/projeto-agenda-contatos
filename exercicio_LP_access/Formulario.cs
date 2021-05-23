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
    public partial class Formulario : Form
    {
        private Form lista;
        private int idAtualizacao;

        private string TratarCampoVazio(object valor)
        {
            if (valor is System.DBNull)
            {
                return "";
            }

            return (string)valor;
        }

        private void PreencherDados()
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\yasmin.sandri.sales\source\repos\exercicio_LP_access\agendaContatos.mdb");
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "Select * from agendaContatos where ID = " + idAtualizacao;

            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                this.textBox_nome.Text = TratarCampoVazio(reader["Nome"]);
                this.textBox_estado.Text = TratarCampoVazio(reader["Estado"]);
                this.textBox_cidade.Text = TratarCampoVazio(reader["Cidade"]);
                this.textBox_bairro.Text = TratarCampoVazio(reader["Bairro"]);
                this.textBox_rua.Text = TratarCampoVazio(reader["Rua"]);
                this.textBox_numero.Text = TratarCampoVazio(reader["Numero"]);
                this.textBox_complemento.Text = TratarCampoVazio(reader["Complemento"]);
                this.textBox_cep.Text = TratarCampoVazio(reader["CEP"]);
                this.textBox_email.Text = TratarCampoVazio(reader["Email"]);
                this.textBox_telefone.Text = TratarCampoVazio(reader["Telefone"]);
                this.textBox_telefoneResidencial.Text = TratarCampoVazio(reader["Telefone_Residencial"]);
            }
            con.Close();
        }

        public Formulario(Form lista, int idAtualizacao)
        {
            this.lista = lista;
            this.idAtualizacao = idAtualizacao;
            InitializeComponent();

            if (idAtualizacao == 0)
            {
                this.Text = "Criar novo contato";
            }
            else
            {
                PreencherDados();
                this.Text = "Editando contato " + idAtualizacao;
            }

        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.lista.Show();
            this.Close();
        }

        private void Criar()
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\yasmin.sandri.sales\source\repos\exercicio_LP_access\agendaContatos.mdb");
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO agendaContatos  " +
                    " (Nome, Estado, Cidade, Bairro, Rua, Numero, Complemento, CEP, Email, Telefone, Telefone_Residencial)" +
                    "VALUES " +
                    " (@Nome, @Estado, @Cidade, @Bairro, @Rua, @Numero, @Complemento, @CEP, @Email, @Telefone, @Telefone_Residencial);";

            cmd.Parameters.AddWithValue("Nome", this.textBox_nome.Text);
            cmd.Parameters.AddWithValue("Estado", this.textBox_estado.Text);
            cmd.Parameters.AddWithValue("Cidade", this.textBox_cidade.Text);
            cmd.Parameters.AddWithValue("Bairro", this.textBox_bairro.Text);
            cmd.Parameters.AddWithValue("Rua", this.textBox_rua.Text);
            cmd.Parameters.AddWithValue("Numero", this.textBox_numero.Text);
            cmd.Parameters.AddWithValue("Complemento", this.textBox_complemento.Text);
            cmd.Parameters.AddWithValue("CEP", this.textBox_cep.Text);
            cmd.Parameters.AddWithValue("Email", this.textBox_email.Text);
            cmd.Parameters.AddWithValue("Telefone", this.textBox_telefone.Text);
            cmd.Parameters.AddWithValue("Telefone_Residencial", this.textBox_telefoneResidencial.Text);

            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Dados inseridos com sucesso");

            this.lista.Show();
            this.Close();
        }

        private void Atualizar()
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\yasmin.sandri.sales\source\repos\exercicio_LP_access\agendaContatos.mdb");
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE agendaContatos SET" +
                    " Nome = @Nome, Estado = @Estado, Cidade = @Cidade, Bairro = @Bairro, Rua = @Rua, " +
                    "Numero = @Numero, Complemento = @Complemento, CEP = @CEP, " +
                    "Email = @Email, Telefone = @Telefone, Telefone_Residencial = @Telefone_Residencial " +
                    " WHERE Id = " + idAtualizacao;

            cmd.Parameters.AddWithValue("Nome", this.textBox_nome.Text);
            cmd.Parameters.AddWithValue("Estado", this.textBox_estado.Text);
            cmd.Parameters.AddWithValue("Cidade", this.textBox_cidade.Text);
            cmd.Parameters.AddWithValue("Bairro", this.textBox_bairro.Text);
            cmd.Parameters.AddWithValue("Rua", this.textBox_rua.Text);
            cmd.Parameters.AddWithValue("Numero", this.textBox_numero.Text);
            cmd.Parameters.AddWithValue("Complemento", this.textBox_complemento.Text);
            cmd.Parameters.AddWithValue("CEP", this.textBox_cep.Text);
            cmd.Parameters.AddWithValue("Email", this.textBox_email.Text);
            cmd.Parameters.AddWithValue("Telefone", this.textBox_telefone.Text);
            cmd.Parameters.AddWithValue("Telefone_Residencial", this.textBox_telefoneResidencial.Text);

            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Dados atualizados com sucesso");

            this.lista.Show();
            this.Close();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (idAtualizacao == 0)
            {
                Criar();
            }
            else
            {
                Atualizar();
            }
        }
    }
}
