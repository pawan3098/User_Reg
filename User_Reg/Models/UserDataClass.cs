using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace User_Reg.Models
{
    public class UserDataClass
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        //To View all User details      
        public IEnumerable<UserClass> GetAllCustomers()
        { 
            List<UserClass> lstUser = new List<UserClass>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    UserClass user = new UserClass();
                    user.FirstName = sdr["Firstname"].ToString();
                    user.LastName = sdr["Lastname"].ToString();
                    user.PhoneNo = sdr["ContactNo"].ToString();
                    user.Email = sdr["EmailID"].ToString();
                    // user.Gender = sdr["Gender"].ToString();
                    
                    
                    lstUser.Add(user);
                }
                con.Close();
            }
            return lstUser;
        }

      
        //To Update the records of a particluar Customer    
        public void UpdateCustomer(UserClass Customer)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", Customer.UserId);
                cmd.Parameters.AddWithValue("@FirstName", Customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", Customer.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", Customer.PhoneNo);
               
                cmd.Parameters.AddWithValue("@EmailId", Customer.Email);
                cmd.Parameters.AddWithValue("@DateOfBirth", Customer.DOB);
                cmd.Parameters.AddWithValue("@CityId", Customer.CityId);
                cmd.Parameters.AddWithValue("@Gender", Customer.Gender);
                cmd.Parameters.AddWithValue("@UserImage", Customer.ProfileImgUrl);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a particular Customer    
        public UserClass GetCustomerData(int? id)
        {
            UserClass Customer = new UserClass();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("sp_GetUserByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", id);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Customer.UserId = (string)sdr["UserID"];
                    Customer.FirstName = sdr["FirstName"].ToString();
                    Customer.LastName = sdr["LastName"].ToString();
                    Customer.Gender = sdr["Gender"].ToString();
                    Customer.PhoneNo = sdr["ContactNo"].ToString();
                    Customer.Email = sdr["MailId"].ToString();
                }
            }
            return Customer;
        }

        //To Delete the record on a particular Customer    
        public void DeleteCustomer(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}