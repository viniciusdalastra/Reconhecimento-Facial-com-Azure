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
using Npgsql;
using Projeto.Controller;

namespace Projeto
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection device;
        private VideoCaptureDevice image;
        internal NpgsqlConnection conexao = null;
        private FormHistorico telaHistorico = null;

        public Form1()
        {
            InitializeComponent();
            //DEFINE TELA CHEIAS
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            conexao = Conexao.getConexao();

            FormHistorico telaManutencao = new FormHistorico();

            telaHistorico = telaManutencao;

            telaManutencao.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            device = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo capture in device)
            {
                comboBox1.Items.Add(capture.Name);

            }
            comboBox1.SelectedIndex = 0;

            image = new VideoCaptureDevice(device[comboBox1.SelectedIndex].MonikerString);
        }

        /*Botão Ligar Camera*/
        private void button1_Click(object sender, EventArgs e)
        {
            image = new VideoCaptureDevice(device[comboBox1.SelectedIndex].MonikerString);
            image.NewFrame += new NewFrameEventHandler(camera);
            image.Start();
        }

        /*Botão Desligar Camera*/
        private void button3_Click(object sender, EventArgs e)
        {
            if (image.IsRunning)
            {
                image.Stop();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            realizaReconhecimento();
        }

        void camera (object sender, NewFrameEventArgs e)
        {
            Bitmap bit = (Bitmap) e.Frame.Clone();
            pictureBox1.Image = bit;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void realizaReconhecimento()
        {
            while(!button2.Enabled)
            {
                object imagem = pictureBox1.Image;
                string sResponse = await Reconhecedor.reconhece(imagem);

                if (sResponse.Contains("error"))
                {
                    continue;
                }
                await Identificador.reconhece(conexao,sResponse, imagem, telaHistorico);

                await Task.Delay(3000);
            }
        }
        /*Botão das Manutenções*/
        private void button4_Click(object sender, EventArgs e)
        {
            FormManutencao telaManutencao = new FormManutencao(conexao);
            telaManutencao.Show();
        }
        /*Botão de Fechar*/
        private void button6_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Deseja encerrar a aplicação?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }
        /*Botão de Relatórios*/
        private void button5_Click(object sender, EventArgs e)
        {}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            button3_Click(sender, e);
            Conexao.setFecharConexao(conexao);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }
        
    }
}
