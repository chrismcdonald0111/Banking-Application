using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JTJ_Entity
{
    public class Invoice
    {
        public int invoice_ID { get; set; }
        public bool Job_Type { get; set; }
        public string C_Name { get; set; }
        public string Address { get; set; }
        public string C_Email { get; set; }
        public string C_PhoneNumber { get; set; }
        public string Employee_ID { get; set; }
        public string Total_Price { get; set; }
        public string C_Signature { get; set; } 

        // Location of invoice.json
        string FileLocation = @"C:\Users\Chris\documents\visual studio 2013\Projects\SE361_GUI\JTJ_Entity\JSON\invoice.json";

        // Function to display the Invoice JSON data as a string
        public string toString()
        {
            string jobType = "";
            if (Job_Type == true)
            {
                jobType = "One-Time Job";
            }
            else
            {
                jobType = "Regular Job";
            }
            return "***************************************************************************" + "\nInvoice ID: " + invoice_ID + "\n" + "Job Type: " + jobType + "\n" + "Customer Name: " + C_Name + "\n" +
                "Address: " + Address + "\n" + "Customer Email: " + C_Email + "\n" + "Customer Phone Number: " + C_PhoneNumber + "\nEmployee ID: " + Employee_ID + "\nTotal Price: " + Total_Price + "\nCustomer Signature: " + C_Signature;
        }

        // Add the invoice object in JSON format to the appropriate file
        public void CreateInvoiceJSON(int invoice_ID, bool Job_Type, string C_Name, string Address, string C_Email, string C_PhoneNumber, string Employee_Id, string Total_Price, string C_Signature)
        {
            // Instantiate the Invoice Class and store the values in the object variables
            Invoice invoice = new Invoice();
            invoice.invoice_ID = invoice_ID;
            invoice.Job_Type = Job_Type;
            invoice.C_Name = C_Name;
            invoice.Address = Address;
            invoice.C_Email = C_Email;
            invoice.C_PhoneNumber = C_PhoneNumber;
            invoice.Employee_ID = Employee_ID;
            invoice.Total_Price = Total_Price;
            invoice.C_Signature = C_Signature;

            // Store the Invoice Object as a JSON array for easy mapping to a list of Invoice Objects 
            List<Invoice> invoiceList = new List<Invoice>();
            invoiceList.Add(invoice);

            // Convert the Invoice Object to a string in JSON format
            string json = JsonConvert.SerializeObject(invoiceList, Formatting.Indented);

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

        // Modify the information about an existing invoice
        public void ModifyInvoiceJSON(int invoice_ID, bool Job_Type, string C_Name, string Address, string C_Email, string C_PhoneNumber, string Employee_ID, string Total_Price, string C_Signature, int ID)
        {
            List<Invoice> invoice = new List<Invoice>();

            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                invoice = JsonConvert.DeserializeObject<List<Invoice>>(json);
            }

            for (int x = 0; x < invoice.Count; x++)
            {
                if (invoice[x].invoice_ID == ID)
                {
                    invoice[x].invoice_ID = invoice_ID;
                    invoice[x].Job_Type = Job_Type;
                    invoice[x].C_Name = C_Name;
                    invoice[x].Address = Address;
                    invoice[x].C_Email = C_Email;
                    invoice[x].C_PhoneNumber = C_PhoneNumber;
                    invoice[x].Employee_ID = Employee_ID;
                    invoice[x].Total_Price = Total_Price;
                    invoice[x].C_Signature = C_Signature;
                    break;
                }
            }

            string json_text = JsonConvert.SerializeObject(invoice, Formatting.Indented);

            File.WriteAllText(FileLocation, json_text);

            // Replace the invalid JSON with valid JSON
            string jsonToReplace = File.ReadAllText(FileLocation);
            jsonToReplace = jsonToReplace.Replace("][", ",");
            File.WriteAllText(FileLocation, jsonToReplace);
        }

        // View the information stored in invoice.json
        public string ViewInvoiceJSON()
        {
            List<Invoice> invoice = new List<Invoice>(); // List to store Invoice objects

            // Open the file, deserialize the JSON data and store in 'invoice' as a list of Invoice objects
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                invoice = JsonConvert.DeserializeObject<List<Invoice>>(json);
            }

            StringBuilder invoiceString_Print_All = new StringBuilder();

            // Append all of the Invoice objects into one string 
            // in order to print the information for all Invoices
            for (int x = 0; x < invoice.Count; x++)
            {
                invoiceString_Print_All.Append(invoice[x].toString() + "\n");
            }
            return invoiceString_Print_All.ToString();
        }

        // View invoices for either One-Time jobs or Regular Jobs
        public string ViewInvoiceByJobTypeJSON(bool Job_Type)
        {
            List<Invoice> invoice = new List<Invoice>();

            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                invoice = JsonConvert.DeserializeObject<List<Invoice>>(json);
            }

            StringBuilder invoiceString_Print_All = new StringBuilder();

            for (int x = 0; x < invoice.Count; x++)
            {
                if (invoice[x].Job_Type == Job_Type)
                {
                    invoiceString_Print_All.Append(invoice[x].toString() + "\n");
                }
            }
            return invoiceString_Print_All.ToString();
        }

        // Search for an invoice by ID
        public Invoice SearchInvoiceJSON(int ID)
        {
            List<Invoice> invoice = new List<Invoice>();

            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                invoice = JsonConvert.DeserializeObject<List<Invoice>>(json);
            }

            Invoice found_Invoice = new Invoice();

            for (int x = 0; x < invoice.Count; x++)
            {
                if (invoice[x].invoice_ID == ID)
                {
                    found_Invoice = invoice[x];
                    break;
                }
                else
                {
                    found_Invoice.invoice_ID = -1;
                }
            }
            return found_Invoice;
        }

        // Cancel an invoice based on ID
        public void CancelInvoiceJSON(int ID)
        {
            List<Invoice> invoice = new List<Invoice>();

            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                invoice = JsonConvert.DeserializeObject<List<Invoice>>(json);
            }

            for (int x = 0; x < invoice.Count; x++)
            {
                if (invoice[x].invoice_ID == ID)
                {
                    invoice.RemoveAt(x);
                }
            }

            string json_text = JsonConvert.SerializeObject(invoice, Formatting.Indented);

            File.WriteAllText(FileLocation, json_text);

            // Replace the invalid JSON with valid JSON
            string jsonToReplace = File.ReadAllText(FileLocation);
            jsonToReplace = jsonToReplace.Replace("][", ",");
            File.WriteAllText(FileLocation, jsonToReplace);
        }
    }
}
