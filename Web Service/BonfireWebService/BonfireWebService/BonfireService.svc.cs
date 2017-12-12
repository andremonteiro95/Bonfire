using BonfireWebService.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BonfireWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BonfireService : IService
    {
        public List<Content> GetContentsByBeacon(string inputUuid)
        {
            List<Content> list = new List<Content>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspGetContentsByBeacon", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramUuid = new SqlParameter("@pUuid", SqlDbType.UniqueIdentifier);
                    paramUuid.Value = new Guid(inputUuid);
                    cmd.Parameters.Add(paramUuid);
                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Content content = new Content();
                        content.id = reader["id"].ToString();
                        content.Title = reader["Title"].ToString();
                        content.Description = reader["Description"].ToString();
                        content.Url = reader["Url"].ToString();
                        content.StartDate = 
                            DateTime.Parse(reader["StartDate"].ToString()).ToString("yyyy-MM-dd");
                        content.EndDate = 
                            DateTime.Parse(reader["EndDate"].ToString()).ToString("yyyy-MM-dd");
                        list.Add(content);
                    }
                }
            }

            return list;
        }
    }
}
