using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL;
using DAL;

namespace progPoeFinal.Pages.Modules
{
    public class IndexModel : PageModel
    {
        public string userID = ""; 
        public List<Module> listModule = new List<Module>();      
        BusinessLogicLayer bll = new BusinessLogicLayer();
        //MODULE FOR DAY
        public string moduleToday = "";
        DayOfWeek wk = DateTime.Today.DayOfWeek;


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
                    newObj.Code = info.Rows[i]["code"].ToString();
                    newObj.Name = info.Rows[i]["name"].ToString();
                    newObj.Credits = Convert.ToInt32(info.Rows[i]["credits"]);                  
                    newObj.ClassHrsWeek = Convert.ToInt32(info.Rows[i]["classHrsWeek"]);
                    newObj.NumOfWeeks = Convert.ToInt32(info.Rows[i]["numOfWeeks"]);
                    newObj.StartDate = info.Rows[i]["startDate"].ToString();
                    newObj.SelfStudyHrs = Convert.ToInt32(info.Rows[i]["selfStudyHrs"]);
                    newObj.AssignDayOfWeek = info.Rows[i]["assignDayOfWeek"].ToString();
                    
                    listModule.Add(newObj);
                }
                CheckDailyModule();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        //CHECK->ASSIGNED MODULE FOR DAY
        public void CheckDailyModule()
        {
            foreach (var item in listModule)
            {
                string raw = Convert.ToString(item.AssignDayOfWeek);
                string dbDay = raw.Replace(" ", String.Empty);
                if (dbDay == wk.ToString())
                {
                    moduleToday = item.Name.ToString();
                }
            }
        }
    }
}
