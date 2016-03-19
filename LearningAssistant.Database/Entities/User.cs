using System.ComponentModel.DataAnnotations;

namespace LearningAssistant.Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        public int ChatId { get; set; }
    }
}
