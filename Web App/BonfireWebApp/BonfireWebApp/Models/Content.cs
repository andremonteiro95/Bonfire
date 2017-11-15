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

        [Required]
        [StringLength(1024)]
        public string Url { get; set; }

        public class ContentDBContext : IDisposable
        {
            public ContentDBContext()
            {

            }

            public void Dispose()
            {
                //throw new NotImplementedException();
            }

            public List<Content> GetAllBeacons()
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

        }
    }
}