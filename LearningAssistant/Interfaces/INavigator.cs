﻿namespace LearningAssistant.Interfaces
{
    interface INavigator
    {
        void ErrorCaught(string ex);
        void NavigateTo(string name);
        void ShowInfo();
    }
}