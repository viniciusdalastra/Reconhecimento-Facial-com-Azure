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
    public partial class FormPessoa : Form
    {
        internal NpgsqlConnection conexao = null;
        public FormPessoa(NpgsqlConnection conexao)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            this.conexao = conexao;
            carregaConsulta();
        }

        private void FormPessoa_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormManutencaoPessoa form = new FormManutencaoPessoa(conexao);
            form.ShowDialog();
            carregaConsulta();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            bool excluiu = ControllerPessoa.setExclui(conexao, codigo);
            if (excluiu)
            {
                MessageBox.Show("Pessoa Removida Com Sucesso.");
            }
            else
            {
                MessageBox.Show("Erro ao Remover Pessoa.");
            }
            carregaConsulta();
        }

        private void carregaConsulta()
        {
            if (textBox1.Text.Length > 0)
            {
                dataGridView1.DataSource = ControllerPessoa.getConsulta(conexao,comboBox1.SelectedIndex,comboBox2.SelectedIndex,textBox1.Text);
            }
            else
            {
                dataGridView1.DataSource = ControllerPessoa.getConsulta(conexao);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                carregaConsulta();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            String nome = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            int codigo_universitario = int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());

            ModelPessoa pessoa = new ModelPessoa();

            pessoa.codigo = codigo;
            pessoa.nome = nome;
            pessoa.codigo_universitario = codigo_universitario;

            bool alterou = ControllerPessoa.setAltera(conexao, pessoa);

            if (alterou)
            {
                MessageBox.Show("Pessoa Alterada com Sucesso");
            }
            else
            {
                MessageBox.Show("Erro ao alterar Pessoa");
            }
            carregaConsulta();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
