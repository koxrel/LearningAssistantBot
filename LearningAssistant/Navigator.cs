using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LearningAssistant
{
    class Navigator       
    {

        Dictionary<string, Window> _dict = new Dictionary<string, Window>();


        public Navigator()
        {
            _dict.Add("AdditionalWindow", new AddWindow());

        }
Window w;
        public string NavigateTo(string name)
        {
            
            if (_dict.TryGetValue(name, out w))
            {
                
                         
               w.Show();
                _dict[name] = new AddWindow();
                return name;
            }
            else
                return null;
        }

        public void CloseWindow(string name)
        {

            
        }
     

    }
}
