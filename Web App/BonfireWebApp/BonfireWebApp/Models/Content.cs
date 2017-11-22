using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BonfireWebApp.Models
{
    public class Content
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(64)]
        public string Title { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(1024)]
        public string Url { get; set; }

        [Required]
        [Display(Name="Start Date")]
        public string StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        public Content()
        {
            StartDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            EndDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
        }

        public class ContentDBContext : IDisposable
        {
            public ContentDBContext()
            {

            }

            public void Dispose()
            {
                //throw new NotImplementedException();
            }

            public List<Content> GetAllContents()
            {
                List<Content> list = new List<Content>();

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspSelectAllContents", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                        paramResp.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramResp);

                        con.Open();
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Content content = new Content();
                            content.id = Int32.Parse(reader["id"].ToString());
                            content.Title = reader["Title"].ToString();
                            content.Description = reader["Description"].ToString();
                            content.Url = reader["Url"].ToString();
                            content.StartDate = reader["StartDate"].ToString();
                            content.EndDate = reader["EndDate"].ToString();
                            list.Add(content);
                        }
                    }
                }

                return list;
            }

            public bool DeleteContent(int id)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspDeleteContent", con))
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

            public bool AddContent(Content content)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspAddContent", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@pTitle", SqlDbType.VarChar).Value = content.Title;
                        cmd.Parameters.Add("@pDescription", SqlDbType.VarChar).Value = content.Description;
                        cmd.Parameters.Add("@pUrl", SqlDbType.VarChar).Value = content.Url;
                        if (cmd.Parameters[2].Value == null)
                            cmd.Parameters[2].Value = DBNull.Value;
                        cmd.Parameters.Add("@pStartDate", SqlDbType.Date).Value = content.StartDate;
                        cmd.Parameters.Add("@pEndDate", SqlDbType.Date).Value = content.EndDate;

                        SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                        paramResp.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramResp);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        return Int32.Parse(paramResp.Value.ToString()) == 1;
                    }
                }
            }

            public Content GetContentById(int id)
            {
                Content content = new Content();

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspSelectContent", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@pId", SqlDbType.Int).Value = id;

                        SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                        paramResp.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramResp);

                        con.Open();
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            content.id = id;
                            content.Title = reader["Title"].ToString();
                            content.Description = reader["Description"].ToString();
                            content.Url = String.IsNullOrWhiteSpace(reader["Url"].ToString()) ? null : reader["Url"].ToString();
                            content.StartDate = reader["StartDate"].ToString();
                            content.EndDate = reader["EndDate"].ToString();
                        }
                    }
                }

                return content;
            }

            public bool EditContent(Content content)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspEditContent", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@pId", SqlDbType.Int).Value = content.id;
                        cmd.Parameters.Add("@pTitle", SqlDbType.VarChar).Value = content.Title;
                        cmd.Parameters.Add("@pDescription", SqlDbType.VarChar).Value = content.Description;
                        cmd.Parameters.Add("@pUrl", SqlDbType.VarChar).Value = content.Url;
                        cmd.Parameters.Add("@pStartDate", SqlDbType.Date).Value = content.StartDate;
                        cmd.Parameters.Add("@pEndDate", SqlDbType.Date).Value = content.EndDate;

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
}