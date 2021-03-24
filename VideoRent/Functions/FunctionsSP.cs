using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VideoRent.Data;
using VideoRent.Models;

namespace VideoRent.Functions
{
    public static class FunctionsSP
    {

        public static void SaveNewCustomer(ApplicationDbContext context, Customer customer)
        {
            ApplicationDbContext _context = context;

            var exec = _context.Database.ExecuteSqlCommand("spAddCustomer @Name, " +
                    "@MembershipType, @Birthdate, @IsSubscribedToNewsletter",

                 new SqlParameter("@Name", customer.Name),
                 new SqlParameter("@MembershipType", customer.MembershipTypeId),
                 new SqlParameter("@Birthdate", customer.Birthdate),
                 new SqlParameter("@IsSubscribedToNewsletter", customer.IsSubscribedToNewsletter)
                 );
        }

        public static int ReturnNewCustomerId(Customer customer)
        {
            int newCustomerId;

            String strConnString = ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spReturnCustomerId";
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = customer.Name;
            cmd.Parameters.Add("@MembershipType", SqlDbType.Int).Value = customer.MembershipTypeId;
            cmd.Parameters.Add("@Birthdate", SqlDbType.DateTime).Value = customer.Birthdate;
            cmd.Parameters.Add("@IsSubscribedToNewsletter", SqlDbType.VarChar).Value = customer.IsSubscribedToNewsletter;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                string id = cmd.Parameters["@Id"].Value.ToString();
                newCustomerId = Convert.ToInt32(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return newCustomerId;
        }
    }
}
