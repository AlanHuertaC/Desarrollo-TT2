using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

using MySql.Data.MySqlClient;
/*Clase encargada a solo establecer los parametros para la conexion a la BD*/
/*Si requieren hacer una consulta hay una clase llamada ConsultasSQL donde lo pueden hacer */
public class ConexionBD
{

    public MySqlConnection getConnection()
    {
        string connStr = "server=localhost;user=root;database=eyetec;port=3306;password=root";
        MySqlConnection conn = new MySqlConnection(connStr);

        try
        {
            conn.Open();
            Debug.Log("Conexion establecida");
        }
        catch (MySqlException ex)
        {
            Debug.Log("Conexion fallida: ");
        }
        return conn;
    }
}
