using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL;
using DAL;

namespace progPoeFinal.Pages.WorkHrs
{
    public class IndexModel : PageModel
    {
        public List<WorkingHrs> listWorkHrs = new List<WorkingHrs>();
        public string userID = "";
        BusinessLogicLayer bll = new BusinessLogicLayer();

        public void OnGet()
        {
            userID = Request.Query["id"];
            try
            {
                WorkingHrs obj = new WorkingHrs();
                obj.UserID = userID;
                var info = bll.GetWorkingHrs(obj);
                for (int i = 0; i < info.Rows.Count; i++)
                {
                    WorkingHrs newObj = new WorkingHrs();
                    newObj.ModuleName= info.Rows[i]["moduleName"].ToString();
                    newObj.HoursSpent = Convert.ToInt32(info.Rows[i]["HoursSpent"]);
                    newObj.Date = info.Rows[i]["date"].ToString();
                    newObj.SelfStudyHrs = Convert.ToInt32(info.Rows[i]["selfStudyHrs"]);

                    listWorkHrs.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
