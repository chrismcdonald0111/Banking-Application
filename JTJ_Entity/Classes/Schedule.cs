using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JTJ_Entity
{
    // Schedule Object
    public class Schedule
    {
        public string Date { get; set; }
        public string Day { get; set; }
        public string Info { get; set; }

        // Location of schedule.json
        string FileLocation = @"C:\Users\Chris\documents\visual studio 2013\Projects\SE361_GUI\JTJ_Entity\JSON\schedule.json";

        // Function to display the Schedule JSON data as a string
        public string toString()
        {
            return "***************************************************************************" + "\nDay of the Week: " + Day + " " + "Date: " + Date + "\n" +
                "Info:\n" + Info;
        }

        // Add the Schedule object in JSON format to the appropriate file
        public void AddScheduleJSON(string Date, string Day, string Info)
        {
            // Instantiate the Schedule Class and store the values in the object variables
            Schedule schedule = new Schedule();
            schedule.Date = Date;
            schedule.Day = Day;
            schedule.Info = Info;

            // Store the Schedule Object as a JSON array for easy mapping to a list of Customer Objects 
            List<Schedule> scheduleList = new List<Schedule>();
            scheduleList.Add(schedule);

            // Convert the Schedule Object to a string in JSON format
            string json = JsonConvert.SerializeObject(scheduleList, Formatting.Indented);

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

        // Modify the information for an existing date in the schedule
        public void ModifyScheduleJSON(string Date, string Day, string Info)
        {
            List<Schedule> schedule = new List<Schedule>();

            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                schedule = JsonConvert.DeserializeObject<List<Schedule>>(json);
            }

            for (int x = 0; x < schedule.Count; x++)
            {
                if (schedule[x].Date == Date)
                {
                    schedule[x].Date = Date;
                    schedule[x].Day = Day;
                    schedule[x].Info = Info;
                    break;
                }
            }

            string json_text = JsonConvert.SerializeObject(schedule, Formatting.Indented);

            File.WriteAllText(FileLocation, json_text);

            // Replace the invalid JSON with valid JSON
            string jsonToReplace = File.ReadAllText(FileLocation);
            jsonToReplace = jsonToReplace.Replace("][", ",");
            File.WriteAllText(FileLocation, jsonToReplace);
        }

        // View a specific one week block stored in schedule.json
        public string ViewOneWeekScheduleJSON(string Date)
        {
            List<Schedule> schedule = new List<Schedule>(); // List to store Schedule objects

            // Open the file, deserialize the JSON data and store in 'schedule' as a list of Customer objects
            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                schedule = JsonConvert.DeserializeObject<List<Schedule>>(json);
            }

            StringBuilder scheduleString_Print_One_Week = new StringBuilder();

            // Append all of the Schedule objects into one string 
            // in order to print the information for a one week block
            int count = 0;
            DateTime selected_date = Convert.ToDateTime(Date);
            DateTime[] selected_week = new DateTime[7];
            selected_week[0] = selected_date;
            List<Schedule> unsorted_schedule = new List<Schedule>
            {
                new Schedule { Date = "0", Day = "0", Info = ""},
                 new Schedule { Date = "0", Day = "0", Info = ""},
                  new Schedule { Date = "0", Day = "0", Info = ""},
                   new Schedule { Date = "0", Day = "0", Info = ""},
                    new Schedule { Date = "0", Day = "0", Info = ""},
                     new Schedule { Date = "0", Day = "0", Info = ""},
                      new Schedule { Date = "0", Day = "0", Info = ""},
                       new Schedule { Date = "0", Day = "0", Info = ""}
            };

            // Store in 'selected_week' the remaining dates of the week based on the given date 
            for (int x = 1; x < 7; x++)
            {
                count++;
                selected_week[x] = selected_date.AddDays(count);
            }
            // Retrieve the information located in schedule.json for the days of the week provided
            for (int x = 0; x < schedule.Count; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if (schedule[x].Date == selected_week[y].ToShortDateString())
                    {
                        unsorted_schedule[y] = schedule[x];
                    }
                    if (unsorted_schedule[y].Date == "0")
                    {
                        unsorted_schedule[y].Date = selected_week[y].ToShortDateString();
                        unsorted_schedule[y].Day = selected_week[y].ToString("dddd");
                    }
                }
            }
            // Sort the list of Schedule objects based on 'Date' using LINQ
            List<Schedule> sorted_schedule = unsorted_schedule.OrderBy(o => o.Date).ToList();
            
            for (int x = 1; x < 8; x++)
            { 
                scheduleString_Print_One_Week.Append(sorted_schedule[x].toString() + "\n");
            }
            return scheduleString_Print_One_Week.ToString();
        }

        // Search for a date from the schedule by ID i.e. Date
        public Schedule SearchDateJSON(string Date)
        {
            List<Schedule> schedule = new List<Schedule>();

            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                schedule = JsonConvert.DeserializeObject<List<Schedule>>(json);
            }

            Schedule found_Schedule = new Schedule();

            for (int x = 0; x < schedule.Count; x++)
            {
                if (schedule[x].Date == Date)
                {
                    found_Schedule = schedule[x];
                    break;
                }
                else
                {
                    found_Schedule.Date = "-1";
                }
            }
            return found_Schedule;
        }

        // Delete a date from the schedule based on ID i.e. Date
        public void CancelDateJSON(string Date)
        {
            List<Schedule> schedule = new List<Schedule>();

            using (StreamReader file = new StreamReader(FileLocation))
            {
                string json = file.ReadToEnd();
                schedule = JsonConvert.DeserializeObject<List<Schedule>>(json);
            }

            for (int x = 0; x < schedule.Count; x++)
            {
                if (schedule[x].Date == Date)
                {
                    schedule.RemoveAt(x);
                }
            }

            string json_text = JsonConvert.SerializeObject(schedule, Formatting.Indented);

            File.WriteAllText(FileLocation, json_text);

            // Replace the invalid JSON with valid JSON
            string jsonToReplace = File.ReadAllText(FileLocation);
            jsonToReplace = jsonToReplace.Replace("][", ",");
            File.WriteAllText(FileLocation, jsonToReplace);
        }
    }
}
