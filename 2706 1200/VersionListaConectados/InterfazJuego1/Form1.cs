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
using System.Threading;

namespace InterfazJuego
{
    public partial class Form1 : Form
    {
        Socket server;
        int SocketServidor;
        Thread atender;
        string[] mensajeRespuesta;
        DataGridViewRow row = new DataGridViewRow();
        private static bool finalizarHilo = true;
        int servidorsocket = 41045;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Gray;
            CheckForIllegalCrossThreadCalls = false; //MAS ADELANTE LO QUITAMOS
        }
        private void AtenderServidor()
        {
            finalizarHilo = true;
            //while (true)
            while (finalizarHilo == true)
            {
                //Recibimos Respuesta
                byte[] msgAnswer = new byte[80];
                server.Receive(msgAnswer);
                mensajeRespuesta = Encoding.ASCII.GetString(msgAnswer).Split(new char[] { '/', '\0' }, StringSplitOptions.RemoveEmptyEntries);
                int codigo = Convert.ToInt32(mensajeRespuesta[0]);

                switch (codigo)
                {
                    case 0:
                        string Primeraparte = mensajeRespuesta[1];
                        server.Shutdown(SocketShutdown.Both);
                        server.Close();
                        finalizarHilo = false;
                        break;
                    case 1: //Respuesta a Iniciar Sesion

                        Primeraparte = mensajeRespuesta[1]; //esto pasa a ser 1 por el tema Case
                        if (Primeraparte == "Correcto")
                        {
                            SocketServidor = Convert.ToInt32(mensajeRespuesta[2]);
                            MessageBox.Show("Se ha iniciado sesion correctamente");
                            this.BackColor = Color.Blue;
                            break;

                        }
                        else if (Primeraparte == "Incorrecto")
                        {
                            MessageBox.Show("El usuario no esta registrado");
                            break;
                        }
                        else if (Primeraparte == "Semicorrecto")
                        {
                            MessageBox.Show("Contraseña incorrecta, comprueba de nuevo.");
                            break;
                        }
                        break;

                    case 2: //Respuesta al Registro

                        if (mensajeRespuesta[1] == "Registrado")
                        {
                            MessageBox.Show("Se ha registrado correctamente");
                        }
                        else if (mensajeRespuesta[1] == "ErrorUsuario")
                        {
                            MessageBox.Show("El usuario ya esta registrado");
                        }
                        break;

                    case 3:
                        //this.Invoke(new Action(ShowForm2));
                        
                        //Recibimos Respuesta a lista conectados
                        int numconectados = Convert.ToInt32(mensajeRespuesta[2]);
                        DGV1.Columns.Add("Usuario", "Usuario");
                        DGV1.Columns.Add("Socket", "Socket");
                        for (int i = 0; i < numconectados; i++)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            DataGridViewTextBoxCell userCell = new DataGridViewTextBoxCell();
                            userCell.Value = mensajeRespuesta[i * 2 + 1]; // Nombre de usuario
                            row.Cells.Add(userCell);
                            DataGridViewTextBoxCell socketCell = new DataGridViewTextBoxCell();
                            socketCell.Value = mensajeRespuesta[i * 2 + 2]; // Socket
                            row.Cells.Add(socketCell);
                            DGV1.Rows.Add(row);
                        }
                        break;
                }
            }
            finalizarHilo = true;
            //atender.Abort();
        }

        private void buttonIniciarSesion_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.Blue)
            {
                MessageBox.Show("Ya estas con la sesión iniciada");
            }
            else if ((this.BackColor == Color.Gray) || (this.BackColor == Color.Red))
            {
                try
                {
                    IPAddress direc = IPAddress.Parse("192.168.56.102"); //IPAddress de conexión
                    IPEndPoint ipep = new IPEndPoint(direc, servidorsocket); //Asociamos dirección IP al puerto.

                    //Creamos Socket llamado server
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    server.Connect(ipep); //Intento de conexion al Socket
                    MessageBox.Show("Conectado");
                    this.BackColor = Color.Green;

                    finalizarHilo = true;
                    ThreadStart ts = delegate { AtenderServidor(); };
                    atender = new Thread(ts);
                    atender.Start();
                    

                    //Enviamos el Usuario y Contraseña
                    string mensajeUsuario = textBoxUsuario.Text;
                    string mensajeContraseña = textBoxContraseña.Text;
                    if ((mensajeUsuario == "") || (mensajeContraseña == ""))
                    {
                        MessageBox.Show("No puedes dejar en blanco el Usuario o Contraseña");
                    }
                    else  //Ha iniciado sesión correctamente. Se envía un mensaje con el formato "1/usuario/contraseña al servidor.
                    {
                        //CODIGO 1
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("1/" + mensajeUsuario + "/" + mensajeContraseña);
                        server.Send(msg);
                        }
                }
                catch(SocketException)
                {
                    MessageBox.Show("Conexion Fallida");
                    return;
                }
            }
            else { 
                //Enviamos el Usuario y Contraseña
                string mensajeUsuario = textBoxUsuario.Text;
                string mensajeContraseña = textBoxContraseña.Text;
                if ((mensajeUsuario == "") || (mensajeContraseña == ""))
                {
                    MessageBox.Show("No puedes dejar en blanco el Usuario o Contraseña");
                }
                else
                {
                    try
                    {
                        //CODIGO 1
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("1/" + mensajeUsuario + "/" + mensajeContraseña);
                        server.Send(msg);
                        }
                    catch (SocketException)
                    {
                        MessageBox.Show("Error al conectar");
                        return;
                    }
                }
            }
        }

        private void buttonRegistrarse_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.Blue)
            {
                MessageBox.Show("Ya estas con la sesión iniciada");
            }
            else if ((this.BackColor == Color.Gray) || (this.BackColor == Color.Red))
            {
                try
                {
                    IPAddress direc = IPAddress.Parse("192.168.56.102"); //IPAddress de conexión
                    IPEndPoint ipep = new IPEndPoint(direc, servidorsocket); //Asociamos dirección IP al puerto.

                    //Creamos Socket llamado server
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    server.Connect(ipep); //Intento de conexion al Socket
                    MessageBox.Show("Conectado");
                    this.BackColor = Color.Green;

                    finalizarHilo = true;
                    ThreadStart ts = delegate { AtenderServidor(); };
                    atender = new Thread(ts);
                    atender.Start();

                    //Enviamos el Usuario y Contraseña
                    string mensajeUsuario = textBoxUsuario.Text;
                    string mensajeContraseña = textBoxContraseña.Text;
                    if ((mensajeUsuario == "") || (mensajeContraseña == ""))
                        MessageBox.Show("No puedes dejar en blanco el Usuario o Contraseña");
                    else
                    {
                       
                        //CODIGO 2
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("2/" + mensajeUsuario + "/" + mensajeContraseña);
                        server.Send(msg);
                       }
                }
                catch (SocketException)
                {
                    MessageBox.Show("Conexion Fallida");
                    return;
                }
            }
            else { 
                //Enviamos el Usuario y Contraseña
                string mensajeUsuario = textBoxUsuario.Text;
                string mensajeContraseña = textBoxContraseña.Text;
                if ((mensajeUsuario == "") || (mensajeContraseña == ""))
                    MessageBox.Show("No puedes dejar en blanco el Usuario o Contraseña");
                else
                {
                    //CODIGO 2
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("2/" + mensajeUsuario + "/" + mensajeContraseña);
                    server.Send(msg);
                    }
            }
        }

        private void buttonDesconectarse_Click(object sender, EventArgs e)
        {
            if ((this.BackColor == Color.Green) || (this.BackColor == Color.Blue))
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes("0/" + SocketServidor);
                server.Send(msg);
                this.BackColor = Color.Red;
                //server.Shutdown(SocketShutdown.Both);
                //server.Close();
            }
            else
                MessageBox.Show("Para desconectarte primero debes estar conectado");
        }
        //public static Form1 Instance1 { get; private set; }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Instance1 = this;
        }
    }
}