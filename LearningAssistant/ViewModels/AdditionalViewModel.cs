﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using LearningAssistant.Classes;
using LearningAssistant.Database.Interfaces;
using LearningAssistant.Interfaces;

namespace LearningAssistant.ViewModels
{
    class AdditionalViewModel : INotifyPropertyChanged
    {        
        public AdditionalViewModel()
        {
            ButtonAddClick = new Command(BAddClick);
        }

        public ICommand ButtonAddClick { get; set; }

        private bool _ae = true;

        public bool AddEnabled
        {
            get { return _ae; }
            set
            {
                _ae = value;
                OnPropertyChanged("AddEnabled");
            }
        }

        private bool _type = true;

        public bool Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Deadline");
            }
        }

        private string _subject;

        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                OnPropertyChanged("Subject");
            }
        }

        private string _desctript;

        public string Description
        {
            get { return _desctript; }
            set
            {
                _desctript = value;
                OnPropertyChanged("Description");
            }
        }

        private DateTime _duedate = DateTime.Today;

        public DateTime DueDate
        {
            get { return _duedate; }
            set
            {
                _duedate = value;
                OnPropertyChanged("DueDate");
            }
        }

        public void OnError(string ex)
        {
            INavigator _nav = Factory.GetNavigator;
            _nav.ErrorCaught(ex);
        }          

        public async void BAddClick(object obj)
        {           
            AddEnabled = false;
            try
            {
                using (IDataAccess da = Factory.GetDataAccess)
                {
                    if (Type)
                        await da.AddDeadline(Subject, Description, DueDate);
                    else
                        await da.AddHometask(Subject, Description, DueDate);
                }
                Subject = string.Empty;
                Description = string.Empty;
            }
            catch (ArgumentNullException)
            {
                OnError("Specify all of the parameters!");
            }
            catch (Exception ex)
            {
                OnError(ex.Message);
            }           
            AddEnabled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}