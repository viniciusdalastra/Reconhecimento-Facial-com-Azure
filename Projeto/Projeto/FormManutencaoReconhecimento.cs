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
    public partial class FormManutencaoReconhecimento : Form
    {

        internal NpgsqlConnection conexao = null;
        public FormManutencaoReconhecimento(NpgsqlConnection conexao)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.conexao = conexao;
        }

        private void FormManutencaoReconhecimento_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sequencia = int.Parse(textBox1.Text);
            String identificador = textBox2.Text;
            DateTime data = dateTimePicker1.Value;

            ModelReconhecimento reconhecimento = new ModelReconhecimento();
            reconhecimento.sequencia = sequencia;
            reconhecimento.identificador = identificador;
            reconhecimento.data = data;

            bool incluiu = ControllerReconhecimento.setInclui(conexao, reconhecimento);

            if (incluiu)
            {
                MessageBox.Show("Reconhecimento Cadastrado");
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao Cadastrar Reconhecimento");
            }
        }
    }
}
