using CodeX.Core.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CodeX.Core.DAL
{
    public class PersonalDAL
    {
        public string ConnectionString { get; set; }

        public PersonalDAL()
        {
            var builder = new ConfigurationBuilder();
            var configuration = builder.Build();
            ConnectionString = configuration["connectionString"];
        }
        public Result PersonalSave(Personal personal)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Personal_Insert";
                        cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 50).Value = personal.FullName;
                        cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = personal.DateOfBirth;
                        cmd.Parameters.Add("@Gender", SqlDbType.Int).Value = personal.Gender;
                        cmd.Parameters.Add("@MobileNumber", SqlDbType.NVarChar, 10).Value = personal.MobileNumber;

                        int result = cmd.ExecuteNonQuery();

                        if (result >= 0)
                            return new Result { Status = true, Message = "Success" };
                        else
                            return new Result { Status = false, Message = "Failed" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Result { Status = false, Message = "Failed", Exception = ex };
            }
        }
    }
}
