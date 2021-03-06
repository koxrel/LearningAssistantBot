﻿using System;

namespace LearningAssistant.Database.EntitiesInterfaces
{
    public interface IHometask
    {
        string Description { get; set; }
        DateTime DueDate { get; set; }
        int Id { get; set; }
        string Subject { get; set; }

        string ToString();
    }
}