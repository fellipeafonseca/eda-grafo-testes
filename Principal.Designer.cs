namespace GrafoEDA
{
    partial class Principal
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbLama = new System.Windows.Forms.RadioButton();
            this.rbSaida = new System.Windows.Forms.RadioButton();
            this.rbPlayer = new System.Windows.Forms.RadioButton();
            this.rbHole = new System.Windows.Forms.RadioButton();
            this.btnGrafo = new System.Windows.Forms.Button();
            this.btnLargura = new System.Windows.Forms.Button();
            this.btnProfundidade = new System.Windows.Forms.Button();
            this.btnMinimo = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbLama);
            this.groupBox1.Controls.Add(this.rbSaida);
            this.groupBox1.Controls.Add(this.rbPlayer);
            this.groupBox1.Controls.Add(this.rbHole);
            this.groupBox1.Location = new System.Drawing.Point(391, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(168, 141);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurar Labirinto:";
            // 
            // rbLama
            // 
            this.rbLama.AutoSize = true;
            this.rbLama.Location = new System.Drawing.Point(8, 50);
            this.rbLama.Margin = new System.Windows.Forms.Padding(4);
            this.rbLama.Name = "rbLama";
            this.rbLama.Size = new System.Drawing.Size(64, 20);
            this.rbLama.TabIndex = 4;
            this.rbLama.Text = "Lama";
            this.rbLama.UseVisualStyleBackColor = true;
            // 
            // rbSaida
            // 
            this.rbSaida.AutoSize = true;
            this.rbSaida.Location = new System.Drawing.Point(8, 106);
            this.rbSaida.Margin = new System.Windows.Forms.Padding(4);
            this.rbSaida.Name = "rbSaida";
            this.rbSaida.Size = new System.Drawing.Size(67, 20);
            this.rbSaida.TabIndex = 3;
            this.rbSaida.Text = "Saída";
            this.rbSaida.UseVisualStyleBackColor = true;
            // 
            // rbPlayer
            // 
            this.rbPlayer.AutoSize = true;
            this.rbPlayer.Location = new System.Drawing.Point(8, 78);
            this.rbPlayer.Margin = new System.Windows.Forms.Padding(4);
            this.rbPlayer.Name = "rbPlayer";
            this.rbPlayer.Size = new System.Drawing.Size(114, 20);
            this.rbPlayer.TabIndex = 2;
            this.rbPlayer.Text = "Personagem";
            this.rbPlayer.UseVisualStyleBackColor = true;
            // 
            // rbHole
            // 
            this.rbHole.Checked = true;
            this.rbHole.Location = new System.Drawing.Point(8, 25);
            this.rbHole.Margin = new System.Windows.Forms.Padding(4);
            this.rbHole.Name = "rbHole";
            this.rbHole.Size = new System.Drawing.Size(75, 20);
            this.rbHole.TabIndex = 0;
            this.rbHole.TabStop = true;
            this.rbHole.Text = "Buraco";
            this.rbHole.UseVisualStyleBackColor = true;
            // 
            // btnGrafo
            // 
            this.btnGrafo.Location = new System.Drawing.Point(392, 150);
            this.btnGrafo.Name = "btnGrafo";
            this.btnGrafo.Size = new System.Drawing.Size(164, 53);
            this.btnGrafo.TabIndex = 3;
            this.btnGrafo.Text = "Criar o Grafo";
            this.btnGrafo.UseVisualStyleBackColor = true;
            this.btnGrafo.Click += new System.EventHandler(this.btnGrafo_Click);
            // 
            // btnLargura
            // 
            this.btnLargura.Location = new System.Drawing.Point(392, 209);
            this.btnLargura.Name = "btnLargura";
            this.btnLargura.Size = new System.Drawing.Size(167, 55);
            this.btnLargura.TabIndex = 4;
            this.btnLargura.Text = "Passeio Largura";
            this.btnLargura.UseVisualStyleBackColor = true;
            this.btnLargura.Click += new System.EventHandler(this.btnLargura_Click);
            // 
            // btnProfundidade
            // 
            this.btnProfundidade.Location = new System.Drawing.Point(392, 270);
            this.btnProfundidade.Name = "btnProfundidade";
            this.btnProfundidade.Size = new System.Drawing.Size(167, 53);
            this.btnProfundidade.TabIndex = 5;
            this.btnProfundidade.Text = "Passeio Profundidade";
            this.btnProfundidade.UseVisualStyleBackColor = true;
            this.btnProfundidade.Click += new System.EventHandler(this.btnProfundidade_Click);
            // 
            // btnMinimo
            // 
            this.btnMinimo.Location = new System.Drawing.Point(392, 329);
            this.btnMinimo.Name = "btnMinimo";
            this.btnMinimo.Size = new System.Drawing.Size(167, 51);
            this.btnMinimo.TabIndex = 6;
            this.btnMinimo.Text = "CaminhoMinimo";
            this.btnMinimo.UseVisualStyleBackColor = true;
            this.btnMinimo.Click += new System.EventHandler(this.btnMinimo_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 392);
            this.Controls.Add(this.btnMinimo);
            this.Controls.Add(this.btnProfundidade);
            this.Controls.Add(this.btnLargura);
            this.Controls.Add(this.btnGrafo);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Principal";
            this.Text = "Labirinto";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPlayer;
        private System.Windows.Forms.RadioButton rbHole;
        private System.Windows.Forms.RadioButton rbSaida;
        private System.Windows.Forms.Button btnGrafo;
        private System.Windows.Forms.Button btnLargura;
        private System.Windows.Forms.Button btnProfundidade;
        private System.Windows.Forms.Button btnMinimo;
        private System.Windows.Forms.RadioButton rbLama;
    }
}

