using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningAssistant.Database.Entities
{
    public class Deadline
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{DueDate:d.MM.yyyy HH:mm:ss} - {Subject}: {Description}";
        }
    }
}
