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
            IPAddress direc = IPAddress.Parse("192.168.56.102"); //IPAddress de conexión
            IPEndPoint ipep = new IPEndPoint(direc, 41028); //Asociamos dirección IP al puerto.

            //Creamos Socket llamado server
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep); //Intento de conexion al Socket
                this.BackColor = Color.Green; //Si conexion satisfactoria cambio fondo a verde.
                MessageBox.Show("Conectado");
            }
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
                //Enviamos el Usuario y Contraseña
                string mensajeUsuario = textBoxUsuario.Text;
                string mensajeContraseña = textBoxContraseña.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes("1/" + mensajeUsuario + "/" + mensajeContraseña);
                server.Send(msg);

                //Recibimos Respuesta
                string mensajeRespuesta; //CREAMOS RESPUESTA
                byte[] msgAnswer = new byte[80];
                server.Receive(msgAnswer);
                mensajeRespuesta = Encoding.ASCII.GetString(msgAnswer).Split('\0')[0];

                if (mensajeRespuesta == "Correcto")
                {
                    MessageBox.Show("Se ha iniciado sesion correctamente");
                    Form2 form2 = new Form2(server);
                    form2.Show();
                    this.BackColor = Color.Gray;
                    //this.Close(); QUIERO CERRAR FORM1 PERO NO FORM2
                    //Ir a forms2 donde se puedan hacer las consultas.
                }
                else if (mensajeRespuesta == "Incorrecto")
                {
                    MessageBox.Show("El usuario no esta registrado");
                }
                else if (mensajeRespuesta == "Semicorrecto")
                {
                    MessageBox.Show("Contraseña incorrecta, comprueba de nuevo.");
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
                //Enviamos el Usuario y Contraseña
                string mensajeUsuario = textBoxUsuario.Text;
                string mensajeContraseña = textBoxContraseña.Text;
                if ((mensajeUsuario == "") || (mensajeContraseña == ""))
                    MessageBox.Show("No puedes dejar en blanco el Usuario o Contraseña");
                else
                {
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("2/" + mensajeUsuario + "/" + mensajeContraseña);
                    server.Send(msg);

                    //Recibimos Respuesta
                    string mensajeRespuesta; //CREAMOS RESPUESTA
                    byte[] msgAnswer = new byte[80];
                    server.Receive(msgAnswer);
                    mensajeRespuesta = Encoding.ASCII.GetString(msgAnswer).Split('\0')[0];

                    if (mensajeRespuesta == "Registrado")
                    {
                        MessageBox.Show("Se ha registrado correctamente");
                    }
                    else if (mensajeRespuesta == "ErrorUsuario")
                    {
                        MessageBox.Show("El usuario ya esta registrado");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debes conectarte primero");
            }
        }

        private void buttonDesconectarse_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.Green)
            {
                string mensaje = "0/";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                this.BackColor = Color.Red;
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            else
                MessageBox.Show("Para desconectarte primero debes estar conectado");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }
    }
}