#include <stdio.h>
#include <unistd.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <mysql.h>
#include <pthread.h>

//Estructura importante para el acceso excluyente, LA PONGO PERO NO SE QUE HACE:
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

//Definicion estructuras para ListaConectados
typedef struct {
	char nombre[80];
	int socket;
} Conectado;
typedef struct {
	Conectado conectados[100];
	int num;
} ListaConectados;

//Definimos Lista de Conectados
ListaConectados miListaConectados;
int contador;

int PonConectado(ListaConectados *lista, char nombre[80], int socket, char notificacion[512]){
	if (lista -> num == 100){
		return -1;
	}
	else{
		strcpy (lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num++;
		return 0;
	}
	
	//PARA IMPRIMIR LA LISTA
	int j = 0;
	printf("%d\n", miListaConectados.num);
	sprintf(notificacion, "3/%d", miListaConectados.num);

	while (j <= miListaConectados.num){
		printf("LISTAAA 	Nombre: %s, socket: %d\n", miListaConectados.conectados[j].nombre, miListaConectados.conectados[j].socket);
		sprintf(notificacion, "%s/%s/%d", notificacion, miListaConectados.conectados[j].nombre, miListaConectados.conectados[j].socket);
		j ++;
	} //FIN DE LISTA
}

/*
int DameSocket(ListaConectados *lista, char nombre [80]){
	//Devuelve el socket o -1 si no esta en la lista
	int i = 0;
	int encontrado = 0;
	while ((i<lista->num && encontrado != 0)){
		if (strcmp(lista->conectados[i].nombre, nombre) == 0){
			encontrado = 1;
		}
		i++;
	}
	if (encontrado)
		return lista->conectados[i].socket;
	else
		return -1;
}
*/

int EliminarConectado(ListaConectados *lista, int socket){
	int posicion;
	for (int i = 0; i<lista->num; i++){
		if (socket == miListaConectados.conectados[i].socket){
			posicion = i;
			printf("Posicion: %d, Socket; %d Nombre: %s\n", posicion,miListaConectados.conectados[i].socket, miListaConectados.conectados[i].nombre );
		}
	}
	for (posicion; posicion<lista->num; posicion++){
		strcpy(lista->conectados[posicion].nombre, lista->conectados[posicion+1].nombre);
		lista->conectados[posicion].socket = lista->conectados[posicion+1].socket;
	}
	lista->num--;
	
	//PARA IMPRIMIR LA LISTA
	int j = 0;
	printf("%d\n", miListaConectados.num);
	//lista->num
	while (j <= miListaConectados.num){
		printf("LISTAAA 	Nombre: %s, socket: %d\n", miListaConectados.conectados[j].nombre, miListaConectados.conectados[j].socket);
		j ++;
	} //FIN DE LISTA
}

/*
int LimpiarLista(ListaConectados *lista){
	int posicion;
	for (int i = 0; i<lista->num; i++){
		if (strcmp (miListaConectados.conectados[i].nombre, "") == 0){
			posicion = i;
			printf("Posicion: %d, Socket; %d Nombre: %s\n", posicion,miListaConectados.conectados[i].socket, miListaConectados.conectados[i].nombre);
		}
		else
			printf("Nada\n");
	}
	for (posicion; posicion<lista->num; posicion++){
		strcpy(lista->conectados[posicion].nombre, lista->conectados[posicion+1].nombre);
		lista->conectados[posicion].socket = lista->conectados[posicion+1].socket;
	}
	//miListaConectados.num = miListaConectados.num - 1;
}
*/

void FuncionDesconectar(ListaConectados *lista, int *terminar, char *p, char *respuesta, int sock_conn) {
	printf("VAMOS A DESCONECTAR\n");
	*terminar = 1;
	p = strtok(NULL, "/");
	int socket = atoi(p);
	if (miListaConectados.num != 0) {
		EliminarConectado(&miListaConectados, socket);
	}
	
	//PARA IMPRIMIR LA LISTA
	sprintf(respuesta, "0/Desconectar/%d", miListaConectados.num);
	int j = 0;
	printf("%d\n", miListaConectados.num );
	while (j <= miListaConectados.num){
		printf("LISTAAA 	Nombre: %s, socket: %d\n", miListaConectados.conectados[j].nombre, miListaConectados.conectados[j].socket);
		strcat(respuesta, "/");
		strcat(respuesta, miListaConectados.conectados[j].nombre);
		strcat(respuesta, "/");
		char Socket[80];
		sprintf(Socket, "%d", miListaConectados.conectados[j].socket);
		strcat(respuesta, Socket);
		j ++;
	} //FIN DE LISTA
	
}

void FuncionIniciarSesion(ListaConectados *lista, char *p, int err, MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, char *respuesta, int sock_conn, char notificacion[512]){

	char Usuario[30];
	char Password[30];
	p = strtok(NULL, "/");
	strcpy(Usuario, p);
	p = strtok(NULL, "/");
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
			//printf ("Usuario: %s, Password: %s, IDJugador: %d\n", row[0], row[1], IDJugador); LO COMENTAMOS PARA QUE NO MUESTRE LA TABLA
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
	printf("dentro 4\n");
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
			sprintf(respuesta, "1/Incorrecto");
			
		}
		else if ((strcmp(row[0], Usuario) == 0) && (strcmp(row[1], Password) == 0)) 
		{
			// El resultado debe ser una matriz con una sola fila y una columna que contiene el Usuario
			printf ("Usuario del Jugador: %s y Socket: %d\n", row[0], sock_conn);
	
			//PONEMOS UN CONECTADO A LA LISTA.
			int x = PonConectado(&miListaConectados, Usuario , sock_conn, notificacion);
			printf("%d\n", miListaConectados.num);
			
			
			if (x == -1)
				printf("La ListaConectados esta llena.\n");
			else
			{
				printf("Añadido correctamente a la ListaConectados.\n");
				printf("%d\n", miListaConectados.num);
			}
			
			
			
			//PARA IMPRIMIR LA LISTA
			sprintf(respuesta, "1/Correcto/%d", miListaConectados.num);
			int j = 0;
			printf("%d\n", miListaConectados.num );
			while (j < miListaConectados.num){
				printf("LISTAAA 	Nombre: %s, socket: %d\n", miListaConectados.conectados[j].nombre, miListaConectados.conectados[j].socket);
				strcat(respuesta, "/");
				strcat(respuesta, miListaConectados.conectados[j].nombre);
				strcat(respuesta, "/");
				char Socket[80];
				sprintf(Socket, "%d", miListaConectados.conectados[j].socket);
				strcat(respuesta, Socket);
				j ++;
			} //FIN DE LISTA
			
			
		}
		else
		{
			printf("Usuario: %s, Password: %s\n", Usuario, Password);
			sprintf(respuesta, "1/Semicorrecto");
		}
	}
	
}
void FuncionRegistrarse(ListaConectados *lista, char *p, int err, MYSQL *conn, MYSQL_RES *resultado, MYSQL_ROW row, char *respuesta, int sock_conn)
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
	
	// CREAMOS consulta SQL para obtener TODOS los valores de la tabla
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
			//printf ("Usuario: %s, Password: %s, IDJugador: %d\n", row[0], row[1], IDJugador); COMENTADO PARA QUE NO IMPRIMA TODA LA TABLA.
			// obtenemos la siguiente fila
			row = mysql_fetch_row (resultado);
			count++;
		}
		printf ("Count = %d\n", count);
		IDJugador = 1111 + count;
		printf ("IDJugador = %d\n", IDJugador);
	}
	//TABLA CREADA
	
	//Construimos la consulta SQL para ver si el nombre ya esta registrado
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
				sprintf(respuesta, "2/Registrado");
			}
		}
		else
		{
			// El resultado debe ser una matriz con una sola fila y una columna que contiene el Usuario
			printf ("Usuario ya registrado con Usuario del Jugador: %s\n", row[0]);
			sprintf(respuesta, "2/ErrorUsuario");
		}
	}
};


void *AtenderCliente (void *socket){
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;
	//Peticion y respuestas son vectores de caracteres
	char peticion[512];
	char respuesta[512];
	char notificacion[512];
	int ret;
	
	//CONECTAMOS MYSQL
	MYSQL *conn;
	int err;
	// Estructura especial para almacenar resultados de consultas
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	//Creamos una conexion al servidor MYSQL
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexionn: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "Juego", 0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion con la BBDD: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	} // CONEXION COMPLETADA CON MYSQL
	
	//Bucle de atencion al cliente
	int terminar = 0;
	while (terminar == 0)
	{
		//Recogemos la peticion. Hacemos una operacion de lectura a traves del socket de conexiton y la peticion la guarda en el vector de caracteres peticion
		ret=read(sock_conn,peticion,sizeof(peticion));
		//Ver como va progresando la ejecucion del servidor
		printf("Recibido\n");
		//Añadimos un fin de linea, nos ayudara a procesar el mensaje
		peticion[ret]='\0';
		printf("Peticion: %s\n", peticion);
		//Cortamos por donde haya una barra
		char *p = strtok(peticion, "/"); //Nos apunta al trozo que va desde el inicio hasta la barra
		int codigo = atoi(p);
		printf("Codigo: %d\n", codigo);
		
		//pthread_mutex_lock(&mutex);
		//pthread_mutex_unlock(&mutex);
		
		if (codigo == 0) 
		{
			pthread_mutex_lock(&mutex);
			FuncionDesconectar(&miListaConectados, &terminar, p, respuesta, sock_conn);
			pthread_mutex_unlock(&mutex);
			printf("Eliminado correctamente de la ListaConectados.\n");
			
			for(int i=0; i< miListaConectados.num; i++)
			{
				write(miListaConectados.conectados[i].socket, respuesta,strlen(respuesta));
			}
			
		}
		else if(codigo==1) //Peticion 1  //"MENSAJE ESPERADO:   1/usuari/contrasena --> usuari: contrasena:
		{
			pthread_mutex_lock(&mutex);
			FuncionIniciarSesion(&miListaConectados, p, err, conn, resultado, row, respuesta, sock_conn, notificacion);			
			pthread_mutex_unlock(&mutex);
			
			for(int i=0; i< miListaConectados.num; i++)
			{
				write(miListaConectados.conectados[i].socket, respuesta,strlen(respuesta));
			}
			
		}
		else if(codigo==2) //Peticion 2  //"MENSAJE ESPERADO:   2/usuari/contrasena --> usuari: contrasena:
		{
			pthread_mutex_lock(&mutex);
			FuncionRegistrarse(&miListaConectados, p, err, conn, resultado, row, respuesta, sock_conn);			
			pthread_mutex_unlock(&mutex);
			
			write(sock_conn, respuesta,strlen(respuesta));

		} 
		else if(codigo==3) //Peticion 3  //"MENSAJE ESPERADO:   3/
		{
			char mensaje[512];
			mensaje[0] = '\0'; // inicializar la cadena mensaje a una cadena vacia
			for (int i=0; i < miListaConectados.num; i++) {
				char usuario[512];
				sprintf(usuario, "%s/", miListaConectados.conectados[i].nombre);
				strcat(mensaje, usuario);
				char socket[512];
				sprintf(socket, "%d/", miListaConectados.conectados[i].socket);
				strcat(mensaje, socket);
				printf("%s\n", mensaje);
				printf("Conectado: %d: %s con Socket: %d\n", i+1, miListaConectados.conectados[i].nombre, miListaConectados.conectados[i].socket);
			}
			//PARA IMPRIMIR LA LISTA
			int j = 0;
			printf("%d\n", miListaConectados.num);
			while (j <= miListaConectados.num){
				printf("LISTAAA 	Nombre: %s, socket: %d\n", miListaConectados.conectados[j].nombre, miListaConectados.conectados[j].socket);
				j ++;
			} //FIN DE LISTA
			sprintf(respuesta, "3/%d/%s\0",  miListaConectados.num, mensaje); // 3/numero/usuario/socket/usuario2... ESTE ES EL MENSAJE QUE ENVIAMOS AL CLIENTE
			printf("%s\n", respuesta);
		}
		
		
		else if(codigo==5) //Peticion 3  //"MENSAJE ESPERADO:   3/
		{
			char usuario[512];
			usuario[0] = '\0'; // inicializar la cadena mensaje a una cadena vacia
			p = strtok(NULL, "/");
			strcpy(usuario, p);
			p = strtok(NULL, "/");
			int dinero = atoi(p);
			char strdinero[512];
			sprintf(strdinero, "%d", dinero);
			
			sprintf(respuesta, "5/%s/%s\0",  usuario, strdinero); // 3/numero/usuario/socket/usuario2...
			printf("%s\n", respuesta);
			
			for(int i=0; i< miListaConectados.num; i++)
			{
				write(miListaConectados.conectados[i].socket, respuesta,strlen(respuesta));
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
		printf("%s\n", respuesta);

		
		printf("Enviado\n");
		printf("%s\n", respuesta);
	}
	mysql_close (conn);
}


int main(int argc, char *argv[]) 
{
	//Necesitamos unas variables y estructuras que podemos usar gracias a los includes
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	//Creamos un socket (lo llamamos socket de escucha) , esperar a una conexiÃ³n, especificando el tipo de conexiÃ³n que vamos a crear
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
	{
		printf("Error creando el socket\n");
		exit(1);
	}
	//Inicializamos una estructura de datos que va a necesitar indicando que tipo de protocolos va a usar
	memset(&serv_adr,0,sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	//Indicamos la direcciÃ³n de IP donde va a escuchar (la que tenga asignada)
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	//Especificamos el puerto en el que va a escuchar (en nuestro caso en el 9050)
	serv_adr.sin_port = htons(41078); 
	//Asociamos al socket de escucha la estructura de datos que hemos creado
	if(bind(sock_listen,(struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
	{
		printf("Error en el bind\n");
		exit(1);
	}
	//La cola de espera de conexiones (peticiones) a ser atendidas no puede ser mayor de 3  ----------------------- HABRA QUE CAMBIARLOOOOOOOOOOOOOOOO --------------------------
	if(listen(sock_listen,3)<0)
	{
		printf("Error en el listen\n");
		exit(1);
	}
	
	int sockets[100];
	int c = 0;
	pthread_t thread;
	//Hacemos un bucle infinito
	for(;;)
	{
		printf("Escuchando Conexion:\n");
		//Esperamos a una conexion. Cuando se conecte creara un socket distinto al de escucha (sock_conn) que se va a comunicar con el cliente
		sock_conn = accept(sock_listen, NULL, NULL);
		printf("	Conexion recibida\n");
		
		sockets[c] = sock_conn;
		//miListaConectados.conectados[ContadorConectados].socket = sock_conn;
		//printf("%d\n", &miListaConectados.conectados[ContadorConectados].socket);
		printf("	%d\n", sockets[c]);
		printf("	ha salido bien\n");
		
		//pthread_create (&thread, NULL, AtenderCliente, &miListaConectados.conectados[ContadorConectados].socket);
		pthread_create (&thread, NULL, AtenderCliente, &sockets[c]);
		c++;
		printf("	ha salido bien 2\n");
 	}
}
