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
    public partial class Form2 : Form
    {
        Socket server;
        public Form2(Socket server)
        {
            InitializeComponent();
            this.server = server;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonSeleccionar_Click(object sender, EventArgs e)
        {
            string mensajeID = textBoxIdPartida.Text;
            string mensajeSeleccion;
            if (mensajeID == "")
                MessageBox.Show("No puedes dejar en blanco el ID");
            else
            {
                //Enviamos la peticion
                if ((checkBoxPregunta1.Checked) && (checkBoxPregunta2.Checked == false) && (checkBoxPregunta3.Checked == false))
                {
                    mensajeSeleccion = "Duracion";

                }
                else if ((checkBoxPregunta2.Checked) && (checkBoxPregunta1.Checked == false) && (checkBoxPregunta3.Checked == false))
                {
                    mensajeSeleccion = "Ganador";
                }
                else if ((checkBoxPregunta3.Checked) && (checkBoxPregunta1.Checked == false) && (checkBoxPregunta2.Checked == false))
                {
                    mensajeSeleccion = "Cuando";
                }
                else if ((checkBoxPregunta1.Checked) && (checkBoxPregunta2.Checked) && (checkBoxPregunta3.Checked == false))
                {
                    mensajeSeleccion = "DuracionGanador";

                }
                else if ((checkBoxPregunta1.Checked) && (checkBoxPregunta3.Checked) && (checkBoxPregunta2.Checked == false))
                {
                    mensajeSeleccion = "DuracionCuando";

                }
                else if ((checkBoxPregunta2.Checked) && (checkBoxPregunta3.Checked) && (checkBoxPregunta1.Checked == false))
                {
                    mensajeSeleccion = "GanadorCuando";

                }
                else if ((checkBoxPregunta1.Checked) && (checkBoxPregunta2.Checked) && (checkBoxPregunta3.Checked))
                {
                    mensajeSeleccion = "DuracionGanadorCuando";

                }
                else
                {
                    mensajeSeleccion = "Error";
                }

                // Codigo 8 POR EJEMPLO
                byte[] msg = System.Text.Encoding.ASCII.GetBytes("8/" + mensajeID + "/" + mensajeSeleccion);
                server.Send(msg);

                //Recibimos Respuesta
                string mensajeRespuesta; //CREAMOS RESPUESTA
                byte[] msgAnswer = new byte[80];
                server.Receive(msgAnswer);
                mensajeRespuesta = Encoding.ASCII.GetString(msgAnswer);
                string[] parts = mensajeRespuesta.Split(new char[] { '/', '\0' }, StringSplitOptions.RemoveEmptyEntries);
                string Primeraparte;
                string Segundaparte;
                string Terceraparte;
                string Cuartaparte;

                //Recibimos Respuesta
                if (parts.Length.ToString() == "2")
                {
                    Primeraparte = parts[0];
                    Segundaparte = parts[1];
                    Terceraparte = "0";
                    Cuartaparte = "0";
                }
                else if (parts.Length.ToString() == "3") //2 elementos
                {
                    Primeraparte = parts[0];
                    Segundaparte = parts[1];
                    Terceraparte = parts[2];
                    Cuartaparte = "0";
                }
                else if (parts.Length.ToString() == "4") //3 elementos
                {
                    Primeraparte = parts[0];
                    Segundaparte = parts[1];
                    Terceraparte = parts[2];
                    Cuartaparte = parts[3];
                }
                else //Error, 0 elementos solo si no seleccion de Cuando|Duracion|Ganador o IDError
                {
                    if (parts[0] == "IDError")
                    {
                        Primeraparte = "IDError";
                        Segundaparte = "0";
                        Terceraparte = "0";
                        Cuartaparte = "0";
                    }
                    else
                    {
                        Primeraparte = "Error";
                        Segundaparte = "0";
                        Terceraparte = "0";
                        Cuartaparte = "0";
                    }

                }


                //POR EJEMPLO. QUE RECIBA: IDError/Error --> Tipo: IDError Resultado: Error
                if (Primeraparte == "IDError")
                {
                    MessageBox.Show("El ID no esta en la base de datos");
                }
                else if (Primeraparte == "Error")
                {
                    MessageBox.Show("No se ha seleccionado ninguna casilla");
                }
                //POR EJEMPLO. QUE RECIBA: Duracion/76 --> Tipo: Duracion Resultado: 76
                else if (Primeraparte == "Duracion")
                {
                    MessageBox.Show("La duracion de la partida es de: " + Segundaparte + "minutos.");
                }
                //POR EJEMPLO. QUE RECIBA: Ganador/Carlos --> Tipo: Ganador Resultado: Carlos
                //                         Ganador/Maria  --> Tipo: Ganador Resultado: Maria
                else if (Primeraparte == "Ganador")
                {
                    MessageBox.Show("El ganador de la partida es: " + Segundaparte);
                }
                //POR EJEMPLO. QUE RECIBA: Cuando/18-03-2020 --> Tipo: Cuando Resultado: 18-03-2020
                else if (Primeraparte == "Cuando")
                {
                    MessageBox.Show("La partida se jugó la fecha: " + Segundaparte);
                }
                else if (Primeraparte == "DuracionGanador")
                {
                    MessageBox.Show("La partida duró " + Segundaparte + "minutos y la ganó " + Terceraparte);
                }
                else if (Primeraparte == "DuracionCuando")
                {
                    MessageBox.Show("La partida duró " + Segundaparte + " y se jugó la fecha: " + Terceraparte);
                }
                else if (Primeraparte == "GanadorCuando")
                {
                    MessageBox.Show("La partida la ganó " + Segundaparte + " y se jugó en la fecha: " + Terceraparte);
                }
                else if (Primeraparte == "DuracionGanadorCuando")
                {
                    MessageBox.Show("La partida duró " + Segundaparte + " y la ganó " + Terceraparte + " en la fecha: " + Cuartaparte);
                }
            }

        }
        public static Form2 Instance2 { get; private set; }
        private void Form2_Load(object sender, EventArgs e)
        {

            Instance2 = this;
            /*if (Form1.Instance1 != null)
            {
                    Form1.Instance1.Close();
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(server);
            form3.Show();
        }

        private void DesconectarUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}
