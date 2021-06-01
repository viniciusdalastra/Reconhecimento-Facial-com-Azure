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
    public partial class FormReconhecimento : Form
    {
        internal NpgsqlConnection conexao = null;
        public FormReconhecimento(NpgsqlConnection conexao)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            this.conexao = conexao;
            carregaConsulta();
        }

        private void FormReconhecimento_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormManutencaoReconhecimento form = new FormManutencaoReconhecimento(conexao);
            form.ShowDialog();
            carregaConsulta();
        }

        private void carregaConsulta()
        {
            if (textBox1.Text.Length > 0)
            {
                dataGridView1.DataSource = ControllerReconhecimento.getConsulta(conexao, comboBox1.SelectedIndex, comboBox2.SelectedIndex, textBox1.Text);
            }
            else
            {
                dataGridView1.DataSource = ControllerReconhecimento.getConsulta(conexao);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                carregaConsulta();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int sequencia = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            bool excluiu = ControllerReconhecimento.setExclui(conexao, sequencia);
            if (excluiu)
            {
                MessageBox.Show("Reconhecimento Removido Com Sucesso.");
            }
            else
            {
                MessageBox.Show("Erro ao Remover Reconhecimento.");
            }
            carregaConsulta();
        }
    }
}
