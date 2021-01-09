using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using MySql.Data.MySqlClient;
public class ConsultasSQL 
{

    public string[] getIds()
    {
        string[] ids= new string[2];
        ConexionBD conexion = new ConexionBD();
        MySqlConnection conn = conexion.getConnection();
        try
        {
           string sql = "SELECT * FROM api ";

           MySqlCommand cmd = new MySqlCommand(sql, conn);
           MySqlDataReader rdr = cmd.ExecuteReader();

           while (rdr.Read())
           {
                Debug.Log(rdr[0] + " -- " + rdr[1] + "--" + rdr[2] + " -- " + rdr[3]);
                ids[0] = rdr[1].ToString();
                ids[1] = rdr[2].ToString();
            }
           
            rdr.Close();

           Debug.Log("Consulta exitosa");
        }
        catch(MySqlException e)
        {
            Debug.Log("No se pudo hacer la consulta IdPaciente");
        }
        conn.Close();
        return ids;
        
    }

    /*public void insertOjo(string idPaciente, string angleValueR, string angleValueL, string prismas)
    {
        ConexionBD conexion = new ConexionBD();
        MySqlConnection conn = conexion.getConnection();
        MySqlCommand cmd = conn.CreateCommand();

        cmd.CommandText = "INSERT INTO ojo (desviacion_der,desviacion_izq,dioptrias_prismaticas,Paciente_idPaciente) VALUES(?desviacion_der,?desviacion_izq,?dioptrias_prismaticas,?Paciente_idPaciente)";
        cmd.Parameters.Add("?desviacion_der", MySqlDbType.Float).Value = angleValueR;
        cmd.Parameters.Add("?desviacion_izq", MySqlDbType.Float).Value = angleValueL;
        cmd.Parameters.Add("?dioptrias_prismaticas", MySqlDbType.Float).Value = prismas;
        cmd.Parameters.Add("?Paciente_idPaciente", MySqlDbType.Int32).Value = idPaciente;
        cmd.ExecuteNonQuery();
        conn.Close();
    }*/

    public void insertDiagnostico(string strabismusType, string idEspecialista, string idPaciente)
    {
        ConexionBD conexion = new ConexionBD();
        MySqlConnection conn = conexion.getConnection();
        MySqlCommand cmd = conn.CreateCommand();

        cmd.CommandText = "INSERT INTO Diagnostico (tipo_estrabismo,Especialista_idEspecialista,Paciente_idPaciente) VALUES(?tipo_estrabismo,?Especialista_idEspecialista,?Paciente_idPaciente)";
        cmd.Parameters.Add("?tipo_estrabismo", MySqlDbType.VarChar).Value = strabismusType;
        cmd.Parameters.Add("?Especialista_idEspecialista", MySqlDbType.Int32).Value = idEspecialista;
        cmd.Parameters.Add("?Paciente_idPaciente", MySqlDbType.Int32).Value = idPaciente;
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void insertPreDiagnostico(string idEspecialista, string idPaciente,string angleValueR, string angleValueL, string prismas)
    {
        string monthVar = System.DateTime.Now.ToString("yyyy-MM-dd");
        ConexionBD conexion = new ConexionBD();
        MySqlConnection conn = conexion.getConnection();
        MySqlCommand cmd = conn.CreateCommand();

        cmd.CommandText = "INSERT INTO Prediagnostico (fecha,Especialista_idEspecialista,Paciente_idPaciente,desviacion_der,desviacion_izq,dioptrias_prismaticas) VALUES(?fecha,?Especialista_idEspecialista,?Paciente_idPaciente,?desviacion_der,?desviacion_izq,?dioptrias_prismaticas)";
        cmd.Parameters.Add("?fecha", MySqlDbType.Date).Value = monthVar;
        cmd.Parameters.Add("?Especialista_idEspecialista", MySqlDbType.Int32).Value = idEspecialista;
        cmd.Parameters.Add("?Paciente_idPaciente", MySqlDbType.Int32).Value = idPaciente;
        cmd.Parameters.Add("?desviacion_der", MySqlDbType.Float).Value = angleValueR;
        cmd.Parameters.Add("?desviacion_izq", MySqlDbType.Float).Value = angleValueL;
        cmd.Parameters.Add("?dioptrias_prismaticas", MySqlDbType.Float).Value = prismas;
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}
