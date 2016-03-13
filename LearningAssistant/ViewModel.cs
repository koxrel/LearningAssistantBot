﻿using System;
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
            StatusLabel = "active";
        }

        public void StopBut(object obj)
        {
            StatusLabel = "inactive";
        }

        public void NABut(object obj)
        {
            _nav.NavigateTo("AdditionalWindow");
        }

        private string _status = "inactive";

        public string StatusLabel
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("StatusLabel");
                    } 
}


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
