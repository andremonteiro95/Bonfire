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
    public class ContentBeacon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int ContentId { get; set; }

        public List<string> BeaconIds { get; set; }

        public ContentBeacon()
        {
            id = 0;
            ContentId = 0;
            BeaconIds = new List<string>();
        }
    }

    public class ContentBeaconDBContext : IDisposable
    {
        public ContentBeaconDBContext()
        {

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public ContentBeacon GetContentBeaconsById(int id)
        {
            ContentBeacon cb = new ContentBeacon();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspSelectContentBeacons", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pId", SqlDbType.Int).Value = id;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    cb.ContentId = id;

                    while (reader.Read())
                    {
                        cb.BeaconIds.Add(reader["BeaconId"].ToString());
                    }
                }
            }

            return cb;
        }
    }
}