using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using LearningAssistant.TelegramBot;



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
            BotWebRequest.Bot.StartProcessing();
            if (BotWebRequest.Bot.IsActive)
                StatusLabel = "active";
            else
                StatusLabel = "inactive";
        }

        public void StopBut(object obj)
        {
            BotWebRequest.Bot.CancelProcessing();
            if (BotWebRequest.Bot.IsActive)
                StatusLabel = "active";
            else
                StatusLabel = "inactive";
        }

        object w;
        public void NABut(object obj)
        {
            w = _nav.NavigateTo("AdditionalWindow");
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
