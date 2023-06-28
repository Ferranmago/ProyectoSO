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
        int servidorsocket = 41078;

        string mensajeUsuario;
        string mensajeContraseña;

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
                int numconectados;

                switch (codigo)
                {
                    case 0:
                        string Primeraparte = mensajeRespuesta[1];

                        numconectados = Convert.ToInt32(mensajeRespuesta[2]);

                        //Actualizar DGV1
                        for (int i = 1; i <= numconectados; i++)
                        {
                            string nombretabla = mensajeRespuesta[i * 2 + 1];
                            DGV1.Rows[i - 1].Cells[0].Value = nombretabla; // Nombre de usuario
                            string sockettabla = mensajeRespuesta[i * 2 + 2];
                            DGV1.Rows[i - 1].Cells[1].Value = sockettabla; // Socket de usuario
                        }

                        for (int num = numconectados; num <= 10; num++)
                        {
                            DGV1.Rows[num].Cells[0].Value = ""; // Establecer el valor de la celda en la columna 1
                            DGV1.Rows[num].Cells[1].Value = ""; // Establecer el valor de la celda en la columna 2
                        }


                        server.Shutdown(SocketShutdown.Both);
                        server.Close();
                        finalizarHilo = false;

                        break;

                    case 1: //Respuesta a Iniciar Sesion

                        Primeraparte = mensajeRespuesta[1]; //esto pasa a ser 1 por el tema Case
                        if (Primeraparte == "Correcto")
                        {

                            //TIENE QUE BUSCAR EL USUARIO QUE HEMOS MANDADO EN LA NOTIFICACION Y SACAR EL SOCKET DE ESE
                            MessageBox.Show("Se ha iniciado sesion correctamente");
                            this.BackColor = Color.FromArgb(172,216,230);

                            numconectados = Convert.ToInt32(mensajeRespuesta[2]);

                            //Buscar el usuario y socket del Cliente
                            for (int i = 0; i < numconectados; i++)
                            {
                                if (mensajeUsuario == mensajeRespuesta[i * 2 + 1])
                                {
                                    SocketServidor = Convert.ToInt32(mensajeRespuesta[i * 2 + 2]);
                                }
                            }


                            //Actualizar DGV1
                            for (int i = 1; i <= numconectados; i++)
                            {
                                string nombretabla = mensajeRespuesta[i * 2 + 1];
                                DGV1.Rows[i - 1].Cells[0].Value = nombretabla; // Nombre de usuario
                                string sockettabla = mensajeRespuesta[i * 2 + 2];
                                DGV1.Rows[i - 1].Cells[1].Value = sockettabla; // Socket de usuario
                            }

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
                        break;

                    case 5: //Respuesta a Iniciar Sesion

                        Primeraparte = mensajeRespuesta[1]; //esto pasa a ser 1 por el tema Case
                        string dineroganado = mensajeRespuesta[2];

                        //Actualizar DGV2
                        string[] rowData = { Primeraparte, dineroganado };
                        // Agrega la nueva fila al control DataGridView
                        DGV2.Rows.Add(rowData);

                        break;
                }
                finalizarHilo = true;
                //atender.Abort();
            }
        }
        private void buttonIniciarSesion_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.FromArgb(172, 216, 230))
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
                    mensajeUsuario = textBoxUsuario.Text;
                    mensajeContraseña = textBoxContraseña.Text;

                    if ((mensajeUsuario == "") || (mensajeContraseña == ""))
                    {
                        MessageBox.Show("No puedes dejar en blanco el Usuario o Contraseña");
                    }
                    else
                    {
                        //CODIGO 1
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("1/" + mensajeUsuario + "/" + mensajeContraseña);
                        server.Send(msg);
                    }
                }
                catch (SocketException)
                {
                    MessageBox.Show("Conexion Fallida");
                    return;
                }
            }
            else
            {
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
            if (this.BackColor == Color.FromArgb(172, 216, 230))
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
            else
            {
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
            if ((this.BackColor == Color.Green) || (this.BackColor == Color.FromArgb(172, 216, 230)))
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes("0/" + SocketServidor);
                server.Send(msg);
                this.BackColor = Color.Red;

            }
            else
                MessageBox.Show("Para desconectarte primero debes estar conectado");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Establecer el tamaño del formulario
            this.Size = new Size(1660, 930); // Aquí puedes ajustar el tamaño deseado
            pictureBox1.Size = new Size(527, 500);
            label3.Location = new Point(1153, 12);
            DGV1.Size = new Size(370, 410);
            DGV2.Size = new Size(370, 200);
            


            //CREAMOS DATAGRIDVIEW
            DGV1.Columns.Add("Usuario", "Usuario");
            DGV1.Columns.Add("Socket", "Socket");
            // Agregar las filas al DataGridView
            for (int i = 0; i < 10; i++)
            {
                DGV1.Rows.Add(); // Agregar una nueva fila
                DGV1.Rows[i].Cells[0].Value = ""; // Establecer el valor de la celda en la columna 1
                DGV1.Rows[i].Cells[1].Value = ""; // Establecer el valor de la celda en la columna 2
            }

            DGV2.Columns.Add("Usuario", "Usuario");
            DGV2.Columns.Add("Premio", "Premio");

        }

        bool apuestaGanadacolor; //Para determinar si es negro o rojo
        bool apuestaParidad; //Para determinar si es par o impar
        bool apuestaMayor; //Para determinar si es 1-18 o 19-36

        private void btnNegro_Click(object sender, EventArgs e)
        {
            apuestaGanadacolor = false;
            btnRojo.Enabled = false;
        }

        private void btnRojo_Click(object sender, EventArgs e)
        {
            apuestaGanadacolor = true;
            btnNegro.Enabled = false;
        }
        private void btnPar_Click(object sender, EventArgs e)
        {
            apuestaParidad = true;
            btnImpar.Enabled = false;
        }
        private void btnImpar_Click(object sender, EventArgs e)
        {
            apuestaParidad = false;
            btnPar.Enabled = false;
        }
        private void btn118_Click(object sender, EventArgs e)
        {
            apuestaMayor = false;
            btn1936.Enabled = false;
        }
        private void btn1936_Click(object sender, EventArgs e)
        {
            apuestaMayor = true;
            btn118.Enabled = false;
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string numeroApostadolectura = textBoxApuesta.Text;
            
            string dinerolectura = textBoxDinero.Text;


            if ((numeroApostadolectura == "") || (dinerolectura == ""))
                MessageBox.Show("Introduzca una cantidad a apostar y su apuesta");
            else if ((Convert.ToInt32(numeroApostadolectura) < 0) || (Convert.ToInt32(numeroApostadolectura)) > 36)
                MessageBox.Show("Introduzca una apuesta válida. Debe ser un número entre el 0 y el 36");
            else if (Convert.ToInt32(dinerolectura) < 0)
                MessageBox.Show("Introduzca una cantidad de dinero válida. Debe ser un número superior a 0€");
            else
            {
                int numeroApostado = Convert.ToInt32(numeroApostadolectura);
                float dinero = float.Parse(dinerolectura);

                Random random = new Random();
                int numeroAleatorio = random.Next(0, 37); // Genera un número aleatorio entre 0 y 36, ambos incluidos
                label2.Text = numeroAleatorio.ToString();


                float dinerototal = 0;
                float dinerocolor = 0;
                float dineronumero = 0;
                float dineropar = 0;
                float dineromayor = 0;

                if (numeroAleatorio % 2 != 0 && numeroAleatorio != 0) // Es impar y cambio el fondo a color rojo
                {
                    label2.BackColor = Color.FromArgb(200, 40, 40);
                    label2.ForeColor = Color.White;

                    if (apuestaGanadacolor == true)
                        dinerocolor = dinero * 2;
                    else
                        dinerocolor = 0;

                    if (apuestaParidad == false)
                        dineropar = dinero * 2;
                    else
                        dineropar = 0;

                    if (apuestaMayor == false && numeroAleatorio > 0 && numeroAleatorio <= 18)
                        dineromayor = dinero * 2;
                    else if (apuestaMayor == true && numeroAleatorio > 18 && numeroAleatorio <= 36)
                        dineromayor = dinero * 2;
                    else
                        dineromayor = 0;

                }
                else if (numeroAleatorio % 2 == 0 && numeroAleatorio != 0) // Es par y cambio el fondo a color negro
                {
                    label2.BackColor = Color.Black;
                    label2.ForeColor = Color.White;

                    if (apuestaGanadacolor == false)
                        dinerocolor = dinero * 2;
                    else
                        dinerocolor = 0;


                    if (apuestaParidad == true)
                        dineropar = dinero * 2;
                    else
                        dineropar = 0;

                    if (apuestaMayor == false && numeroAleatorio > 0 && numeroAleatorio <= 18)
                        dineromayor = dinero * 2;
                    else if (apuestaMayor == true && numeroAleatorio > 18 && numeroAleatorio <= 36)
                        dineromayor = dinero * 2;
                    else
                        dineromayor = 0;


                }
                else if (numeroAleatorio == 0) //Es el numero cero
                {
                    label2.BackColor = Color.Green;
                    label2.ForeColor = Color.Black;
                    dinerocolor = 0;
                }

                if (numeroApostado == numeroAleatorio)
                {
                    dineronumero = dinero * 36;
                }

                dinerototal = dinerocolor + dineromayor + dineronumero + dineropar;

                lblSaldo.Text = dinerototal.ToString();

                //CODIGO 5
                byte[] msg = System.Text.Encoding.ASCII.GetBytes("5/" + mensajeUsuario + "/" + dinerototal);
                server.Send(msg);

                if (dinerototal > 0)
                    MessageBox.Show("¡Enhorabuena! Has ganado " + Convert.ToString(dinerototal) + "€");
                else
                    MessageBox.Show("¡Vaya! Has perdido todo el dinero");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            textBoxDinero.Text = string.Empty;
            textBoxApuesta.Text = string.Empty;
            btn118.Enabled = true;
            btn1936.Enabled = true;
            btnImpar.Enabled = true;
            btnPar.Enabled = true;
            btnNegro.Enabled = true;
            btnRojo.Enabled = true;
            label2.Text = string.Empty;
            label2.BackColor = Color.White;
        }
    }
}