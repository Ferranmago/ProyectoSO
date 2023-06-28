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
using System.Threading;

namespace InterfazJuego
{
    public partial class Form3 : Form
    {
        Socket server;
        DataGridViewRow row = new DataGridViewRow();
        string[] mensajeRespuesta;
        //Thread atender;
        public Form3(Socket server, string[] mensajeRespuesta)
        {
            InitializeComponent();
            this.server = server;
            this.mensajeRespuesta = mensajeRespuesta;
            //CheckForIllegalCrossThreadCalls = false; //MAS ADELANTE LO QUITAMOS

        }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Refresh(string mensajeRespuesta)
        {
            int Primeraparte = Convert.ToInt32(mensajeRespuesta[1]);
            DGV1.Columns.Add("Usuario", "Usuario");
            DGV1.Columns.Add("Socket", "Socket");
            for (int i = 0; i < Primeraparte; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell userCell = new DataGridViewTextBoxCell();
                userCell.Value = mensajeRespuesta[i * 2 + 2]; // Nombre de usuario
                row.Cells.Add(userCell);
                DataGridViewTextBoxCell socketCell = new DataGridViewTextBoxCell();
                socketCell.Value = mensajeRespuesta[i * 2 + 3]; // Socket
                row.Cells.Add(socketCell);
                DGV1.Rows.Add(row);
            }
        } 
        private void Form3_Load_1(object sender, EventArgs e)
        {
            // Codigo 3
            //byte[] msg = System.Text.Encoding.ASCII.GetBytes("3/"); ya lo hemos hecho con Form2
            //server.Send(msg);


            /*//Recibimos Respuesta
            string mensajeRespuesta; //CREAMOS RESPUESTA
            byte[] msgAnswer = new byte[80];
            server.Receive(msgAnswer);
            mensajeRespuesta = Encoding.ASCII.GetString(msgAnswer);
            
            string[] parts = mensajeRespuesta.Split(new char[] { '/', '\0' }, StringSplitOptions.RemoveEmptyEntries);
            */
            int Primeraparte = Convert.ToInt32(mensajeRespuesta[1]);
            DGV1.Columns.Add("Usuario", "Usuario");
            DGV1.Columns.Add("Socket", "Socket");
            for (int i = 0; i < Primeraparte; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell userCell = new DataGridViewTextBoxCell();
                userCell.Value = mensajeRespuesta[i * 2 + 2]; // Nombre de usuario
                row.Cells.Add(userCell);
                DataGridViewTextBoxCell socketCell = new DataGridViewTextBoxCell();
                socketCell.Value = mensajeRespuesta[i * 2 + 3]; // Socket
                row.Cells.Add(socketCell);
                DGV1.Rows.Add(row);
            }
        }
    }
}
