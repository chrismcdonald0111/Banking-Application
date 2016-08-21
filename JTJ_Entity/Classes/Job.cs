using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JTJ_Entity
{
    // Job Object
    public class Job
    {
        public int JobNumber { get; set; }
        public bool Complete { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
        public string Start_Time { get; set; }
        public string Length { get; set; }
        public string Price { get; set; }
        public string Amount_Paid { get; set; }
        public string Amount_ToPay { get; set; }

        // Location of job.json
        string FileLocation = @"C:\Users\Chris\documents\visual studio 2013\Projects\SE361_GUI\JTJ_Entity\JSON\job.json";

        // Function to display the Job JSON data as a string
        public string toString()
        {
            // Determine the correct string for the job status based on the completion status
            string jobStatus = "";
            if (Complete == true)
            {
                jobStatus = "Completed";
            }
            else
            {
                jobStatus = "In Progress";
            }
            return "**********************************************************************" + "\nJob Number: " + JobNumber + "\n" + "Completion Status: " + jobStatus + "\n" +
                "Address: " + Address + "\n" + "Date: " + Date + "\n" + "Start Time: " + Start_Time + "\n" +
                    "Length: " + Length + "\n" + "Price: " + Price + "\n" +
                    "Amount of Deposit Paid: " + Amount_Paid + "\n" + "Amount of Balance Due: " + Amount_ToPay;
        }

        // Add the job object in JSON format to the appropriate file
        public void AddJobJSON(int JobNumber, bool Complete, string Address, string Date, string Start_Time, string Length, string Price, string Amount_Paid, string Amount_ToPay)
        {
            // Instantiate the Job Class and store the values in the object variables
            Job job = new Job();
            job.JobNumber = JobNumber;
            job.Complete = Complete;
            job.Address = Address;
            job.Date = Date;
            job.Start_Time = Start_Time;
            job.Length = Length;
            job.Price = Price;
            job.Amount_Paid = Amount_Paid;
            job.Amount_ToPay = Amount_ToPay;

            // Store the Job Object as a JSON array for easy mapping to a list of Job Objects 
            List<Job> jobList = new List<Job>();
            jobList.Add(job);

            // Convert the Job Object to a string in JSON format
            string json = JsonConvert.SerializeObject(jobList, Formatting.Indented);
            
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

        // Modify the information about an existing job
        public void ModifyJobJSON(int JobNumber, bool Complete, string Address, string Date, string Start_Time, string Length, string Price, string Amount_Paid, string Amount_ToPay, int ID)
        {
            List<Job> job = new List<Job>(); 
            
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                job = JsonConvert.DeserializeObject<List<Job>>(json);
            }
            
            for (int x = 0; x < job.Count; x++)
            {
                if (job[x].JobNumber == JobNumber)
                {
                    job[x].JobNumber = JobNumber;
                    job[x].Complete = Complete;
                    job[x].Address = Address;
                    job[x].Date = Date;
                    job[x].Start_Time = Start_Time;
                    job[x].Length = Length;
                    job[x].Price = Price;
                    job[x].Amount_Paid = Amount_Paid;
                    job[x].Amount_ToPay = Amount_ToPay;
                    break;
                }
            }
            
            string json_text = JsonConvert.SerializeObject(job, Formatting.Indented);

            File.WriteAllText(FileLocation, json_text);

            // Replace the invalid JSON with valid JSON
            string jsonToReplace = File.ReadAllText(FileLocation);
            jsonToReplace = jsonToReplace.Replace("][", ",");
            File.WriteAllText(FileLocation, jsonToReplace);
        }

        // View the information stored in job.json
        public string ViewJobJSON()
        {
            List<Job> job = new List<Job>(); // List to store Job objects

            // Open the file, deserialize the JSON data and store in 'job' as a list of Job objects
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                job = JsonConvert.DeserializeObject<List<Job>>(json);
            }

            StringBuilder jobString_Print_All = new StringBuilder();

            // Append all of the Job objects into one string 
            // in order to print the information for all Jobs
            for (int x = 0; x < job.Count; x++)
            {
                jobString_Print_All.Append(job[x].toString() + "\n");
            }
            return jobString_Print_All.ToString();
        }

        // View either Completed Jobs or Pending Jobs
        public string ViewJobByStatusJSON(bool Job_Status)
        {
            List<Job> job = new List<Job>(); 
            
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                job = JsonConvert.DeserializeObject<List<Job>>(json);
            }

            StringBuilder jobString_Print_All = new StringBuilder();

            for (int x = 0; x < job.Count; x++)
            {
                if (job[x].Complete == Job_Status)
                {
                    jobString_Print_All.Append(job[x].toString() + "\n");
                }
            }
            return jobString_Print_All.ToString();
        }

        // Search for a job based on ID i.e. Job Number
        public Job SearchJobJSON(int ID)
        {
            List<Job> job = new List<Job>(); 
            
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                job = JsonConvert.DeserializeObject<List<Job>>(json);
            }

            Job found_Job = new Job();
           
            for (int x = 0; x < job.Count; x++)
            {
                if (job[x].JobNumber == ID)
                {
                    found_Job = job[x];
                    break;
                }
                else
                {
                    found_Job.JobNumber = -1;
                }
            }
            return found_Job;
        }

        // Delete a job based on ID i.e. Job Number
        public void DeleteJobJSON(int ID)
        {
            List<Job> job = new List<Job>(); 
            
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                job = JsonConvert.DeserializeObject<List<Job>>(json);
            }
            
            for (int x = 0; x < job.Count; x++)
            {
                if (job[x].JobNumber == ID)
                {
                    job.RemoveAt(x);
                }
            }
           
            string json_text = JsonConvert.SerializeObject(job, Formatting.Indented);

            File.WriteAllText(FileLocation, json_text);

            // Replace the invalid JSON with valid JSON
            string jsonToReplace = File.ReadAllText(FileLocation);
            jsonToReplace = jsonToReplace.Replace("][", ",");
            File.WriteAllText(FileLocation, jsonToReplace);
        }
    }
}
