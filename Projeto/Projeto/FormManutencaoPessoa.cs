using Npgsql;
using Projeto.Controller;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class FormManutencaoPessoa : Form
    {
        internal NpgsqlConnection conexao = null;
        public FormManutencaoPessoa(NpgsqlConnection conexao)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.conexao = conexao;
        }

        private void FormManutencaoPessoa_Load(object sender, EventArgs e)
        {
            int sequencia = GetSequencia();
            textBox1.Text = sequencia.ToString();
        }

        private int GetSequencia()
        {
            return ControllerPessoa.getSequenciaPessoa(conexao);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(textBox1.Text);
            String nome = textBox2.Text;
            int codigo_universitario = int.Parse(textBox3.Text);

            ModelPessoa pessoa = new ModelPessoa();
            pessoa.codigo = codigo;
            pessoa.nome = nome;
            pessoa.codigo_universitario = codigo_universitario;

            bool incluiu = ControllerPessoa.setInclui(conexao, pessoa);

            if (incluiu)
            {
                MessageBox.Show("Pessoa Incluida com Sucesso");
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao Incluir Pessoa");
            }
        }
    }
}
