using SimpleBot.Repo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class UserSqlRepo : IUserRepo
    {
        string _connectionString;
        public UserSqlRepo(string conectionString)
        {
            _connectionString = conectionString;
        }
        
        public void SalvarHistorico(Message message)
        {         
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("insert into testeTB (id,usuario,mesagem) values (@id,@user,@mesagem)", conn);
                cmd.Parameters.AddWithValue("@id", message.Id);
                cmd.Parameters.AddWithValue("@user", message.User);
                cmd.Parameters.AddWithValue("@mesagem", message.Text);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally {
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }   
                
                
            }
        }
    }
}