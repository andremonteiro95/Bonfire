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
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public class Contents
        {
            public string id { get; set; }
            
            public string Title { get; set; }
            
            public string Description { get; set; }

            public string Url { get; set; }
        }

        public List<Contents> GetContentsByBeacon(string str)
        {
            List<Contents> list = new List<Contents>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspGetContentsByBeacon", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Contents content = new Contents();
                        content.id = reader["id"].ToString();
                        content.Title = reader["Title"].ToString();
                        content.Description = reader["Description"].ToString();
                        content.Url = reader["Url"].ToString();
                        list.Add(content);
                    }
                }
            }

            return list;
        }
    }
}
