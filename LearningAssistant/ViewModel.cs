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

        
        private string _TxtBx;
        public string TxtBx
        {
            get { return _TxtBx; }
            set
            {
                _TxtBx = value;
                OnPropertyChanged("TxtBx");
            }
        }

        Navigator Nav = new Navigator();
        


        public ViewModel()
        {
            Nav.NavigateTo("AdditionalWindow");


        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
