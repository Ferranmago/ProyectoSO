#include <mysql.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>

int main(int argc, char **argv)
{

	MYSQL*conn;
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	

	conn = mysql_init(NULL);
	if (conn==NULL){
		printf ("Error al crear la conexión: %u %s\n" , mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	


	conn = mysql_real_connect (conn, "localhost","root", "mysql", "Juego",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}



	int iden;
	printf("Introduce el identificador la partida de la que quieres saber quien ha sido el ganador\n");
	scanf ("%d" , &iden);
	


	char consulta[80];
	strcpy(consulta, "SELECT Partida.ganador FROM Partida WHERE Partida.Identificador = '%d'" , iden);
	err=mysql_query (conn,consulta);

	if (err!=0){
		printf ("Error al consultar datos de la base %u %s\n" , mysql_errno(conn), mysql_error(conn));
		exit (1);
	}


	resultado = mysql_store_result(conn);
	row = mysql_fetch_row (resultado);

	if (row == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
		while (row !=NULL) {
			printf ("%s\n", row[0]);
			row = mysql_fetch_row (resultado);
	}



	mysql_close (conn);
	exit(0);

}


