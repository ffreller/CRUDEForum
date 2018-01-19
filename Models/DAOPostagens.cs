using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;

namespace CRUDForum.Models
{
    public class DAOPostagens
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        string conexao = @"Data Source=.\SQLEXPRESS; Initial Catalog=CRUDE_Forum; uid=sa; pwd=senai@123";

  
        public List<Postagens> Listar(){
            var postagem = new List<Postagens>();

            try{
                con = new SqlConnection();
                con.ConnectionString = conexao;

                con.Open(); //aberta conexão

                //SQL query
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Postagens";
                
                dr = cmd.ExecuteReader();
                
                //adiciona a postagem à lista
                while(dr.Read()){
                    postagem.Add(new Postagens(){
                        id = dr.GetInt32(0),
                        idtopico = dr.GetInt32(1),
                        idusuario = dr.GetInt32(2),
                        mensagem = dr.GetString(3),
                        datapublicacao = dr.GetDateTime(4)
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
            
            return postagem;
        }

        public bool Cadastrar(Postagens postagem){
            bool resultado = false;

            try{
                con = new SqlConnection(conexao);

                con.Open();

                string query = string.Format("INSERT INTO Postagens (idtopico, idusuario, mensagem) VALUES (@n, @e, @m)");
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@n", postagem.idtopico);
                cmd.Parameters.AddWithValue("@e", postagem.idusuario);
                cmd.Parameters.AddWithValue("@m", postagem.mensagem);

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

        public bool Atualizar(Postagens postagem){
            bool resultado = false;
            
            try{
                con = new SqlConnection(conexao);

                con.Open();

                string query = "UPDATE Postagens SET idtopico = @n, idusuario = @e, mensagem = @m WHERE id = @i";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@n", postagem.idtopico);
                cmd.Parameters.AddWithValue("@e", postagem.idusuario);
                cmd.Parameters.AddWithValue("@m", postagem.mensagem);
                cmd.Parameters.AddWithValue("@i", postagem.id);

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
                string query = "DELETE from Postagens WHERE id = @i";

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