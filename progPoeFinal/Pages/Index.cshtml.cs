using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL;
using DAL;

namespace progPoeFinal.Pages
{
    public class IndexModel : PageModel
    {
        //------------------ DEFAULT -------------------
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        //----------------------------------------------


        BusinessLogicLayer bll = new BusinessLogicLayer();
        User objUser = new User();
        public string userID = "";
        public string Message = "";
        public string willRedirect = "";


        //ON-REGISTER
        public void OnPostRegister()
        {
            objUser.Username = Request.Form["username"];
            objUser.Password = Request.Form["password"];
            //CHECK INPUT
            if (objUser.Username.Length == 0 || objUser.Password.Length == 0)
            {
                Message = "Please, enter all fields.";
            }
            else
            {
                try
                {
                    //REGISTER USER
                    Message = bll.Registration(objUser);
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }
        }


        //ON-LOGIN
        public void OnPostLogin()
        {
            objUser.Username = Request.Form["username"];
            objUser.Password = Request.Form["password"];
            //CHECK INPUT
            if (objUser.Username.Length == 0 || objUser.Password.Length == 0)
            {
                Message = "Please, enter all fields.";
            }
            else
            {
                try
                {
                    //LOGIN USER
                    userID = bll.Login(objUser);
                    if (userID == "")
                    {
                        Message = "Incorrect, try again.";
                    }
                    else
                    {
                        Console.WriteLine("LoggedIn: " + userID);
                        willRedirect = userID.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }
        }
    }
}