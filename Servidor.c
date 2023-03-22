#include <stdio.h>
#include <unistd.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>

int main(int argc, char *argv[]) {
	
	//Necesitamos unas variables y estructuras que podemos usar gracias a los includes
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	//Petición y respuestas son vectores de caracteres
	char peticion[512];
	char respuesta[512];
	//Creamos un socket (lo llamamos socket de escucha) , esperar a una conexión, especificando el tipo de conexión que vamos a crear
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creando el socket");
	//Inicializamos una estructura de datos que va a necesitar indicando que tipo de protocolos va a usar
	memset(&serv_adr,0,sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	//Indicamos la dirección de IP donde va a escuchar (la que tenga asignada)
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	//Especificamos el puerto en el que va a escuchar (en nuestro caso en el 9050)
	serv_adr.sin_port = htons(35674); //-------------------------------------------------- HABRA QUE CAMBIARLOOOOOOOOOOOOOOOO --------------------------
	//Asociamos al socket de escucha la estructura de datos que hemos creado
	if(bind(sock_listen,(struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf("Error en el bind");
	//La cola de espera de conexiones (peticiones) a ser atendidas no puede ser mayor de 3     ------------------------- HABRA QUE CAMBIARLOOOOOOOOOOOOOOOO --------------------------
	if(listen(sock_listen,3)<0)
		printf("Error en el listen");
	
	//Hacemos un bucle para solo atender a 5 peticiones  ------------------------- HABRA QUE CAMBIARLOOOOOOOOOOOOOOOO --------------------------
		
	int i;
	for(i=0;i<5;i++){
		printf("%s\n", serv_adr.sin_addr.s_addr); 
		printf("Escuchando\n");
		
		//Esperamos a una conexión. Cuando se conecte creará un socket distinto al de escucha (sock_conn) que se va a comunicar con el cliente
		sock_conn = accept(sock_listen, NULL, NULL);
		printf("Conexion recibida");
		//Recogemos la petición. Hacemos una operación de lectura a través del socker de conexión y la petición la guarda en el vector de caracteres petición
		ret=read(sock_conn,peticion,sizeof(peticion));
		//Ver como va progresando la ejecución del servidor
		printf("Recibido\n");
		//Añadimos un fin de linea, nos ayudará a procesar el mensaje
		peticion[ret]='\0';
		
		printf("Peticion: %s\n", peticion);
		//Cortamos por donde haya una barra
		char *p = strtok(peticion, "/"); //Nos apunta al trozo que va desde el inicio hasta la barra
		int codigo = atoi(p);
		p = strtok(NULL, "/");
		char nombre[20];
		strcpy(nombre,p); //El segundo trozo que empieza en p me lo copio a nombre
		printf("Codigo: %d, Nombre: %s\n", codigo, nombre);
		
		if(codigo==1) //Queremos sa
			//"MENSAJE ESPERADO:   /1 usuari /contraseña --> usuari contraseña:
		{
		
			p = strtok(NULL, "/");
			char usuari[30];
			strcpy(usuari, p);
			p = strtok(NULL, "/");
			char password[30];
			strcpy(password, p);
			//sprintf
			
		}
	}
		
		
		
		
		
}

