using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using LearningAssistant.Database.Interfaces;
using System.Windows.Input;
using LearningAssistant.Classes;
using LearningAssistant.Interfaces;

namespace LearningAssistant.ViewModels
{

    public abstract class DetailsBaseViewModel<T>:INotifyPropertyChanged
    {
        public DetailsBaseViewModel()
        {            
            ItemSet();
            ButtonAddClick = new Command(AddBut);
            ButtonRemoveClick = new Command(RemoveBut);
        }

        INavigator _nav;

        public ICommand ButtonAddClick { get; set; }
        public ICommand ButtonRemoveClick { get; set; }

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

        public async void AddBut(object obj)
        {
            if (_nav == null)
                _nav = Factory.GetNavigator;              
            _nav.NavigateTo("AdditionalWindow");
            await RefreshGrid();
        }        

        

        public void OnError(string ex)
        {
            if (_nav == null)
                _nav = Factory.GetNavigator;
            _nav.ErrorCaught(ex);
        }

        private async void ItemSet()
        {
            await RefreshGrid();
        }

        public abstract void RemoveBut(object obj);
        public abstract Task RefreshGrid();
        public abstract Task RefreshGrid(IDataAccess mta);

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}

