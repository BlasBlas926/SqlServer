using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Practica.Models;

namespace Practica.Data
{
    public class PersonaData
    {

        public static bool Agregar(Persona opersona, string connectionString)
        {
            using (SqlConnection oConexion = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "insert into Persona (Id,Nombre, ApellidoPaterno, ApellidoMaterno) values (@Id,@Nombre, @ApellidoPaterno, @ApellidoMaterno)",
                    oConexion
                );
                cmd.Parameters.AddWithValue("@Id", opersona.Id);
                cmd.Parameters.AddWithValue("@Nombre", opersona.Nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", opersona.ApellidoPaterno ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", opersona.ApellidoMaterno ?? (object)DBNull.Value);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }
        public static Persona Actualizar(int Id, Persona persona, string connectionString)
{
    using (SqlConnection oConexion = new SqlConnection(connectionString))
    {
        SqlCommand cmd = new SqlCommand(
            "update Persona set Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno where Id = @Id",
            oConexion
        );
        cmd.Parameters.AddWithValue("@Id", Id);
        cmd.Parameters.AddWithValue("@Nombre", persona.Nombre ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@ApellidoPaterno", persona.ApellidoPaterno ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@ApellidoMaterno", persona.ApellidoMaterno ?? (object)DBNull.Value);
        cmd.CommandType = CommandType.Text;
        try
        {
            oConexion.Open();
            int filasAfectadas = cmd.ExecuteNonQuery();
            if (filasAfectadas > 0)
            {
                persona.Id = Id;
                return persona;
            }
            else
            {
                return null;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
}
        public static List<Persona> ObtenerTodos(string connectionString)
        {
            List<Persona> personas = new List<Persona>();
            using (SqlConnection oConexion = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Persona", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Persona oPersona = new Persona();
                            oPersona.Id = Convert.ToInt32(dr["Id"]);
                            oPersona.Nombre = dr["Nombre"].ToString();
                            oPersona.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
                            oPersona.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
                            personas.Add(oPersona);
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return personas;
        }

        public static Persona Obtener(int Id, string connectionString)
        {
            Persona oPersona = new Persona();
            using (SqlConnection oConexion = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Persona where Id = @Id", oConexion);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            oPersona.Id = Convert.ToInt32(dr["Id"]);
                            oPersona.Nombre = dr["Nombre"].ToString();
                            oPersona.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
                            oPersona.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return oPersona;
        }

        public static Persona Eliminar(int Id, string connectionString)
        {
            Persona opersona = new Persona();
            using (SqlConnection oconexion = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("delete from Persona where Id = @Id", oconexion);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    return opersona;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}