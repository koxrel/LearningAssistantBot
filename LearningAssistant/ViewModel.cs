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



        Navigator _nav = new Navigator();

        public ICommand ButtonStartClick { get; set; }
        public ICommand ButtonStopClick { get; set; }
        public ICommand ButtonNewAssignmentClick { get; set; }


        public void StartBut(object obj)
        {

        }

        public void StopBut(object obj)
        {

        }

        public void NABut(object obj)
        {
            _nav.NavigateTo("AdditionalWindow");
        }

        public string Status { get; set; } = "active";
        
        public ViewModel()
        {
            ButtonStartClick = new Command(StartBut);
            ButtonStopClick = new Command(StopBut);
            ButtonNewAssignmentClick = new Command(NABut);

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
