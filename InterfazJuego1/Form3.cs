using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using InterfazJuego;

namespace InterfazJuego
{
    public partial class Form3 : Form
    {
        Socket server;
        DataGridViewRow row = new DataGridViewRow();
        public Form3(Socket server)
        {
            InitializeComponent();
            this.server = server;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load_1(object sender, EventArgs e)
        {
            // Codigo 3
            byte[] msg = System.Text.Encoding.ASCII.GetBytes("3/");
            server.Send(msg);


            //Recibimos Respuesta
            string mensajeRespuesta; //CREAMOS RESPUESTA
            byte[] msgAnswer = new byte[80];
            server.Receive(msgAnswer);
            mensajeRespuesta = Encoding.ASCII.GetString(msgAnswer);
            string[] parts = mensajeRespuesta.Split(new char[] { '/', '\0' }, StringSplitOptions.RemoveEmptyEntries);
            int Primeraparte = Convert.ToInt32(parts[0]);
            DGV1.Columns.Add("Usuario", "Usuario");
            DGV1.Columns.Add("Socket", "Socket");
            for (int i = 0; i < Primeraparte; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell userCell = new DataGridViewTextBoxCell();
                userCell.Value = parts[i * 2 + 1]; // Nombre de usuario
                row.Cells.Add(userCell);
                DataGridViewTextBoxCell socketCell = new DataGridViewTextBoxCell();
                socketCell.Value = parts[i * 2 + 2]; // Socket
                row.Cells.Add(socketCell);
                DGV1.Rows.Add(row);
            }
        }
    }
}
