using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;

namespace CRUDForum.Models
{
    public class DAOTopicos
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        string conexao = @"Data Source=.\SQLEXPRESS; Initial Catalog=CRUDE_Forum; uid=sa; pwd=senai@123";

  
        public List<Topicos> Listar(){
            var topico = new List<Topicos>();

            try{
                con = new SqlConnection();
                con.ConnectionString = conexao;

                con.Open(); //aberta conexão

                //SQL query
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Topicos";
                
                dr = cmd.ExecuteReader();
                
                //adiciona a topico à lista
                while(dr.Read()){
                    topico.Add(new Topicos(){
                        id = dr.GetInt32(0),
                        titulo = dr.GetString(1),
                        descricao = dr.GetString(2),
                        datacadastro = dr.GetDateTime(3)
                    });
                }
            }
            catch(SqlException ex){
                throw new Exception(ex.Message);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            finally{
                con.Close();
            }
            
            return topico;
        }

        public bool Cadastrar(Topicos topico){
            bool resultado = false;

            try{
                con = new SqlConnection(conexao);

                con.Open();

                string query = string.Format("INSERT INTO Topicos (titulo, descricao) VALUES (@n, @e)");
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@n", topico.titulo);
                cmd.Parameters.AddWithValue("@e", topico.descricao);

                int r = cmd.ExecuteNonQuery();
                if(r > 0)
                    resultado = true;
                
                cmd.Parameters.Clear();
            }
            catch(SqlException ex){
                throw new Exception(ex.Message);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            finally{
                con.Close();
            }

            return resultado;
        }

        public bool Atualizar(Topicos topico){
            bool resultado = false;
            
            try{
                con = new SqlConnection(conexao);

                con.Open();

                string query = "UPDATE Topicos SET titulo = @n, descricao = @e WHERE id = @i";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@n", topico.titulo);
                cmd.Parameters.AddWithValue("@e", topico.descricao);
                cmd.Parameters.AddWithValue("@i", topico.id);

                int r = cmd.ExecuteNonQuery();
                if(r > 0)
                    resultado = true;
                
                cmd.Parameters.Clear();
            }
            catch(SqlException ex){
                throw new Exception(ex.Message);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            finally{
                con.Close();
            }

            return resultado;
        }

        public bool Excluir(int id){
            bool resultado = false;
            
            try{
                con = new SqlConnection(conexao);
                string query = "DELETE from Topicos WHERE id = @i";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@i", id);
                
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if(r > 0)
                    resultado = true;
                
                cmd.Parameters.Clear();
            }
            catch(SqlException ex){
                throw new Exception(ex.Message);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            finally{
                con.Close();
            }

            return resultado;
        }
    }
}