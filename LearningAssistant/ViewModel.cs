using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;


namespace LearningAssistant
{
    class ViewModel : INotifyPropertyChanged
    {



        Navigator Nav = new Navigator();


        public string Status { get; set; } = "active";
        
        public ViewModel()
        {
           // Nav.NavigateTo("AdditionalWindow");


        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
