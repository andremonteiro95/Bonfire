using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BonfireWebApp.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        public string Email { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        [Required]
        public bool Privilege { get; set; }
     
        public User()
        {
            id = 0;
            Name = "";
            Email = "";
            Password = "";
            Privilege = false;
        }
    }

    public class UserDBContext : IDisposable
    {
        public UserDBContext()
        {

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public User UserLogin(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspLoginUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pEmail", SqlDbType.VarChar).Value = email;
                    cmd.Parameters.Add("@pPassword", SqlDbType.VarChar).Value = password;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    try { 
                        if (Int32.Parse(paramResp.Value.ToString()) > 0)
                        {
                            User result = GetUserById(Int32.Parse(paramResp.Value.ToString()));
                            return result;
                        }
                        else
                            return null;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                }
            }
        }

        public User GetUserById(int id)
        {
            User user = new User();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspSelectUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pId", SqlDbType.VarChar).Value = id;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user.id = id;
                        user.Name = reader["Name"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.Privilege = Int32.Parse(reader["Privilege"].ToString()) > 0 ? true : false;
                    }
                }
            }

            return user;
        }
    }
}