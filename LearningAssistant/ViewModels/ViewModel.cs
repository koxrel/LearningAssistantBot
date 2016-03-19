using System;
using System.ComponentModel;
using System.Windows.Input;
using LearningAssistant.Classes;
using LearningAssistant.Interfaces;
using LearningAssistant.TelegramBot.Classes;
using Factory = LearningAssistant.Classes.Factory;

namespace LearningAssistant.ViewModels
{
    class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {           
            ButtonStartClick = new Command(StartBut);
            ButtonStopClick = new Command(StopBut);
            ButtonNewAssignmentClick = new Command(NABut);
            ButtonOverviewDeadlinesClick = new Command(OverviewDeadlines);
            ButtonOverviewHomeTasksClick = new Command(OverviewHomeTasks);
            ButtonOverviewUsersClick = new Command(OverviewUsers);
            ButtonSendClick = new Command(SendMessage);
        }

        INavigator _nav = Factory.GetNavigator;
        
        public ICommand ButtonStartClick { get; set; }
        public ICommand ButtonStopClick { get; set; }
        public ICommand ButtonNewAssignmentClick { get; set; }
        public ICommand ButtonOverviewHomeTasksClick { get; set; }
        public ICommand ButtonOverviewDeadlinesClick { get; set; }
        public ICommand ButtonOverviewUsersClick { get; set; }
        public ICommand ButtonSendClick { get; set; }

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

        private bool _startbe = true;

        public bool StartButEnabled
        {
            get { return _startbe; }
            set
            {
                _startbe = value;
                OnPropertyChanged("StartButEnabled");
            }
        }

        private bool _stopbe = false;

        public bool StopButEnabled
        {
            get { return _stopbe; }
            set
            {
                _stopbe = value;
                OnPropertyChanged("StopButEnabled");
            }
        }

        private string _mes;

        public string Message
        {
            get { return _mes; }
            set
            {
                _mes = value;
                OnPropertyChanged("Message");
            }
        }

        public void BotError()
        {
           
            _nav.ErrorCaught("bot could not process requests");
            StatusLabel = "inactive";
        }       

        public void SendMessage(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Message))
            {
                BotWebRequest.Bot.SendBulkMessage(Message);
            }
            Message = string.Empty;
        }

        public void OverviewUsers(object obj)
        {
            _nav.NavigateTo("UE");
        }

        public void OverviewHomeTasks(object obj)
        {
            _nav.NavigateTo("HTE");
        }

        public void OverviewDeadlines(object obj)
        {
            _nav.NavigateTo("DE");
        }

        public void StartBut(object obj)
        {
            StartButEnabled = false;
            try
            {                
                BotWebRequest.OnError += BotError;
                BotWebRequest.Bot.StartProcessing();
                if (BotWebRequest.Bot.IsActive)
                {
                    StatusLabel = "active";
                    StopButEnabled = true;
                }
                else
                {
                    StatusLabel = "inactive";
                    StartButEnabled = true;
                }
            }
            catch(Exception ex)
            {
                OnError(ex.Message);
                StartButEnabled = true;
                StopButEnabled = false;
            }
        }

        public void StopBut(object obj)
        {            
            StopButEnabled = false;
            try {
                BotWebRequest.Bot.CancelProcessing();
                if (BotWebRequest.Bot.IsActive)
                {
                    StatusLabel = "active";
                    StopButEnabled = true;
                }
                else
                {
                    StatusLabel = "inactive";
                    StartButEnabled = true;
                }
            }
            catch(Exception ex)
            {
                StartButEnabled = false;
                StopButEnabled = true;
                OnError(ex.Message);
            }
        }

        public void OnError(string ex)
        {
            _nav.ErrorCaught(ex);
        }

        public void NABut(object obj)
        {
            _nav.NavigateTo("AdditionalWindow");
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
