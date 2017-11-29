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

        public ContentBeacon GetContentBeaconsById(int contentId)
        {
            ContentBeacon cb = new ContentBeacon();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspSelectContentBeacons", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pContentId", SqlDbType.Int).Value = contentId;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    cb.ContentId = contentId;

                    while (reader.Read())
                    {
                        cb.BeaconIds.Add(reader["BeaconId"].ToString());
                    }
                }
            }

            return cb;
        }

        public bool SaveContentBeacons(int contentId, string[] content)
        {
            List<string> list = new List<string>(content);

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                // Get all content's beacons
                ContentBeacon contentBeacon = GetContentBeaconsById(contentId);

                var beaconsToDelete = contentBeacon.BeaconIds.Except(list);

                var beaconsToKeep = contentBeacon.BeaconIds.Intersect(list);

                var beaconsToSave = list.Except(beaconsToKeep);

                foreach (string beaconId in beaconsToDelete) { 
                    using (SqlCommand cmd = new SqlCommand("uspDeleteContentBeacon", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@pContentId", SqlDbType.Int).Value = contentId;
                        cmd.Parameters.Add("@pBeaconId", SqlDbType.UniqueIdentifier).Value = new Guid(beaconId);

                        SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                        paramResp.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramResp);

                        if (con.State != ConnectionState.Open)
                            con.Open();
                        cmd.ExecuteNonQuery();

                        if (Int32.Parse(paramResp.Value.ToString()) == 0)
                            return false;
                    }
                }

                foreach (string beaconId in beaconsToSave)
                {
                    using (SqlCommand cmd = new SqlCommand("uspAddContentBeacon", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@pContentId", SqlDbType.Int).Value = contentId;
                        cmd.Parameters.Add("@pBeaconId", SqlDbType.UniqueIdentifier).Value = new Guid(beaconId);

                        SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                        paramResp.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramResp);

                        if (con.State != ConnectionState.Open)
                            con.Open();

                        cmd.ExecuteNonQuery();

                        if (Int32.Parse(paramResp.Value.ToString()) <= 0)
                            return false;
                    }
                }
            }

            return true;
        }
    }
}