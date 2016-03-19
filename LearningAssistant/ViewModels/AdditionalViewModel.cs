﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LearningAssistant.Database.DataAccessImplementations;
using LearningAssistant.Database.Interfaces;
using LearningAssistant.Database.Entities;

namespace LearningAssistant
{
    class AdditionalViewModel : INotifyPropertyChanged
    {
        
        public AdditionalViewModel()
        {
            ButtonAddClick = new Command(BAddClick);
        }

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

        public void OnError(string ex)
        {
            INavigator _nav = Factory.GetNavigator;
            _nav.ErrorCaught(ex);
        }

        public ICommand ButtonAddClick { get; set; }

        IDataAccess da;

        public async void BAddClick(object obj)
        {
            AddEnabled = false;
            try
            {
                da = Factory.GetDataAccess;
                if (Type)
                    await da.AddDeadline(Subject, Description, DueDate);
                else
                    await da.AddHometask(Subject, Description, DueDate);
            }
            catch (ArgumentNullException)
            {
                OnError("Specify all of the parameters!");
            }
            catch (Exception ex)
            {
                OnError(ex.Message);
            }
            finally
            {
                da.Dispose();
            }

            AddEnabled = true;
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
        private string _summodule;
        public string SumModule
        {
            get { return _summodule; }
            set
            {
                _summodule = value;
                OnPropertyChanged("SumModule");
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







        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }

}