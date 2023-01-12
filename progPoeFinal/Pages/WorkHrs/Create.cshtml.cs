using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL;
using DAL;

namespace progPoeFinal.Pages.WorkHrs
{
    public class CreateModel : PageModel
    {
        BusinessLogicLayer bll = new BusinessLogicLayer();
        public string userID = "";
        public string Message = "";


        //SUBMIT WORK-HRS-INFO
        public void OnPost()
        {
            userID = Request.Query["id"];

            WorkingHrs obj = new WorkingHrs();
            String[] moduleData = Request.Form["moduleName"].ToString().Split(',');
            obj.ModuleName = moduleData[0];
            obj.HoursSpent = Convert.ToInt32(Request.Form["hoursSpent"]);
            obj.Date = Request.Form["date"];
            obj.SelfStudyHrs = Convert.ToInt32(moduleData[1]);
            obj.UserID = userID;

            //CHECK INPUT
            if (obj.ModuleName.Length == 0 || obj.HoursSpent == 0 || obj.Date.Length == 0 || obj.SelfStudyHrs == 0 || obj.UserID.Length == 0)
            {
                Message = "Please, enter all fields.";
            }
            else
            {
                try
                {
                    //STORE WORK-HR DATA
                    int x = bll.StoreWorkingHrs(obj);
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


        //MODULES-INFO FOR DROPDOWN
        public List<Module> listModule = new List<Module>();
        public void OnGet()
        {
            userID = Request.Query["id"];
            try
            {
                Module obj = new Module();
                obj.UserID = userID;
                var info = bll.GetModules(obj);
                for (int i = 0; i < info.Rows.Count; i++)
                {
                    Module newObj = new Module();
                    newObj.Name = info.Rows[i]["name"].ToString();
                    newObj.SelfStudyHrs = Convert.ToInt32(info.Rows[i]["selfStudyHrs"]);
                    listModule.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}