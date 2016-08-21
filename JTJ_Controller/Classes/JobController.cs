using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTJ_Entity; // Entity Classes

namespace JTJ_Controller
{
    public class JobController
    {
        public Job objJob = new Job();

        public void AddJob(int JobNumber, bool Complete, string Address, string Date, string Start_Time, string Length, string Price, string Amount_Paid, string Amount_ToPay)
        {
            objJob.AddJobJSON(JobNumber, Complete, Address, Date, Start_Time, Length, Price, Amount_Paid, Amount_ToPay);
        }
        public void ModifyJob(int JobNumber, bool Complete, string Address, string Date, string Start_Time, string Length, string Price, string Amount_Paid, string Amount_ToPay, int ID)
        {
            objJob.ModifyJobJSON(JobNumber, Complete, Address, Date, Start_Time, Length, Price, Amount_Paid, Amount_ToPay, ID);
        }
        public string ViewJob()
        {
            return objJob.ViewJobJSON();
        }
        public string ViewJobByStatus(bool Job_Status)
        {
            return objJob.ViewJobByStatusJSON(Job_Status);
        }
        public Job SearchJob(int ID)
        {
            return objJob.SearchJobJSON(ID);
        }
        public void DeleteJob(int ID)
        {
            objJob.DeleteJobJSON(ID);
        }
    }
}
