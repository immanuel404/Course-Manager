using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL;
using DAL;

namespace progPoeFinal.Pages.Modules
{
    public class CreateModel : PageModel
    {
        BusinessLogicLayer bll = new BusinessLogicLayer();
        public string userID = "";
        public string Message = "";
        
        
        //SUBMIT MODULE-INFO
        public void OnPost()
        {
            userID = Request.Query["id"];

            Module obj = new Module();
            obj.Code = Request.Form["code"];
            obj.Name = Request.Form["name"];
            obj.Credits = Convert.ToInt32(Request.Form["credits"]);
            obj.ClassHrsWeek = Convert.ToInt32(Request.Form["classHrsWeek"]);
            obj.NumOfWeeks = Convert.ToInt32(Request.Form["numOfWeeks"]);
            obj.StartDate = Request.Form["startDate"];
            obj.UserID = userID;
            obj.AssignDayOfWeek = Request.Form["assignDayOfWeek"];

            //CHECK INPUT
                if (obj.Code.Length == 0 || obj.Name.Length == 0 || obj.Credits == 0 || obj.ClassHrsWeek == 0 || obj.NumOfWeeks == 0 || obj.StartDate.Length == 0 || obj.UserID.Length == 0)
            {
                Message = "Please, enter all fields.";
            }
            else
            {
                try
                {
                    //STORE NEW MODULE
                    int x = bll.StoreModule(obj);
                    if (x > 0)
                    {
                        Message = "Success!";
                    }
                    else
                    {
                        Message = "Failed!";
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
