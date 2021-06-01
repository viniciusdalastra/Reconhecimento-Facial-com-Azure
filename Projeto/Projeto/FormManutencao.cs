using Npgsql;
using Projeto.Controller;
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
    public partial class FormManutencao : Form
    {
        internal NpgsqlConnection conexao = null;
        public FormManutencao(NpgsqlConnection conexao)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.conexao = conexao;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /*Botao Pessoa*/
        private void button1_Click(object sender, EventArgs e)
        {
            FormPessoa telaPessoa = new FormPessoa(this.conexao);
            telaPessoa.ShowDialog();
        }
        /*Botao Reconhecimento*/
        private void button2_Click(object sender, EventArgs e)
        {
            FormReconhecimento telaReconhecimento = new FormReconhecimento(this.conexao);
            telaReconhecimento.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormPessoaIdentificador telaPessoaIdentificador = new FormPessoaIdentificador(this.conexao);
            telaPessoaIdentificador.ShowDialog();
        }

        private void FormManutencao_Load(object sender, EventArgs e)
        {

        }

        private async void button3_ClickAsync(object sender, EventArgs e)
        {
            if(await Treinar.treinarApiAsync())
            {
                MessageBox.Show("API Treinada!!");
            }
            else
            {
                MessageBox.Show("Erro ao Treinar API");
            }
        }
    }
}
