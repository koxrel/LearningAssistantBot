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

        public void BotError()
        {
            Navigator nav = new Navigator();
            nav.ErrorCaught("bot could not process requests");
            StatusLabel = "inactive";
        }
       
        public ICommand ButtonStartClick { get; set; }
        public ICommand ButtonStopClick { get; set; }
        public ICommand ButtonNewAssignmentClick { get; set; }
        public ICommand ButtonOverviewHomeTasksClick { get; set; }
        public ICommand ButtonOverviewDeadlinesClick { get; set; }

        public void OverviewHomeTasks(object obj)
        {
            new Navigator().NavigateTo("HTE");
        }

        public void OverviewDeadlines(object obj)
        {
            new Navigator().NavigateTo("DE");
        }

        public void StartBut(object obj)
        {
            
            BotWebRequest.Bot.OnError += BotError;            
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



      
        public void NABut(object obj)
        {
           new Navigator().NavigateTo("AdditionalWindow");
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
            ButtonOverviewDeadlinesClick = new Command(OverviewDeadlines);
            ButtonOverviewHomeTasksClick = new Command(OverviewHomeTasks);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
