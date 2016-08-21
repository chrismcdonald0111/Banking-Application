using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JTJ_Entity
{
    // Employee Object
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Pay_Rate { get; set; }
        
        // Location of employee.json
        string FileLocation = @"C:\Users\Chris\documents\visual studio 2013\Projects\SE361_GUI\JTJ_Entity\JSON\employee.json";
        
        // Function to display the Employee JSON data as a string
        public string toString()
        {
            return "***************************************************************************" + "\nID: " + ID + "\n" + "Name: " + Name + "\n" + "Address: " +
                Address + "\n" + "Pay Rate: " + Pay_Rate;
        }
  
        // Add the employee object in JSON format to the appropriate file
        public void AddEmployeeJSON(int ID, string Name, string Address, string Pay_Rate)
        {
            // Instantiate the Employee Class and store the values in the object variables 
            Employee employee = new Employee();
            employee.ID = ID;
            employee.Name = Name;
            employee.Address = Address;
            employee.Pay_Rate = Pay_Rate;
            
            // Store the Employee Object as a JSON array for easy mapping to a list of Employee Objects 
            List<Employee> employeeList = new List<Employee>();
            employeeList.Add(employee);

            // Convert the Employee Object to a string in JSON format
            string json = JsonConvert.SerializeObject(employeeList, Formatting.Indented);
            
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

        // Modify the information about an existing employee
        public void ModifyEmployeeJSON(int ID, string Name, string Address, string Pay_Rate, int original_ID)
        {
            List<Employee> employee = new List<Employee>();
            
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                employee = JsonConvert.DeserializeObject<List<Employee>>(json);
            }
            
            for (int x = 0; x < employee.Count; x++)
            {
                if (employee[x].ID == original_ID)
                {
                    employee[x].ID = ID;
                    employee[x].Name = Name;
                    employee[x].Address = Address;
                    employee[x].Pay_Rate = Pay_Rate;
                    break;
                }
            }

            string json_text = JsonConvert.SerializeObject(employee, Formatting.Indented);

            File.WriteAllText(FileLocation, json_text);

            // Replace the invalid JSON with valid JSON
            string jsonToReplace = File.ReadAllText(FileLocation);
            jsonToReplace = jsonToReplace.Replace("][", ",");
            File.WriteAllText(FileLocation, jsonToReplace);
        }

        // View the information stored in employee.json
        public string ViewEmployeeJSON()
        {
            List<Employee> employee = new List<Employee>(); // List to store Employee objects
            
            // Open the file, deserialize the JSON data and store in 'employee' as a list of Employee objects
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                employee = JsonConvert.DeserializeObject<List<Employee>>(json);
            }
 
            StringBuilder employeeString_Print_All = new StringBuilder();
            
            // Append all of the Employee objects into one string 
            // in order to print the information for all Employees
            for(int x = 0; x < employee.Count; x++)
            {
                employeeString_Print_All.Append(employee[x].toString() + "\n");
            }
            return employeeString_Print_All.ToString();
        }

        // Search for an employee by their ID
        public Employee SearchEmployeeJSON(int ID)
        {
            List<Employee> employee = new List<Employee>(); 
            
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                employee = JsonConvert.DeserializeObject<List<Employee>>(json);
            }

            Employee found_Employee = new Employee();
            
            for (int x = 0; x < employee.Count; x++)
            {
                if (employee[x].ID == ID)
                {
                    found_Employee = employee[x];
                    break;
                }
                else
                {
                    found_Employee.ID = -1;
                }
            }
            return found_Employee;
        }

        // Delete an employee based on their ID
        public void DeleteEmployeeJSON(int ID)
        {
            List<Employee> employee = new List<Employee>();
            
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                employee = JsonConvert.DeserializeObject<List<Employee>>(json);
            }

            for (int x = 0; x < employee.Count; x++)
            {
                if (employee[x].ID == ID)
                {
                    employee.RemoveAt(x);
                }
            }
            
            string json_text = JsonConvert.SerializeObject(employee, Formatting.Indented);

            File.WriteAllText(FileLocation, json_text);

            // Replace the invalid JSON with valid JSON
            string jsonToReplace = File.ReadAllText(FileLocation);
            jsonToReplace = jsonToReplace.Replace("][", ",");
            File.WriteAllText(FileLocation, jsonToReplace);
        }
    }
}
