using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JTJ_Entity
{
    // Customer Object
    public class Customer
    {
        public int CustNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Location of customer.json
        string FileLocation = @"C:\Users\Chris\documents\visual studio 2013\Projects\SE361_GUI\JTJ_Entity\JSON\customer.json";

        // Function to display the Customer JSON data as a string
        public string toString()
        {
            return "***************************************************************************" + "\nCustomer Number: " + CustNumber + "\n" + "Name: " + Name + "\n" +
                "Address: " + Address + "\n" + "Email: " + Email + "\n" + "Phone Number: " + PhoneNumber;
        }

        // Add the customer object in JSON format to the appropriate file
        public void AddCustomerJSON(int CustNumber, string Name, string Address, string Email, string PhoneNumber)
        {
            // Instantiate the Customer Class and store the values in the object variables
            Customer customer = new Customer();
            customer.CustNumber = CustNumber;
            customer.Name = Name;
            customer.Address = Address;
            customer.Email = Email;
            customer.PhoneNumber = PhoneNumber;

            // Store the Customer Object as a JSON array for easy mapping to a list of Customer Objects 
            List<Customer> customerList = new List<Customer>();
            customerList.Add(customer);

            // Convert the Customer Object to a string in JSON format
            string json = JsonConvert.SerializeObject(customerList, Formatting.Indented);
            
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

        // Modify the information about an existing customer
        public void ModifyCustomerJSON(int CustNumber, string Name, string Address, string Email, string PhoneNumber, int ID)
        {
            List<Customer> customer = new List<Customer>(); 

            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                customer = JsonConvert.DeserializeObject<List<Customer>>(json);
            }

            for (int x = 0; x < customer.Count; x++)
            {
                if (customer[x].CustNumber == ID)
                {
                    customer[x].CustNumber = CustNumber;
                    customer[x].Name = Name;
                    customer[x].Address = Address;
                    customer[x].Email = Email;
                    customer[x].PhoneNumber = PhoneNumber;
                    break;
                }
            }

            string json_text = JsonConvert.SerializeObject(customer, Formatting.Indented);

            File.WriteAllText(FileLocation, json_text);

            // Replace the invalid JSON with valid JSON
            string jsonToReplace = File.ReadAllText(FileLocation);
            jsonToReplace = jsonToReplace.Replace("][", ",");
            File.WriteAllText(FileLocation, jsonToReplace);
        }

        // View the information stored in customer.json
        public string ViewCustomerJSON()
        {
            List<Customer> customer = new List<Customer>(); // List to store Customer objects

            // Open the file, deserialize the JSON data and store in 'customer' as a list of Customer objects
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                customer = JsonConvert.DeserializeObject<List<Customer>>(json);
            }

            StringBuilder customerString_Print_All = new StringBuilder();

            // Append all of the Customer objects into one string 
            // in order to print the information for all Customers
            for (int x = 0; x < customer.Count; x++)
            {
                customerString_Print_All.Append(customer[x].toString() + "\n");
            }
            return customerString_Print_All.ToString();
        }

        // Search for a customer by their ID i.e. Customer Number
        public Customer SearchCustomerJSON(int ID)
        {   
            List<Customer> customer = new List<Customer>(); 
            
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                customer = JsonConvert.DeserializeObject<List<Customer>>(json);
            }

            Customer found_Customer = new Customer();
            
            for (int x = 0; x < customer.Count; x++)
            {
                if (customer[x].CustNumber == ID)
                {
                    found_Customer = customer[x];
                    break;
                }
                else
                {
                    found_Customer.CustNumber = -1;
                }
            }
            return found_Customer;
        }

        // Delete a customer based on their ID i.e. Customer Number
        public void DeleteCustomerJSON(int ID)
        {
            List<Customer> customer = new List<Customer>(); 
            
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                customer = JsonConvert.DeserializeObject<List<Customer>>(json);
            }
            
            for (int x = 0; x < customer.Count; x++)
            {
                if (customer[x].CustNumber == ID)
                {
                    customer.RemoveAt(x);
                }
            }
            
            string json_text = JsonConvert.SerializeObject(customer, Formatting.Indented);

            File.WriteAllText(FileLocation, json_text);

            // Replace the invalid JSON with valid JSON
            string jsonToReplace = File.ReadAllText(FileLocation);
            jsonToReplace = jsonToReplace.Replace("][", ",");
            File.WriteAllText(FileLocation, jsonToReplace);
        }
    }
}
