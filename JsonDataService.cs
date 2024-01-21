using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace AirplaneTicketReservationSystem
{
    public class CustomJsonService<T>
    {
        private readonly string filePath;

        public CustomJsonService(string filePath)
        {
            this.filePath = filePath;
        }

        public List<T>? ReadData()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<T>();
                }

                string jsonData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<T>>(jsonData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void WriteData(T newData)
        {
            try
            {
                List<T>? list = ReadData();

                if (list == null)
                {
                    list = new List<T>();
                }

                list.Add(newData);
                string jsonData = JsonConvert.SerializeObject(list, Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
                Console.WriteLine("Record added successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

