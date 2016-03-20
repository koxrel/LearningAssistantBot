using System;
using System.Collections.Generic;
using System.Windows;
using LearningAssistant.Interfaces;
using LearningAssistant.Views;

namespace LearningAssistant.INavigatorImplementations
{
    class Navigator : INavigator
    {
        public Navigator()
        {
            _dict = new Dictionary<string, Window>();
            _dict.Add("AdditionalWindow", new Views.AddWindow());
            _dict.Add("HTE", new Views.HoTaExplorer());
            _dict.Add("DE", new DeadlineExplorer());
            _dict.Add("UE", new UserExplorer());
        }

        private Dictionary<string, Window> _dict;

        public void NavigateTo(string name)
        {
            Window w;
            if (_dict.TryGetValue(name, out w))
            {
                w.ShowDialog();                
                _dict[name] = (Window)Activator.CreateInstance(_dict[name].GetType());         
            }           
        }
       
        public void ErrorCaught(string ex)
        {
            MessageBox.Show($"Error occured: {ex}");
        }

        public void ShowInfo()
        {
            MessageBox.Show("This bot was made in accordance with our mission: providing students with first class studying experience.\n\nThis program is the grace of two aspiring developers:\nIgor Tresoumov (koxrel) and Sergey Pavlov (CapObvios).\n\nPlease, be easy on us. @_@", "Bot info");
        }
    }
}
