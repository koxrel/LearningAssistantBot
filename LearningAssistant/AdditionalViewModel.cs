using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LearningAssistant
{
    class AdditionalViewModel : INotifyPropertyChanged
    {
        public AdditionalViewModel()
        {
            ButtonAddClick = new Command(BAddClick);
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

        public ICommand ButtonAddClick { get; set; }

        
        public void BAddClick(object obj)
        {
           
        }

        //private bool _ht;

        //public bool HomeTask
        //{
        //    get { return _ht; }
        //    set
        //    {
        //        _ht = value;
        //        OnPropertyChanged("HomeTask");
        //    }
        //}

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