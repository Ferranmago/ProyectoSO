namespace InterfazJuego
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonIniciarSesion = new System.Windows.Forms.Button();
            this.buttonRegistrarse = new System.Windows.Forms.Button();
            this.textBoxContraseña = new System.Windows.Forms.TextBox();
            this.textBoxUsuario = new System.Windows.Forms.TextBox();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.labelContraseña = new System.Windows.Forms.Label();
            this.buttonDesconectarse = new System.Windows.Forms.Button();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxDinero = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxApuesta = new System.Windows.Forms.TextBox();
            this.btnNegro = new System.Windows.Forms.Button();
            this.btnRojo = new System.Windows.Forms.Button();
            this.btnImpar = new System.Windows.Forms.Button();
            this.btnPar = new System.Windows.Forms.Button();
            this.btn1936 = new System.Windows.Forms.Button();
            this.btn118 = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DGV2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonIniciarSesion
            // 
            this.buttonIniciarSesion.Location = new System.Drawing.Point(11, 138);
            this.buttonIniciarSesion.Name = "buttonIniciarSesion";
            this.buttonIniciarSesion.Size = new System.Drawing.Size(139, 37);
            this.buttonIniciarSesion.TabIndex = 0;
            this.buttonIniciarSesion.Text = "Iniciar Sesion";
            this.buttonIniciarSesion.UseVisualStyleBackColor = true;
            this.buttonIniciarSesion.Click += new System.EventHandler(this.buttonIniciarSesion_Click);
            // 
            // buttonRegistrarse
            // 
            this.buttonRegistrarse.Location = new System.Drawing.Point(11, 182);
            this.buttonRegistrarse.Name = "buttonRegistrarse";
            this.buttonRegistrarse.Size = new System.Drawing.Size(139, 37);
            this.buttonRegistrarse.TabIndex = 1;
            this.buttonRegistrarse.Text = "Registrarse";
            this.buttonRegistrarse.UseVisualStyleBackColor = true;
            this.buttonRegistrarse.Click += new System.EventHandler(this.buttonRegistrarse_Click);
            // 
            // textBoxContraseña
            // 
            this.textBoxContraseña.Location = new System.Drawing.Point(11, 102);
            this.textBoxContraseña.Name = "textBoxContraseña";
            this.textBoxContraseña.Size = new System.Drawing.Size(138, 31);
            this.textBoxContraseña.TabIndex = 2;
            this.textBoxContraseña.UseSystemPasswordChar = true;
            // 
            // textBoxUsuario
            // 
            this.textBoxUsuario.Location = new System.Drawing.Point(11, 38);
            this.textBoxUsuario.Name = "textBoxUsuario";
            this.textBoxUsuario.Size = new System.Drawing.Size(138, 31);
            this.textBoxUsuario.TabIndex = 3;
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Location = new System.Drawing.Point(47, 12);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(72, 25);
            this.labelUsuario.TabIndex = 4;
            this.labelUsuario.Text = "Usuario";
            // 
            // labelContraseña
            // 
            this.labelContraseña.AutoSize = true;
            this.labelContraseña.Location = new System.Drawing.Point(31, 73);
            this.labelContraseña.Name = "labelContraseña";
            this.labelContraseña.Size = new System.Drawing.Size(101, 25);
            this.labelContraseña.TabIndex = 5;
            this.labelContraseña.Text = "Contraseña";
            // 
            // buttonDesconectarse
            // 
            this.buttonDesconectarse.Location = new System.Drawing.Point(11, 223);
            this.buttonDesconectarse.Name = "buttonDesconectarse";
            this.buttonDesconectarse.Size = new System.Drawing.Size(139, 73);
            this.buttonDesconectarse.TabIndex = 7;
            this.buttonDesconectarse.Text = "Desconectarse";
            this.buttonDesconectarse.UseVisualStyleBackColor = true;
            this.buttonDesconectarse.Click += new System.EventHandler(this.buttonDesconectarse_Click);
            // 
            // DGV1
            // 
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Location = new System.Drawing.Point(186, 72);
            this.DGV1.Name = "DGV1";
            this.DGV1.RowHeadersWidth = 62;
            this.DGV1.Size = new System.Drawing.Size(400, 427);
            this.DGV1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(284, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Lista de Conectados";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::InterfazJuego.Properties.Resources.giphy;
            this.pictureBox1.Location = new System.Drawing.Point(1055, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(497, 557);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // textBoxDinero
            // 
            this.textBoxDinero.Location = new System.Drawing.Point(711, 100);
            this.textBoxDinero.Name = "textBoxDinero";
            this.textBoxDinero.Size = new System.Drawing.Size(138, 31);
            this.textBoxDinero.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(720, 475);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 50);
            this.label2.TabIndex = 13;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(1153, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(319, 25);
            this.label3.TabIndex = 14;
            this.label3.Text = "CLICA EN LA RULETA PARA JUGAR!!";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(671, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(248, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "Introduzca cantidad a apostar";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(686, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(186, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "Introduzca su apuesta";
            // 
            // textBoxApuesta
            // 
            this.textBoxApuesta.Location = new System.Drawing.Point(701, 195);
            this.textBoxApuesta.Name = "textBoxApuesta";
            this.textBoxApuesta.Size = new System.Drawing.Size(150, 31);
            this.textBoxApuesta.TabIndex = 17;
            // 
            // btnNegro
            // 
            this.btnNegro.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnNegro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnNegro.Location = new System.Drawing.Point(711, 252);
            this.btnNegro.Name = "btnNegro";
            this.btnNegro.Size = new System.Drawing.Size(80, 47);
            this.btnNegro.TabIndex = 18;
            this.btnNegro.Text = "NEGRO";
            this.btnNegro.UseVisualStyleBackColor = false;
            this.btnNegro.Click += new System.EventHandler(this.btnNegro_Click);
            // 
            // btnRojo
            // 
            this.btnRojo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRojo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRojo.Location = new System.Drawing.Point(791, 252);
            this.btnRojo.Name = "btnRojo";
            this.btnRojo.Size = new System.Drawing.Size(80, 47);
            this.btnRojo.TabIndex = 19;
            this.btnRojo.Text = "ROJO";
            this.btnRojo.UseVisualStyleBackColor = false;
            this.btnRojo.Click += new System.EventHandler(this.btnRojo_Click);
            // 
            // btnImpar
            // 
            this.btnImpar.BackColor = System.Drawing.Color.SeaGreen;
            this.btnImpar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnImpar.Location = new System.Drawing.Point(791, 328);
            this.btnImpar.Name = "btnImpar";
            this.btnImpar.Size = new System.Drawing.Size(80, 47);
            this.btnImpar.TabIndex = 21;
            this.btnImpar.Text = "IMPAR";
            this.btnImpar.UseVisualStyleBackColor = false;
            this.btnImpar.Click += new System.EventHandler(this.btnImpar_Click);
            // 
            // btnPar
            // 
            this.btnPar.BackColor = System.Drawing.Color.Khaki;
            this.btnPar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnPar.Location = new System.Drawing.Point(711, 328);
            this.btnPar.Name = "btnPar";
            this.btnPar.Size = new System.Drawing.Size(80, 47);
            this.btnPar.TabIndex = 20;
            this.btnPar.Text = "PAR";
            this.btnPar.UseVisualStyleBackColor = false;
            this.btnPar.Click += new System.EventHandler(this.btnPar_Click);
            // 
            // btn1936
            // 
            this.btn1936.BackColor = System.Drawing.Color.Chocolate;
            this.btn1936.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn1936.Location = new System.Drawing.Point(791, 404);
            this.btn1936.Name = "btn1936";
            this.btn1936.Size = new System.Drawing.Size(80, 47);
            this.btn1936.TabIndex = 23;
            this.btn1936.Text = "19-36";
            this.btn1936.UseVisualStyleBackColor = false;
            this.btn1936.Click += new System.EventHandler(this.btn1936_Click);
            // 
            // btn118
            // 
            this.btn118.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn118.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn118.Location = new System.Drawing.Point(711, 404);
            this.btn118.Name = "btn118";
            this.btn118.Size = new System.Drawing.Size(80, 47);
            this.btn118.TabIndex = 22;
            this.btn118.Text = "1-18";
            this.btn118.UseVisualStyleBackColor = false;
            this.btn118.Click += new System.EventHandler(this.btn118_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(605, 480);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(93, 41);
            this.btnReset.TabIndex = 24;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblSaldo
            // 
            this.lblSaldo.Location = new System.Drawing.Point(720, 29);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(138, 34);
            this.lblSaldo.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(740, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 25);
            this.label6.TabIndex = 26;
            this.label6.Text = "Saldo";
            // 
            // DGV2
            // 
            this.DGV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV2.Location = new System.Drawing.Point(186, 570);
            this.DGV2.Name = "DGV2";
            this.DGV2.RowHeadersWidth = 62;
            this.DGV2.RowTemplate.Height = 33;
            this.DGV2.Size = new System.Drawing.Size(400, 206);
            this.DGV2.TabIndex = 27;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1638, 874);
            this.Controls.Add(this.DGV2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btn1936);
            this.Controls.Add(this.btn118);
            this.Controls.Add(this.btnImpar);
            this.Controls.Add(this.btnPar);
            this.Controls.Add(this.btnRojo);
            this.Controls.Add(this.btnNegro);
            this.Controls.Add(this.textBoxApuesta);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxDinero);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DGV1);
            this.Controls.Add(this.buttonDesconectarse);
            this.Controls.Add(this.labelContraseña);
            this.Controls.Add(this.labelUsuario);
            this.Controls.Add(this.textBoxUsuario);
            this.Controls.Add(this.textBoxContraseña);
            this.Controls.Add(this.buttonRegistrarse);
            this.Controls.Add(this.buttonIniciarSesion);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonIniciarSesion;
        private Button buttonRegistrarse;
        private TextBox textBoxContraseña;
        private TextBox textBoxUsuario;
        private Label labelUsuario;
        private Label labelContraseña;
        private Button buttonDesconectarse;
        private DataGridView DGV1;
        private Label label1;
        private PictureBox pictureBox1;
        private TextBox textBoxDinero;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBoxApuesta;
        private Button btnNegro;
        private Button btnRojo;
        private Button btnImpar;
        private Button btnPar;
        private Button btn1936;
        private Button btn118;
        private Button btnReset;
        private Label lblSaldo;
        private Label label6;
        private DataGridView DGV2;
    }
}