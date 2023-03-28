#include <stdio.h>
#include <unistd.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <mysql.h>

int main(int argc, char *argv[]) 
{
	
	//Necesitamos unas variables y estructuras que podemos usar gracias a los includes
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	//PeticiÃ³n y respuestas son vectores de caracteres
	char peticion[512];
	char respuesta[512];
	//Creamos un socket (lo llamamos socket de escucha) , esperar a una conexiÃ³n, especificando el tipo de conexiÃ³n que vamos a crear
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
	{
		printf("Error creando el socket");
		exit(1);
	}
	//Inicializamos una estructura de datos que va a necesitar indicando que tipo de protocolos va a usar
	memset(&serv_adr,0,sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	//Indicamos la direcciÃ³n de IP donde va a escuchar (la que tenga asignada)
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	//Especificamos el puerto en el que va a escuchar (en nuestro caso en el 9050)
	serv_adr.sin_port = htons(41028); //-------------------------------------------------- HABRA QUE CAMBIARLOOOOOOOOOOOOOOOO --------------------------
/*	
	int reuse = 1;
	if (setsockopt(sock_listen, SOL_SOCKET, SO_REUSEADDR, &reuse, sizeof(int)) == -1) {
		printf("Error en setsockopt");
		exit(1);
	}
*/	
	//Asociamos al socket de escucha la estructura de datos que hemos creado
	if(bind(sock_listen,(struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
	{
		printf("Error en el bind");
		exit(1);
	}
	//La cola de espera de conexiones (peticiones) a ser atendidas no puede ser mayor de 3     ------------------------- HABRA QUE CAMBIARLOOOOOOOOOOOOOOOO --------------------------
	if(listen(sock_listen,3)<0)
	{
		printf("Error en el listen");
		exit(1);
	}
	
	//CONECTAMOS MYSQL
	MYSQL *conn;
	int err;
	// Estructura especial para almacenar resultados de consultas
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	//Creamos una conexion al servidor MYSQL
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexi\uffc3\uffb3n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "Juego", 0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexi\uffc3\uffb3n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//Hacemos un bucle infinito
		
	int i;
	for(;;){
		
		printf("Escuchando\n");
		//Esperamos a una conexiÃ³n. Cuando se conecte crearÃ¡ un socket distinto al de escucha (sock_conn) que se va a comunicar con el cliente
		sock_conn = accept(sock_listen, NULL, NULL);
		printf("Conexion recibida");
		
		//Bucle de atencion al cliente
		int terminar = 0;
		while (terminar == 0)
		{
			//Recogemos la peticiÃ³n. Hacemos una operaciÃ³n de lectura a travÃ©s del socker de conexiÃ³n y la peticiÃ³n la guarda en el vector de caracteres peticiÃ³n
			ret=read(sock_conn,peticion,sizeof(peticion));
			//Ver como va progresando la ejecuciÃ³n del servidor
			printf("Recibido\n");
			//AÃ±adimos un fin de linea, nos ayudarÃ¡ a procesar el mensaje
			peticion[ret]='\0';
			printf("Peticion: %s\n", peticion);
			//Cortamos por donde haya una barra
			char *p = strtok(peticion, "/"); //Nos apunta al trozo que va desde el inicio hasta la barra
			int codigo = atoi(p);
			printf("Codigo: %d\n", codigo);
			
			if (codigo == 0) 
			{
				terminar = 1;
				sprintf(respuesta, "Desconectar");
				
			}
			else if(codigo==1) //Peticion 1  //"MENSAJE ESPERADO:   1/usuari/contrasena --> usuari: contrasena:
			{
				
				p = strtok(NULL, "/");
				char Usuario[30];
				strcpy(Usuario, p);
				p = strtok(NULL, "/");
				char Password[30];
				strcpy(Password, p);
				int IDJugador;
				char consulta[512];
				
				// Consulta SQL para obtener valores con los datos entrados como Usuario y Password
				err=mysql_query (conn, "SELECT * FROM Jugador");
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//Recogemos el resultado de la consulta. Se trata de una tabla virtual en memoria que es la copia de la tabla real en disco.
				resultado = mysql_store_result (conn);
				// Ahora obtenemos la primera fila que se almacena en una variable de tipo MYSQL_ROW
				row = mysql_fetch_row (resultado);
				// En una fila hay tantas columnas como datos tiene un Jugador. Tres columnas: Usuario(row[0]), Password(row[1]) y IDJugador(row[2]).
				if (row == NULL)
					printf ("No se han obtenido datos en la consulta\n");
				else
				{
					while (row !=NULL) 
					{
						// la columna 3 contiene una palabra que es el identificador y la convertimos a entero 
						IDJugador = atoi (row[2]);
						// las columnas 0 y 1 contienen Usuario y Password
						printf ("Usuario: %s, Password: %s, IDJugador: %d\n", row[0], row[1], IDJugador);
						// obtenemos la siguiente fila
						row = mysql_fetch_row (resultado);
					}
				}
				//TABLA CREADA
				
				// construimos la consulta SQL
				strcpy (consulta,"SELECT username, password FROM Jugador WHERE username = '"); 
				strcat (consulta, Usuario);
				strcat (consulta,"'");
				//hacemos la consulta 
				err=mysql_query (conn, consulta); 
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				else
				{
					//recogemos el resultado de la consulta 
					resultado = mysql_store_result (conn); 
					row = mysql_fetch_row (resultado);
					//printf("%s,%s,%s,%s\n", Usuario, row[0], Password, row[1]); //PARA COMPROVAR SI TENEMOS ERRORES (SI TODO OKEI NO HACE FALTA)
					if (row == NULL)
					{
						printf ("No se han obtenido datos en la consulta\n");
						sprintf(respuesta, "Incorrecto");
						
					}
					else if ((strcmp(row[0], Usuario) == 0) && (strcmp(row[1], Password) == 0)) 
					{
						// El resultado debe ser una matriz con una sola fila y una columna que contiene el Usuario
						printf ("Usuario del Jugador: %s\n", row[0]);
						sprintf(respuesta, "Correcto");
				//		mysql_close (conn);
					}
					else
					{
						printf("Usuario: %s, Password: %s\n", Usuario, Password);
						sprintf(respuesta, "Semicorrecto");
					}
				}
			}
			else if(codigo==2) //Peticion 2  //"MENSAJE ESPERADO:   2/usuari/contrasena --> usuari: contrasena:
			{
				
				p = strtok(NULL, "/");
				char Usuario[30];
				strcpy(Usuario, p);
				p = strtok(NULL, "/");
				char Password[30];
				strcpy(Password, p);
				int IDJugador;
				char strIDJugador[512];
				char consulta[512];
				
				// Consulta SQL para obtener todos los valores de la tabla
				err=mysql_query (conn, "SELECT * FROM Jugador");
				if (err!=0) 
				{
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//Recogemos el resultado de la consulta. Se trata de una tabla virtual en memoria que es la copia de la tabla real en disco.
				resultado = mysql_store_result (conn);
				// Ahora obtenemos la primera fila que se almacena en una variable de tipo MYSQL_ROW
				row = mysql_fetch_row (resultado);
				int count = 0;
				// En una fila hay tantas columnas como datos tiene un Jugador. Tres columnas: Usuario(row[0]), Password(row[1]) y IDJugador(row[2]).
				if (row == NULL)
					printf ("No se han obtenido datos en la consulta\n");
				else
				{
					while (row !=NULL) 
					{
						// la columna 3 contiene una palabra que es el identificador y la convertimos a entero 
						IDJugador = atoi (row[2]);
						// las columnas 0 y 1 contienen Usuario y Password
						printf ("Usuario: %s, Password: %s, IDJugador: %d\n", row[0], row[1], IDJugador);
						// obtenemos la siguiente fila
						row = mysql_fetch_row (resultado);
						count++;
					}
					printf ("Count = %d\n", count);
					IDJugador = 1111 + count;
					printf ("IDJugador = %d\n", IDJugador);
				}
				
				
				//TABLA CREADA
				
				// construimos la consulta SQL para ver si el nombre ya esta registrado
				strcpy (consulta,"SELECT username, password FROM Jugador WHERE username = '"); 
				strcat (consulta, Usuario);
				strcat (consulta,"'");
				//hacemos la consulta 
				err=mysql_query (conn, consulta); 
				if (err!=0) 
				{
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				else
				{
					//recogemos el resultado de la consulta 
					resultado = mysql_store_result (conn); 
					row = mysql_fetch_row (resultado);
					//printf("%s,%s,%s,%s\n", Usuario, row[0], Password, row[1]); //PARA COMPROVAR SI TENEMOS ERRORES (SI TODO OKEI NO HACE FALTA)
					if (row == NULL)
					{
						//listo para registrar
						printf ("No se han obtenido datos en la consulta, listo para registrar\n");
						
						// construimos la consulta SQL
						strcpy (consulta,"INSERT INTO Jugador VALUES('"); 
						strcat (consulta, Usuario);
						strcat (consulta, "','");
						strcat (consulta, Password);
						strcat (consulta, "',");
						sprintf(strIDJugador, "%d", IDJugador);
						strcat (consulta, strIDJugador);
						strcat (consulta,");");
						printf("consulta = %s\n", consulta);
						
						//hacemos la consulta 
						err=mysql_query (conn, consulta); 
						printf("consulta = %s\n", consulta);
						if (err!=0) 
						{
							printf ("Error al introducir datos la base %u %s\n", 
							mysql_errno(conn), mysql_error(conn));
							exit (1);
						}
						else
						{
							//recogemos el resultado de la consulta
							printf("Registrado con: Usuario: %s, Password: %s, ID: %d\n", Usuario, Password, IDJugador);
							sprintf(respuesta, "Registrado");
						}
					}
					else
					{
						// El resultado debe ser una matriz con una sola fila y una columna que contiene el Usuario
						printf ("Usuario ya registrado con Usuario del Jugador: %s\n", row[0]);
						sprintf(respuesta, "ErrorUsuario");
					}
				}
			} 
			else if(codigo==8) //Peticion 8
			{
				p = strtok(NULL, "/");
				int IDPartida = atoi(p);
				printf("IDPartida: %d\n", IDPartida);
				p = strtok(NULL, "/");
				char mensaje[512];
				strcpy(mensaje, p);
				char Cuando[512];
				int Duracion;
				char Ganador[512];
				char consulta[512];
				char strIDPartida[512];
				sprintf(strIDPartida, "%d", IDPartida);
				
				printf("%s\n", mensaje);
				
				// Consulta SQL para obtener todos los valores de la tabla
				err=mysql_query (conn, "SELECT * FROM Partida");
				printf("2 %s\n", mensaje);
				if (err!=0) 
				{
					printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				// Se trata de una tabla virtual en memoria que es la copia de la tabla real en disco.
				resultado = mysql_store_result (conn);
				// Ahora obtenemos la primera fila que se almacena en una variable de tipo MYSQL_ROW
				row = mysql_fetch_row (resultado);
				printf("3 %s\n", mensaje);
			//	int count = 0;
				// En una fila hay tantas columnas como datos tiene una Partida. Cuatro columnas: IDPartida(row[0]), Cuando(row[1]), Duracion(row[2]), Ganador(row[3]).
				if (row == NULL)
					printf ("No se han obtenido datos en la consulta\n");
				else
				{
					while (row !=NULL) 
					{
						// la columna 0,2 contienen palabras y las convertimos a enteros
						IDPartida = atoi (row[0]);
						Duracion = atoi(row[2]);
						// las columnas 1 y 3 contienen Cuando y Ganador
						printf ("IDPartida: %d, Cuando: %s, Duracion: %d, Ganador: %s\n", IDPartida, row[1], Duracion, row[3]); //ROW[0] NSADOMOWMADOAMDIS POR IDPARTIDA
						// obtenemos la siguiente fila
						row = mysql_fetch_row (resultado);
					}
				}
				//TABLA CREADA
				
				// construimos la consulta SQL
				strcpy (consulta,"SELECT fecha_hora, duracion, ganador  FROM Partida WHERE identificador = '"); 
				strcat (consulta, strIDPartida);
				strcat (consulta,"'");
				printf("4 %s\n", mensaje);
				
				//hacemos la consulta 
				err=mysql_query (conn, consulta); 
				printf("5 %s\n", mensaje);
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(respuesta, "IDError/null\0");
					//exit (1);
				}
				else
				{
					//recogemos el resultado de la consulta 
					printf("6 %s\n", mensaje);
					resultado = mysql_store_result (conn); 
					row = mysql_fetch_row (resultado);
					//printf("%d,%s,%s,%s\n", IDPartida, row[0], row[1], row[2]); //row[0] = Cuando, row[1] = Duracion, row[2] = Ganador
					printf("7 %s\n", mensaje);
					if (row == NULL)
					{
						printf ("No se han obtenido datos en la consulta\n");
						sprintf(respuesta, "IDError/0\0");
						
					}
					else if (strcmp(mensaje, "Duracion") == 0)
					{
						// El resultado debe ser una matriz con una sola fila y una columna que contiene la duracion
						printf ("Duracion de la partida: %s\n", row[1]);
						sprintf(respuesta, "Duracion/%s\0", row[1]);
					}
					else if (strcmp(mensaje, "Ganador") == 0)
					{
						// El resultado debe ser una matriz con una sola fila y una columna que contiene la duracion
						printf ("Ganador de la partida: %s\n", row[1]);
						sprintf(respuesta, "Ganador/%s\0", row[2]);
					}
					else if (strcmp(mensaje, "Cuando") == 0)
					{
						// El resultado debe ser una matriz con una sola fila y una columna que contiene la duracion
						printf ("La partida se jugó en: %s\n", row[1]);
						sprintf(respuesta, "Cuando/%s\0", row[0]);
					}
					else if (strcmp(mensaje, "DuracionGanador") == 0)
					{
						// El resultado debe ser una matriz con una sola fila y una columna que contiene la duracion
						printf ("La partida duró: %sminutos y ganó: %s\n", row[1], row[2]);
						sprintf(respuesta, "DuracionGanador/%s/%s\0", row[1], row[2]);
					}
					else if (strcmp(mensaje, "DuracionCuando") == 0)
					{
						// El resultado debe ser una matriz con una sola fila y una columna que contiene la duracion
						printf ("La partida duró: %sminutos y se jugó: %s\n", row[1], row[0]);
						sprintf(respuesta, "DuracionCuando/%s/%s\0", row[1], row[0]);
					}
					else if (strcmp(mensaje, "GanadorCuando") == 0)
					{
						// El resultado debe ser una matriz con una sola fila y una columna que contiene la duracion
						printf ("La partida la ganó: %s y se jugó: %s\n", row[2], row[0]);
						sprintf(respuesta, "GanadorCuando/%s/%s\0", row[2], row[0]);
					}
					else if (strcmp(mensaje, "DuracionGanadorCuando") == 0)
					{
						// El resultado debe ser una matriz con una sola fila y una columna que contiene la duracion
						printf ("La partida duró: %s, la ganó: %s y se jugó: %s\n", row[1], row[2], row[0]);
						sprintf(respuesta, "DuracionGanadorCuando/%s/%s/%s\0", row[1], row[2], row[0]);
					}
					else
					{
						sprintf(respuesta, "Error/null\0");
					}
				}
			}
			write(sock_conn,respuesta,strlen(respuesta));
			printf("Enviado\n");
		}
		close(sock_conn);
	}
	mysql_close (conn);
}
