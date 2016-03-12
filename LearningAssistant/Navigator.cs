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

        public void NavigateTo(string name)
        {
            Window w;
            if (_dict.TryGetValue(name, out w))
            {
                w.Show();
            }


        }
    }
}
