using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningAssistant.Database.Entities;
using LearningAssistant.Database.Interfaces;
using LearningAssistant.Database.DataAccessImplementations;
using System.Windows.Input;

namespace LearningAssistant.ViewModels
{
    
    abstract public class DetailsBaseViewModel<T>:INotifyPropertyChanged
    {


        public DetailsBaseViewModel()
        {            
            ItemSet();
            ButtonAddClick = new Command(AddBut);
            ButtonRemoveClick = new Command(RemoveBut);
        }

        INavigator _nav;
        private IEnumerable<T> _items;

        public IEnumerable<T> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        private T _si;

        public T SelectedItem
        {
            get { return _si; }
            set
            {
                _si = value;
                OnPropertyChanged("Items");
            }
        }

        public ICommand ButtonAddClick { get; set; }

        public async void AddBut(object obj)
        {
            if (_nav == null)
                _nav = Factory.GetNavigator;              
            _nav.NavigateTo("AdditionalWindow");
            await RefreshGrid();
        }

        public ICommand ButtonRemoveClick { get; set; }

        abstract public void RemoveBut(object obj);
        

        public void OnError(string ex)
        {
            if (_nav == null)
                _nav = Factory.GetNavigator;
            _nav.ErrorCaught(ex);
        }

        abstract public Task RefreshGrid();



       abstract public Task RefreshGrid(IDataAccess mta);
      


        private bool _be = true;

        public bool ButEnabled
        {
            get { return _be; }
            set
            {
                _be = value;
                OnPropertyChanged("Items");
            }
        }

        private async void ItemSet()
        {
            await RefreshGrid();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}

