using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User_Reg.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;



namespace User_Reg.Controllers
{


    public class UserController : Controller
    {
        string maincon = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        UserDataClass objUser = new UserDataClass();
        // GET: User
        public ActionResult Index()
        {
            UserClass UserInfo = new UserClass();
            UserInfo.CityRecords = GetCityMasters();
            return View(UserInfo);
        }

        
        [HttpPost]
       
        public ActionResult Index(UserClass uc, HttpPostedFileBase Ufile)
        {
            UserClass mod = new UserClass();
            SqlConnection sqlConnect = new SqlConnection(maincon);
            string sqlquery = "insert into [dbo].[Registration2] (FirstName,LastName,DateOfBirth,EmailID,Gender,ContactNo,UserImage,CityId) values (@FirstName,@LastName,@DOB,@Email,@Gender,@PhoneNo,@ProfileImgUrl,@CityId)";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlConnect);
            sqlConnect.Open();
            sqlcomm.Parameters.AddWithValue("@FirstName", uc.FirstName);
            sqlcomm.Parameters.AddWithValue("@LastName", uc.LastName);
            sqlcomm.Parameters.AddWithValue("@DOB", uc.DOB);
            sqlcomm.Parameters.AddWithValue("@Email", uc.Email);
            sqlcomm.Parameters.AddWithValue("@Gender", uc.Gender);
            sqlcomm.Parameters.AddWithValue("@CityId", uc.CityId);
            sqlcomm.Parameters.AddWithValue("@PhoneNo", uc.PhoneNo);

            if (Ufile != null && Ufile.ContentLength > 0)
            {
                string filename = Path.GetFileName(Ufile.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/UserImages/"), filename);
                Ufile.SaveAs(imgpath);
            }

            sqlcomm.Parameters.AddWithValue("@ProfileImgUrl", "~/UserImages/" + Ufile.FileName);
            sqlcomm.ExecuteNonQuery();
            sqlConnect.Close();

            mod.CityRecords = GetCityMasters();
            ViewData["Message"] = "User Record " + uc.FirstName + " is saved Successfully ";
            return View(mod);
        }
   

        public List<CityMaster> GetCityMasters()
        {
            List<CityMaster> rvar = new List<CityMaster>();
            DataTable CityList = new DataTable();

            string sqlQery = "select * from [dbo].[City]";
            SqlConnection sqlConnect = new SqlConnection(maincon);
            sqlConnect.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlQery, sqlConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlcomm);
            da.Fill(CityList);
            sqlConnect.Close();
            da.Dispose();
            rvar = (from DataRow dr in CityList.Rows
                                    select new CityMaster()
                                    {
                                        CityId = Convert.ToInt32(dr["Id_city"]),
                                        CityName = dr["city"].ToString()
                                    }).ToList();
            return rvar;
        }

        public ActionResult DisplayAll()
        {
            List<UserClass> users = new List<UserClass>();
            users = objUser.GetAllCustomers().ToList();
            return View(users);
        }


        public ActionResult EditUser(int? id)
        {
           /* if (id == null)
            {
                return HttpNotFound();
            }*/
            UserClass user = objUser.GetCustomerData(id);
            /*
            if (user == null)
            {
                return HttpNotFound();
            }*/
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(int id, [Bind] UserClass cust)
        {
            int userId = ConvertToInt32(cust.UserId);
            if (id != userId)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                objUser.UpdateCustomer(cust);
                return RedirectToAction("DisplayAll");
            }
            return View(objUser);
        }

        private int ConvertToInt32(string userId)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            UserClass objcustomer = objUser.GetCustomerData(id);

            if (objcustomer == null)
            {
                return HttpNotFound();
            }
            return View(objcustomer);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            objUser.DeleteCustomer(id);
            return RedirectToAction("Index");
        }

        public ActionResult AboutCompany()
        {
            return View();
        }
    }
}