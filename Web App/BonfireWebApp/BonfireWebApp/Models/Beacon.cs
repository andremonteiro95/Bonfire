using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BonfireWebApp.Models
{
    public class Beacon
    {
        [Key]
        [Display(Name="Unique Identifier")]
        public string uuid { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name="Descriptive Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        [Display(Name="Location")]
        public string Localization { get; set; }
    }

    public class BeaconDBContext : IDisposable
    {
        public BeaconDBContext()
        {

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<Beacon> GetAllBeacons()
        {
            List<Beacon> list = new List<Beacon>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspSelectAllBeacons", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Beacon user = new Beacon();
                        user.uuid = reader["uuid"].ToString();
                        user.Name = reader["Name"].ToString();
                        user.Localization = reader["Localization"].ToString();
                        list.Add(user);
                    }
                }
            }

            return list;
        }

        public Beacon GetBeaconById(string uuid)
        {
            Beacon beacon = new Beacon();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspSelectBeacon", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pUuid", SqlDbType.UniqueIdentifier).Value = new Guid(uuid);

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        beacon.uuid = uuid;
                        beacon.Name = reader["Name"].ToString();
                        beacon.Localization = reader["Localization"].ToString();
                    }
                }
            }

            return beacon;
        }

        public bool AddNewBeacon(Beacon beacon)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspAddBeacon", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pUuid", SqlDbType.UniqueIdentifier).Value = new Guid(beacon.uuid);
                    cmd.Parameters.Add("@pName", SqlDbType.VarChar).Value = beacon.Name;
                    cmd.Parameters.Add("@pLocalization", SqlDbType.VarChar).Value = beacon.Localization;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return Int32.Parse(paramResp.Value.ToString()) == 1;
                }
            }
        }

        public bool EditBeacon(Beacon beacon)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspEditBeacon", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pUuid", SqlDbType.UniqueIdentifier).Value = new Guid(beacon.uuid);
                    cmd.Parameters.Add("@pName", SqlDbType.VarChar).Value = beacon.Name;
                    cmd.Parameters.Add("@pLocalization", SqlDbType.VarChar).Value = beacon.Localization;

                    SqlParameter paramResp = new SqlParameter("@response", SqlDbType.Int);
                    paramResp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramResp);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return Int32.Parse(paramResp.Value.ToString()) == 1;
                }
            }
        }

        public bool DeleteBeacon(string uuid)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspDeleteBeacon", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pUuid", SqlDbType.UniqueIdentifier).Value = new Guid(uuid);

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