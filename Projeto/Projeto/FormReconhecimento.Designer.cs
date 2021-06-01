namespace Projeto
{
    partial class FormReconhecimento
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.recsequencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recdata = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_pessoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_universitario_pessoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nome_pessoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "FECHAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(730, 51);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AÇÕES";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(196, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "EXCLUIR";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(88, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "CADASTRAR";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(730, 82);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILTRO";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Contém",
            "Inicia com",
            "Termina com",
            "Igual",
            "Maior ou Igual",
            "Menor ou Igual"});
            this.comboBox2.Location = new System.Drawing.Point(177, 38);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 5;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Sequência",
            "Data",
            "Identificador",
            "Código Pessoa",
            "Código Universitário Pessoa",
            "Nome Pessoa"});
            this.comboBox1.Location = new System.Drawing.Point(7, 37);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(149, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(316, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(408, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(313, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Descrição";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tipo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Campo";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.recsequencia,
            this.recdata,
            this.identificador,
            this.codigo_pessoa,
            this.codigo_universitario_pessoa,
            this.nome_pessoa});
            this.dataGridView1.Location = new System.Drawing.Point(13, 158);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(729, 329);
            this.dataGridView1.TabIndex = 6;
            // 
            // recsequencia
            // 
            this.recsequencia.DataPropertyName = "recsequencia";
            this.recsequencia.HeaderText = "Sequência";
            this.recsequencia.Name = "recsequencia";
            // 
            // recdata
            // 
            this.recdata.DataPropertyName = "recdata";
            this.recdata.HeaderText = "Data";
            this.recdata.Name = "recdata";
            // 
            // identificador
            // 
            this.identificador.DataPropertyName = "identificador";
            this.identificador.HeaderText = "Identificador";
            this.identificador.Name = "identificador";
            // 
            // codigo_pessoa
            // 
            this.codigo_pessoa.DataPropertyName = "codigo_pessoa";
            this.codigo_pessoa.HeaderText = "Código Pessoa";
            this.codigo_pessoa.Name = "codigo_pessoa";
            // 
            // codigo_universitario_pessoa
            // 
            this.codigo_universitario_pessoa.DataPropertyName = "codigo_universitario_pessoa";
            this.codigo_universitario_pessoa.HeaderText = "Código Universitário Pessoa";
            this.codigo_universitario_pessoa.Name = "codigo_universitario_pessoa";
            // 
            // nome_pessoa
            // 
            this.nome_pessoa.DataPropertyName = "nome_pessoa";
            this.nome_pessoa.HeaderText = "Nome Pessoa";
            this.nome_pessoa.Name = "nome_pessoa";
            // 
            // FormReconhecimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 605);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormReconhecimento";
            this.Text = "FormReconhecimento";
            this.Load += new System.EventHandler(this.FormReconhecimento_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn recsequencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn recdata;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo_pessoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo_universitario_pessoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn nome_pessoa;
    }
}