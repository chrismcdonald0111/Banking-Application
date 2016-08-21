using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTJ_Entity; // Entity Classes

namespace JTJ_Controller
{
    public class InvoiceController
    {
        public Invoice objInvoice = new Invoice();

        public void CreateInvoice(int invoice_ID, bool Job_Type, string C_Name, string Address, string C_Email, string C_PhoneNumber, string Employee_ID, string Total_Price, string C_Signature)
        {
            objInvoice.CreateInvoiceJSON(invoice_ID, Job_Type, C_Name, Address, C_Email, C_PhoneNumber, Employee_ID, Total_Price, C_Signature);
        }
        public void ModifyInvoice(int invoice_ID, bool Job_Type, string C_Name, string Address, string C_Email, string C_PhoneNumber, string Employee_ID, string Total_Price, string C_Signature, int ID)
        {
            objInvoice.ModifyInvoiceJSON(invoice_ID, Job_Type, C_Name, Address, C_Email, C_PhoneNumber, Employee_ID, Total_Price, C_Signature, ID);
        }
        public string ViewInvoice()
        {
            return objInvoice.ViewInvoiceJSON();
        }
        public string ViewInvoiceByJobType(bool Job_Type)
        {
            return objInvoice.ViewInvoiceByJobTypeJSON(Job_Type);
        }
        public Invoice SearchInvoice(int ID)
        {
            return objInvoice.SearchInvoiceJSON(ID);
        }
        public void CancelInvoice(int ID)
        {
            objInvoice.CancelInvoiceJSON(ID);
        }
    }
}
