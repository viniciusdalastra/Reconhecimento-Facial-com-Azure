using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using Projeto.Model;
using Projeto.Controller;
using System.Collections;

namespace Projeto
{
    public partial class FormManutencaoPessoaIdentificador : Form
    {
        private FilterInfoCollection device;
        private VideoCaptureDevice image;
        internal NpgsqlConnection conexao = null;
        private ArrayList imagens = new ArrayList();
        public FormManutencaoPessoaIdentificador(NpgsqlConnection conexao)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.conexao = conexao;
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox4.Text != null)
            {   
                await Pessoa.removeIdPessoa(textBox4.Text);
            }
            this.Close();
        }

        private void FormManutencaoPessoaIdentificador_Load(object sender, EventArgs e)
        {
            device = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo capture in device)
            {
                comboBox1.Items.Add(capture.Name);

            }
            comboBox1.SelectedIndex = 0;

            image = new VideoCaptureDevice(device[comboBox1.SelectedIndex].MonikerString);
            imagens = new ArrayList();
            int sequencia = GetSequencia();
            textBox6.Text = sequencia.ToString();
        }

        private int GetSequencia()
        {
            return ControllerPessoaIdentificador.getSequenciaIdentificador(conexao);
        }

        void camera(object sender, NewFrameEventArgs e)
        {
            Bitmap bit = (Bitmap)e.Frame.Clone();
            pictureBox1.Image = bit;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            image = new VideoCaptureDevice(device[comboBox1.SelectedIndex].MonikerString);
            image.NewFrame += new NewFrameEventHandler(camera);
            image.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (image.IsRunning)
            {
                image.Stop();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(textBox6.Text);
            int codigo_pessoa = int.Parse(textBox5.Text);
            String identificador = textBox4.Text;

            ModelPessoaIdentificador pessoaIdentificador = new ModelPessoaIdentificador();
            pessoaIdentificador.codigo = codigo;
            pessoaIdentificador.codigo_pessoa = codigo_pessoa;
            pessoaIdentificador.identificador = identificador;

            bool incluiu = ControllerPessoaIdentificador.setInclui(conexao, pessoaIdentificador);

            if (incluiu)
            {
                MessageBox.Show("Identificador Incluido com Sucesso");
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao Incluir Identificador");
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (imagens.Count < 4)
            {
                MessageBox.Show("Favor adicionar mais " + (3 - imagens.Count) +" imagens");
            }
            else if (textBox4.Text == "" || textBox4.Text == null)
            {
                MessageBox.Show("Gerar ID de PESSOA!!");
            }
            else
            {
                for (var i = 0; i < imagens.Count; i++)
                {
                    Boolean cadastrouRosto = await RostoPessoa.adicionaFaceAsync(textBox4.Text, imagens[i]);
                    if (cadastrouRosto)
                    {
                        MessageBox.Show("Face Cadastradas com Sucesso");
                    }
                    else
                    {
                        MessageBox.Show("Erro ao Cadastrar Imagens");
                    }
                }
                button7_Click(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Imagem Adicionada");
            imagens.Add(pictureBox1.Image);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lista de Imagens Limpa");
            imagens = new ArrayList(); 
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            textBox4.Text = await Pessoa.CriaIdPessoa(textBox5.Text);
            MessageBox.Show("Gerado ID com sucesso");
        }

        private void FormManutencaoPessoaIdentificador_FormClosing(object sender, FormClosingEventArgs e)
        {
            button4_Click(sender, e);
        }
    }
}
