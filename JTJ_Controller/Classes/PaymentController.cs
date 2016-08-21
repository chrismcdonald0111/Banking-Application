using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTJ_Entity; // Entity Classes

namespace JTJ_Controller
{
    public class PaymentController
    {
        public Payment objPayment = new Payment();

        public void RecordPayment(int Pay_ID, int CustNumber, int Job_ID, string Payment_Amount)
        {
            objPayment.RecordPaymentJSON(Pay_ID, CustNumber, Job_ID, Payment_Amount);
        }
        public string ViewPayment(int CustNumber)
        {
            return objPayment.ViewPaymentJSON(CustNumber);
        }
        public Payment SearchPayment(int Pay_ID, int CustNumber)
        {
            return objPayment.SearchPaymentJSON(Pay_ID, CustNumber);
        }
        public void CancelPayment(int Pay_ID, int CustNumber)
        {
            objPayment.CancelPaymentJSON(Pay_ID, CustNumber);
        }
    }
}
