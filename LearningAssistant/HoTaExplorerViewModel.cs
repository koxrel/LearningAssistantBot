using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningAssistant.Database.Entities;
using LearningAssistant.Database.Interfaces;
using LearningAssistant.Database.DataAccessImplementations;

namespace LearningAssistant
{
    public class HoTaExplorerViewModel : INotifyPropertyChanged
    {

        private IEnumerable<Hometask> _items;

        public IEnumerable<Hometask> Items
        {
            get { return _items; }
            set {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public HoTaExplorerViewModel()
        {
           ItemSet();
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
