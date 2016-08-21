using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTJ_Entity; // Entity Classes

namespace JTJ_Controller
{
    public class EmployeeController
    {
        public Employee objEmployee = new Employee();
 
        public void AddEmployee(int ID, string Name, string Address, string Pay_Rate)
        {
            objEmployee.AddEmployeeJSON(ID, Name, Address, Pay_Rate);
        }
        public void ModifyEmployee(int ID, string Name, string Address, string Pay_Rate, int original_ID)
        {
            objEmployee.ModifyEmployeeJSON(ID, Name, Address, Pay_Rate, original_ID);
        }
        public string ViewEmployee()
        {
            return objEmployee.ViewEmployeeJSON();
        }
        public Employee SearchEmployee(int ID)
        {
            return objEmployee.SearchEmployeeJSON(ID);
        }
        public void DeleteEmployee(int ID)
        {
            objEmployee.DeleteEmployeeJSON(ID);
        }
    }
}
