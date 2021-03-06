﻿using System;
using System.ComponentModel.DataAnnotations;
using LearningAssistant.Database.EntitiesInterfaces;

namespace LearningAssistant.Database.Entities
{
    public class Deadline : IDeadline
    {
        public int Id { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{DueDate:d.MM.yyyy HH:mm:ss} - {Subject}: {Description}";
        }
    }
}
