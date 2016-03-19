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

namespace LearningAssistant
{
    public class HoTaExplorerViewModel : INotifyPropertyChanged
    {


        public HoTaExplorerViewModel()
        {
            ItemSet();
            ButtonAddClick = new Command(AddBut);
            ButtonRemoveClick = new Command(RemoveBut);
        }

        private IEnumerable<Hometask> _items;

        public IEnumerable<Hometask> Items
        {
            get { return _items; }
            set {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        private Hometask _si;

        public Hometask SelectedItem
        {
            get { return _si; }
            set
            {
                _si = value;
                OnPropertyChanged("Items");
            }
        }

        public ICommand ButtonAddClick { get; set; }

        public void AddBut(object obj)
        {
            Navigator nav = new Navigator();
            nav.NavigateTo("AdditionalWindow");            
        }

        public ICommand ButtonRemoveClick { get; set; }

        public void RemoveBut(object obj)
        {
            Navigator nav = new Navigator();
            nav.NavigateTo("AdditionalWindow");
        }



        private async void ItemSet()
        {
            IDataAccess mta = new DataAccess();
            Items = await mta.GetHomeTasks();            
        }




        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
