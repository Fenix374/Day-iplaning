using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DailyPlanner
{
    public class Crud
    {
        private string filePath;

        public Crud(string filePath)
        {
            this.filePath = filePath;
        }

        public void Serialize(List<Note> notes)
        {
            string json = JsonConvert.SerializeObject(notes);
            File.WriteAllText(filePath, json);
        }

        public List<Note> Deserialize()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Note>>(json);
            }
            else
            {
                return new List<Note>();
            }
        }
    }
}
