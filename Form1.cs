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



namespace InterfazJuego
{
    public partial class Form1 : Form
    {
        Socket server;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Gray;
            
        }
        private void buttonConectarse_Click(object sender, EventArgs e)
        {

            //IPAddress direc = IPAddress.Parse("10.192.192.216"); //IPAddress de conexión
            IPAddress direc = IPAddress.Parse("192.168.56.101"); //IPAddress de conexión
            IPEndPoint ipep = new IPEndPoint(direc, 35674); //Asociamos dirección IP al puerto.

            //Creamos Socket llamado server
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep); //Intento de conexion al Socket
                this.BackColor = Color.Green; //Si conexion satisfactoria cambio fondo a verde.
            }
            //catch (SocketException ex)
            catch (SocketException)
            {
                MessageBox.Show("Conexion Fallida");
                return;
            }
        }

        private void buttonIniciarSesion_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.Green)
            {
                //Enviamos el Usuario
                string mensajeUsuario = textBoxUsuario.Text;
                string mensajeContraseña = textBoxContraseña.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes("1/" + mensajeUsuario+ "/" + mensajeContraseña);
                server.Send(msg);

                //Enviamos la Contraseña
                
                //byte[] msgPassword = System.Text.Encoding.ASCII.GetBytes();
                //server.Send(msgPassword);

                //Recibimos Respuesta
                string mensajeRespuesta; //CREAMOS RESPUESTA
                byte[] msgAnswer = new byte[80];
                server.Receive(msgAnswer);
                mensajeRespuesta = Encoding.ASCII.GetString(msgAnswer).Split('\0')[0]; 
                
                if (mensajeRespuesta == "Correcto")
                {
                    MessageBox.Show("Se ha iniciado sesion correctamente");
                    //Ir a forms2 donde se puedan hacer las consultas.
                }
                /*else if(mensajeRespuesta == "USUARIO Incorrecto")
                {
                    COMPRUEBA TU USUARIO O REGISTRATE PRIMERO
                }
                */
          
                else //EN CASO DE QUE USUARIO SI PERO CONTRASEÑA NO, VUELVE A INTRODUCIR CONTRASEÑA
                {
                    MessageBox.Show("Contraseña incorrecta, comprueba de nuevo o debes registrarte");
                }
            }
            else
            {
                MessageBox.Show("Debes conectarte primero");
            }
        }

        private void buttonRegistrarse_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.Green)
            {

            }
            else
            {
                MessageBox.Show("Debes conectarte primero");
            }
        }

        private void buttonDesconectarse_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Red;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }
    }
}