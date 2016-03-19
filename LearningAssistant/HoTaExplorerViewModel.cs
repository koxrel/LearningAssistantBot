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

        public async void AddBut(object obj)
        {
            Navigator nav = new Navigator();
            nav.NavigateTo("AdditionalWindow");
           await RefreshGrid();          
        }

        public ICommand ButtonRemoveClick { get; set; }

        public async void RemoveBut(object obj)
        {

            if (SelectedItem == null)
            {
                OnError("Select something!");
                return;
            }
            try
            {

                ButEnabled = false;
                using (IDataAccess da = new DataAccess())
                {
                    await da.RemoveHometask(SelectedItem);
                    await RefreshGrid(da);
                }

                ButEnabled = true;

            }
            catch(Exception ex)
            {
                OnError(ex.Message);
            }
        }

        public void OnError(string ex)
        {
            Navigator nav = new Navigator();
            nav.ErrorCaught(ex);
        }

        public async Task RefreshGrid()
        {
            IDataAccess mta = new DataAccess();
            Items = await mta.GetHomeTasks();
        }

        public async Task RefreshGrid(IDataAccess mta)
        {
            Items = await mta.GetHomeTasks();
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
