using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;

namespace CRUDForum.Models
{
    public class DAOUsuarios
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        string conexao = @"Data Source=.\SQLEXPRESS; Initial Catalog=CRUDE_Forum; uid=sa; pwd=senai@123";

  
        public List<Usuarios> Listar(){
            var usuario = new List<Usuarios>();

            try{
                con = new SqlConnection();
                con.ConnectionString = conexao;

                con.Open(); //aberta conexão

                //SQL query
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Usuarios";
                
                dr = cmd.ExecuteReader();
                
                //adiciona a usuario à lista
                while(dr.Read()){
                    usuario.Add(new Usuarios(){
                        id = dr.GetInt32(0),
                        nome = dr.GetString(1),
                        login = dr.GetString(2),
                        senha = dr.GetString(3),
                        datacadastro = dr.GetDateTime(4)
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
            
            return usuario;
        }

        public bool Cadastrar(Usuarios usuario){
            bool resultado = false;

            try{
                con = new SqlConnection(conexao);

                con.Open();

                string query = string.Format("INSERT INTO Usuarios (nome, login, senha) VALUES (@n, @e, @m)");
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@n", usuario.nome);
                cmd.Parameters.AddWithValue("@e", usuario.login);
                cmd.Parameters.AddWithValue("@m", usuario.senha);

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

        public bool Atualizar(Usuarios usuario){
            bool resultado = false;
            
            try{
                con = new SqlConnection(conexao);

                con.Open();

                string query = "UPDATE Usuarios SET nome = @n, login = @e, senha = @m WHERE id = @i";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@n", usuario.nome);
                cmd.Parameters.AddWithValue("@e", usuario.login);
                cmd.Parameters.AddWithValue("@m", usuario.senha);
                cmd.Parameters.AddWithValue("@i", usuario.id);

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
                string query = "DELETE from Usuarios WHERE id = @i";

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