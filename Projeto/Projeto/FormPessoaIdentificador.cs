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
    public partial class FormPessoaIdentificador : Form
    {
        internal NpgsqlConnection conexao = null;
        public FormPessoaIdentificador(NpgsqlConnection conexao)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            this.conexao = conexao;
            carregaConsulta();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormPessoaIdentificador_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                carregaConsulta();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormManutencaoPessoaIdentificador form = new FormManutencaoPessoaIdentificador(conexao);
            form.ShowDialog();
            carregaConsulta();
        }

        private void carregaConsulta()
        {
            if (textBox1.Text.Length > 0)
            {
                dataGridView1.DataSource = ControllerPessoaIdentificador.getConsulta(conexao, comboBox1.SelectedIndex, comboBox2.SelectedIndex, textBox1.Text);
            }
            else
            {
                dataGridView1.DataSource = ControllerPessoaIdentificador.getConsulta(conexao);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            String identificador = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            int codigo_pessoa = int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());

            ModelPessoaIdentificador pessoaIdentificador = new ModelPessoaIdentificador();

            pessoaIdentificador.codigo = codigo;
            pessoaIdentificador.identificador = identificador;
            pessoaIdentificador.codigo_pessoa = codigo_pessoa;

            bool alterou = ControllerPessoaIdentificador.setAltera(conexao, pessoaIdentificador);

            if (alterou)
            {
                MessageBox.Show("Identificador Alterado com Sucesso");
            }
            else
            {
                MessageBox.Show("Erro ao alterar Identificador");
            }
            carregaConsulta();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            bool excluiu = ControllerPessoaIdentificador.setExclui(conexao, codigo);
            if (excluiu)
            {
                MessageBox.Show("Identificador Removido Com Sucesso.");
            }
            else
            {
                MessageBox.Show("Erro ao Remover Identificador.");
            }
            carregaConsulta();
        }
    }
}
