using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningAssistant.Database.Entities
{
    public class Hometask
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Subject}:\n{Description}";
        }
    }
}
