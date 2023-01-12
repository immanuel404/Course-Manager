using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace DAL
{
    public class DataAccessLayer
    {
        string message = "";

        //CONNECT TO DB.
        static string connString = "Data Source=LAPTOP-H44QJADV\\SQLEXPRESS;Initial Catalog=progPoeDB;Integrated Security=True";
        SqlConnection dbConn = new SqlConnection(connString);
        SqlCommand dbComm;
        SqlDataAdapter dbAdapter;
        DataTable dt;


        //===================================== USER CLASS ========================================

        //REGISTER USER
        public string Registration(User user)
        {
            //HASH PASSWORD
            byte[] byteArray = Encoding.UTF8.GetBytes(user.Password);
            MemoryStream stream = new MemoryStream(byteArray);
            var md5 = new MD5CryptoServiceProvider();
            var hashedPassword = md5.ComputeHash(stream);

            dbConn.Open();
            dbComm = new SqlCommand("sp_CheckUser", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            dbComm.Parameters.AddWithValue("@username", user.Username);
            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                message = "Username already taken.";
            }
            else
            {
                dbConn.Close();
                dbConn.Open();
                dbComm = new SqlCommand("sp_InsertUser", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;
                dbComm.Parameters.AddWithValue("@username", user.Username);
                dbComm.Parameters.AddWithValue("@password", hashedPassword);
                dbComm.ExecuteNonQuery();
                message = "Registration Successful, now login.";
            }
            dbConn.Close();
            return message;
        }

        //LOGIN USER
        public string Login(User user)
        {
            //COMPARE HASH PASSWORD
            byte[] byteArray = Encoding.UTF8.GetBytes(user.Password);
            MemoryStream stream = new MemoryStream(byteArray);
            var md5 = new MD5CryptoServiceProvider();
            var hashedPassword = md5.ComputeHash(stream);

            dbConn.Open();
            dbComm = new SqlCommand("sp_VerifyUser", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            dbComm.Parameters.AddWithValue("@username", user.Username);
            dbComm.Parameters.AddWithValue("@password", hashedPassword);
            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                message = dt.Rows[0]["userID"].ToString();
            }
            else
            {
                message = "";
            }
            dbConn.Close();
            return message;
        }



        //===================================== MODULE CLASS ========================================

        //CALC.SELF-STUDY-HRS
        public double CalculateSelftStudyHrs(Module module)
        {
            double SelfStudyHrs = ((module.Credits * 10) / module.NumOfWeeks) - module.ClassHrsWeek;
            return SelfStudyHrs;
        }

        //STORE MODULES DATA
        public int StoreModule(Module module)
        {
            dbConn.Open();
            dbComm = new SqlCommand("sp_InsertModule", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            dbComm.Parameters.AddWithValue("@code", module.Code);
            dbComm.Parameters.AddWithValue("@name", module.Name);
            dbComm.Parameters.AddWithValue("@credits", module.Credits);
            dbComm.Parameters.AddWithValue("@classHrsWeek", module.ClassHrsWeek);
            dbComm.Parameters.AddWithValue("@numOfWeeks", module.NumOfWeeks);
            dbComm.Parameters.AddWithValue("@startDate", module.StartDate);
            dbComm.Parameters.AddWithValue("@selfStudyHrs", Convert.ToInt32(CalculateSelftStudyHrs(module)));
            dbComm.Parameters.AddWithValue("@userID", module.UserID);
            dbComm.Parameters.AddWithValue("@assignDayOfWeek", module.AssignDayOfWeek);
            int x = dbComm.ExecuteNonQuery();
            dbConn.Close();
            return x;
        }

        //GET MODULES DATA
        public DataTable GetModules(Module module)
        {
            dbConn.Open();
            dbComm = new SqlCommand("sp_GetModule", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            dbComm.Parameters.AddWithValue("@userID", module.UserID);
            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            dbConn.Close();
            return dt;
        }



        //===================================== WORKING-HRS CLASS ========================================

        //STORE WORKING-HRS
        public int StoreWorkingHrs(WorkingHrs workinghrs)
        {
            dbConn.Open();
            dbComm = new SqlCommand("sp_InsertWorkingHrs", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            dbComm.Parameters.AddWithValue("@moduleName", workinghrs.ModuleName);
            dbComm.Parameters.AddWithValue("@hoursSpent", workinghrs.HoursSpent);
            dbComm.Parameters.AddWithValue("@date", workinghrs.Date);
            dbComm.Parameters.AddWithValue("@selfStudyHrs", workinghrs.SelfStudyHrs);
            dbComm.Parameters.AddWithValue("@userID", workinghrs.UserID);
            int x = dbComm.ExecuteNonQuery();
            dbConn.Close();
            return x;
        }

        //GET WORKING-HRS
        public DataTable GetWorkingHrs(WorkingHrs workinghrs)
        {
            dbConn.Open();
            dbComm = new SqlCommand("sp_GetWorkingHrs", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            dbComm.Parameters.AddWithValue("@userID", workinghrs.UserID);
            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            dbConn.Close();
            return dt;
        }
    }
}