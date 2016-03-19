using LearningAssistant.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LearningAssistant
{
    class Navigator : INavigator
    {

        private Dictionary<string, Window> _dict = new Dictionary<string, Window>();


        public Navigator()
        {
            _dict.Add("AdditionalWindow", new AddWindow());
            _dict.Add("HTE", new HoTaExplorer());
            _dict.Add("DE", new DeadlineExplorer());
            _dict.Add("UE", new UserExplorer());
        }
        
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


    }
}
