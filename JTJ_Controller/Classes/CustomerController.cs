using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTJ_Entity; // Entity Classes

namespace JTJ_Controller
{
    public class CustomerController
    {
        public Customer objCustomer = new Customer();
    
        public void AddCustomer(int CustNumber, string Name, string Address, string Email, string PhoneNumber)
        {
            objCustomer.AddCustomerJSON(CustNumber, Name, Address, Email, PhoneNumber);
        }
        public void ModifyCustomer(int CustNumber, string Name, string Address, string Email, string PhoneNumber, int ID)
        {
            objCustomer.ModifyCustomerJSON(CustNumber, Name, Address, Email, PhoneNumber, ID);
        }
        public string ViewCustomer()
        {
            return objCustomer.ViewCustomerJSON();
        }
        public Customer SearchCustomer(int ID)
        {
            return objCustomer.SearchCustomerJSON(ID);
        }
        public void DeleteCustomer(int ID)
        {
            objCustomer.DeleteCustomerJSON(ID);
        }
    }
}
