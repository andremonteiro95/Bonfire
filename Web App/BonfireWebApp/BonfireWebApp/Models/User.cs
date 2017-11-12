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
using System.Collections;

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
        [Display(Name = "Administrator Privileges")]
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

        public List<User> GetAllUsers()
        {
            List<User> list = new List<User>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspSelectAllUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        user.id = Int32.Parse(reader["id"].ToString());
                        user.Name = reader["Name"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.Privilege = Int32.Parse(reader["Privilege"].ToString()) > 0 ? true : false;
                        list.Add(user);
                    }
                }
            }

            return list;
        }

        public bool AddNewUser(User user)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspAddUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pName", SqlDbType.VarChar).Value = user.Name;
                    cmd.Parameters.Add("@pEmail", SqlDbType.VarChar).Value = user.Email;
                    cmd.Parameters.Add("@pPassword", SqlDbType.VarChar).Value = user.Password;
                    cmd.Parameters.Add("@pPrivilege", SqlDbType.Int).Value = user.Privilege ? 1 : 0;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return Int32.Parse(paramResp.Value.ToString()) == 1;
                }
            }
        }

        public bool EditUser(User user)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspEditUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pId", SqlDbType.Int).Value = user.id;
                    cmd.Parameters.Add("@pName", SqlDbType.VarChar).Value = user.Name;
                    cmd.Parameters.Add("@pEmail", SqlDbType.VarChar).Value = user.Email;
                    cmd.Parameters.Add("@pPassword", SqlDbType.VarChar).Value = user.Password;
                    cmd.Parameters.Add("@pPrivilege", SqlDbType.Int).Value = user.Privilege ? 1 : 0;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return Int32.Parse(paramResp.Value.ToString()) == 1;
                }
            }
        }

        public bool DeleteUser(int id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspDeleteUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pId", SqlDbType.Int).Value = id;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return Int32.Parse(paramResp.Value.ToString()) == 1;
                }
            }
        }
    }
}