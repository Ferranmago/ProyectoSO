namespace InterfazJuego
{
    partial class Form2
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
            this.checkBoxPregunta1 = new System.Windows.Forms.CheckBox();
            this.checkBoxPregunta2 = new System.Windows.Forms.CheckBox();
            this.checkBoxPregunta3 = new System.Windows.Forms.CheckBox();
            this.textBoxIdPartida = new System.Windows.Forms.TextBox();
            this.labelIdPartida = new System.Windows.Forms.Label();
            this.buttonSeleccionar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxPregunta1
            // 
            this.checkBoxPregunta1.AutoSize = true;
            this.checkBoxPregunta1.Location = new System.Drawing.Point(237, 165);
            this.checkBoxPregunta1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxPregunta1.Name = "checkBoxPregunta1";
            this.checkBoxPregunta1.Size = new System.Drawing.Size(293, 29);
            this.checkBoxPregunta1.TabIndex = 0;
            this.checkBoxPregunta1.Text = "Cuánto ha durado esta partida? ";
            this.checkBoxPregunta1.UseVisualStyleBackColor = true;
            this.checkBoxPregunta1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBoxPregunta2
            // 
            this.checkBoxPregunta2.AutoSize = true;
            this.checkBoxPregunta2.Location = new System.Drawing.Point(237, 218);
            this.checkBoxPregunta2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxPregunta2.Name = "checkBoxPregunta2";
            this.checkBoxPregunta2.Size = new System.Drawing.Size(281, 29);
            this.checkBoxPregunta2.TabIndex = 1;
            this.checkBoxPregunta2.Text = "Quién ha ganado esta partida?";
            this.checkBoxPregunta2.UseVisualStyleBackColor = true;
            this.checkBoxPregunta2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBoxPregunta3
            // 
            this.checkBoxPregunta3.AutoSize = true;
            this.checkBoxPregunta3.Location = new System.Drawing.Point(237, 269);
            this.checkBoxPregunta3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxPregunta3.Name = "checkBoxPregunta3";
            this.checkBoxPregunta3.Size = new System.Drawing.Size(250, 29);
            this.checkBoxPregunta3.TabIndex = 2;
            this.checkBoxPregunta3.Text = "Cuándo se jugó la partida?";
            this.checkBoxPregunta3.UseVisualStyleBackColor = true;
            this.checkBoxPregunta3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // textBoxIdPartida
            // 
            this.textBoxIdPartida.Location = new System.Drawing.Point(34, 54);
            this.textBoxIdPartida.Name = "textBoxIdPartida";
            this.textBoxIdPartida.Size = new System.Drawing.Size(148, 31);
            this.textBoxIdPartida.TabIndex = 3;
            // 
            // labelIdPartida
            // 
            this.labelIdPartida.AutoSize = true;
            this.labelIdPartida.Location = new System.Drawing.Point(34, 26);
            this.labelIdPartida.Name = "labelIdPartida";
            this.labelIdPartida.Size = new System.Drawing.Size(89, 25);
            this.labelIdPartida.TabIndex = 4;
            this.labelIdPartida.Text = "ID Partida";
            // 
            // buttonSeleccionar
            // 
            this.buttonSeleccionar.Location = new System.Drawing.Point(296, 327);
            this.buttonSeleccionar.Name = "buttonSeleccionar";
            this.buttonSeleccionar.Size = new System.Drawing.Size(141, 35);
            this.buttonSeleccionar.TabIndex = 5;
            this.buttonSeleccionar.Text = "Seleccionar";
            this.buttonSeleccionar.UseVisualStyleBackColor = true;
            this.buttonSeleccionar.Click += new System.EventHandler(this.buttonSeleccionar_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSeleccionar);
            this.Controls.Add(this.labelIdPartida);
            this.Controls.Add(this.textBoxIdPartida);
            this.Controls.Add(this.checkBoxPregunta3);
            this.Controls.Add(this.checkBoxPregunta2);
            this.Controls.Add(this.checkBoxPregunta1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox checkBoxPregunta1;
        private CheckBox checkBoxPregunta2;
        private CheckBox checkBoxPregunta3;
        private TextBox textBoxIdPartida;
        private Label labelIdPartida;
        private Button buttonSeleccionar;
    }
}