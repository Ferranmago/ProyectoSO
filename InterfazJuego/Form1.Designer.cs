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
            this.buttonConectarse = new System.Windows.Forms.Button();
            this.buttonDesconectarse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonIniciarSesion
            // 
            this.buttonIniciarSesion.Location = new System.Drawing.Point(218, 255);
            this.buttonIniciarSesion.Name = "buttonIniciarSesion";
            this.buttonIniciarSesion.Size = new System.Drawing.Size(173, 37);
            this.buttonIniciarSesion.TabIndex = 0;
            this.buttonIniciarSesion.Text = "Iniciar Sesion";
            this.buttonIniciarSesion.UseVisualStyleBackColor = true;
            this.buttonIniciarSesion.Click += new System.EventHandler(this.buttonIniciarSesion_Click);
            // 
            // buttonRegistrarse
            // 
            this.buttonRegistrarse.Location = new System.Drawing.Point(431, 255);
            this.buttonRegistrarse.Name = "buttonRegistrarse";
            this.buttonRegistrarse.Size = new System.Drawing.Size(139, 37);
            this.buttonRegistrarse.TabIndex = 1;
            this.buttonRegistrarse.Text = "Registrarse";
            this.buttonRegistrarse.UseVisualStyleBackColor = true;
            this.buttonRegistrarse.Click += new System.EventHandler(this.buttonRegistrarse_Click);
            // 
            // textBoxContraseña
            // 
            this.textBoxContraseña.Location = new System.Drawing.Point(218, 190);
            this.textBoxContraseña.Name = "textBoxContraseña";
            this.textBoxContraseña.Size = new System.Drawing.Size(352, 31);
            this.textBoxContraseña.TabIndex = 2;
            // 
            // textBoxUsuario
            // 
            this.textBoxUsuario.Location = new System.Drawing.Point(218, 122);
            this.textBoxUsuario.Name = "textBoxUsuario";
            this.textBoxUsuario.Size = new System.Drawing.Size(352, 31);
            this.textBoxUsuario.TabIndex = 3;
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Location = new System.Drawing.Point(369, 94);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(72, 25);
            this.labelUsuario.TabIndex = 4;
            this.labelUsuario.Text = "Usuario";
            // 
            // labelContraseña
            // 
            this.labelContraseña.AutoSize = true;
            this.labelContraseña.Location = new System.Drawing.Point(358, 162);
            this.labelContraseña.Name = "labelContraseña";
            this.labelContraseña.Size = new System.Drawing.Size(101, 25);
            this.labelContraseña.TabIndex = 5;
            this.labelContraseña.Text = "Contraseña";
            // 
            // buttonConectarse
            // 
            this.buttonConectarse.Location = new System.Drawing.Point(12, 12);
            this.buttonConectarse.Name = "buttonConectarse";
            this.buttonConectarse.Size = new System.Drawing.Size(142, 73);
            this.buttonConectarse.TabIndex = 6;
            this.buttonConectarse.Text = "Conectarse";
            this.buttonConectarse.UseVisualStyleBackColor = true;
            this.buttonConectarse.Click += new System.EventHandler(this.buttonConectarse_Click);
            // 
            // buttonDesconectarse
            // 
            this.buttonDesconectarse.Location = new System.Drawing.Point(646, 12);
            this.buttonDesconectarse.Name = "buttonDesconectarse";
            this.buttonDesconectarse.Size = new System.Drawing.Size(142, 73);
            this.buttonDesconectarse.TabIndex = 7;
            this.buttonDesconectarse.Text = "Desconectarse";
            this.buttonDesconectarse.UseVisualStyleBackColor = true;
            this.buttonDesconectarse.Click += new System.EventHandler(this.buttonDesconectarse_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonDesconectarse);
            this.Controls.Add(this.buttonConectarse);
            this.Controls.Add(this.labelContraseña);
            this.Controls.Add(this.labelUsuario);
            this.Controls.Add(this.textBoxUsuario);
            this.Controls.Add(this.textBoxContraseña);
            this.Controls.Add(this.buttonRegistrarse);
            this.Controls.Add(this.buttonIniciarSesion);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private Button buttonConectarse;
        private Button buttonDesconectarse;
    }
}