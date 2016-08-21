using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JTJ_Entity
{
    // Payment Object
    public class Payment
    {
        public int Pay_ID { get; set; }
        public int CustNumber { get; set; }
        public int Job_ID { get; set; }
        public string Payment_Amount { get; set; }

        // Location of payment.json
        string FileLocation = @"C:\Users\Chris\documents\visual studio 2013\Projects\SE361_GUI\JTJ_Entity\JSON\payment.json";

        // Function to display the Payment JSON data as a string
        public string toString()
        {
            return "***************************************************************************" + "\nPayment ID: " + Pay_ID + "\n" + "Customer Number: " + CustNumber + "\n" + "Job ID: " + Job_ID + "\n" + "Payment Amount: " + Payment_Amount;
        }

        // Add the payment object in JSON format to the appropriate file
        public void RecordPaymentJSON(int Pay_ID, int CustNumber, int Job_ID, string Payment_Amount)
        {
            // Instantiate the Payment Class and store the values in the object variables
            Payment payment = new Payment();
            payment.Pay_ID = Pay_ID;
            payment.CustNumber = CustNumber;
            payment.Job_ID = Job_ID;
            payment.Payment_Amount = Payment_Amount;

            // Store the Payment Object as a JSON array for easy mapping to a list of Customer Objects 
            List<Payment> paymentList = new List<Payment>();
            paymentList.Add(payment);

            // Convert the Payment Object to a string in JSON format
            string json = JsonConvert.SerializeObject(paymentList, Formatting.Indented);

            // Write the JSON to the appropriate file or create the file and then write to it if it doesn't exist
            if (File.Exists(FileLocation))
            {
                File.AppendAllText(FileLocation, json);

                // Replace the invalid JSON with valid JSON
                string jsonToReplace = File.ReadAllText(FileLocation);
                jsonToReplace = jsonToReplace.Replace("][", ",");
                File.WriteAllText(FileLocation, jsonToReplace);
            }
            else
            {
                File.WriteAllText(FileLocation, json);
            }
        }

        // View the information stored in payment.json for a specific customer
        public string ViewPaymentJSON(int CustNumber)
        {
            List<Payment> payment = new List<Payment>(); // List to store Payment objects

            // Open the file, deserialize the JSON data and store in 'payment' as a list of Customer objects
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                payment = JsonConvert.DeserializeObject<List<Payment>>(json);
            }

            StringBuilder paymentString_Print_All = new StringBuilder();

            // Append all of the Payment objects into one string 
            // in order to print the information for all Payments for a specific customer
            for (int x = 0; x < payment.Count; x++)
            {
                if (payment[x].CustNumber == CustNumber)
                {
                    paymentString_Print_All.Append(payment[x].toString() + "\n");
                }
            }
            return paymentString_Print_All.ToString();
        }

        // Search for a payment by ID i.e. Pay_ID
        public Payment SearchPaymentJSON(int Pay_ID, int CustNumber)
        {
            List<Payment> payment = new List<Payment>();

            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                payment = JsonConvert.DeserializeObject<List<Payment>>(json);
            }

            Payment found_Payment = new Payment();

            for (int x = 0; x < payment.Count; x++)
            {
                if (payment[x].Pay_ID == Pay_ID && payment[x].CustNumber == CustNumber)
                {
                    found_Payment = payment[x];
                    break;
                }
                else
                {
                    found_Payment.Pay_ID = -1;
                }
            }
            return found_Payment;
        }

        // Cancel a payment based on their ID i.e. Pay_ID
        public void CancelPaymentJSON(int Pay_ID, int CustNumber)
        {
            List<Payment> payment = new List<Payment>();

            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                payment = JsonConvert.DeserializeObject<List<Payment>>(json);
            }

            for (int x = 0; x < payment.Count; x++)
            {
                if (payment[x].Pay_ID == Pay_ID && payment[x].CustNumber == CustNumber)
                {
                    payment.RemoveAt(x);
                }
            }

            string json_text = JsonConvert.SerializeObject(payment, Formatting.Indented);

            File.WriteAllText(FileLocation, json_text);

            // Replace the invalid JSON with valid JSON
            string jsonToReplace = File.ReadAllText(FileLocation);
            jsonToReplace = jsonToReplace.Replace("][", ",");
            File.WriteAllText(FileLocation, jsonToReplace);
        }
    }
}
