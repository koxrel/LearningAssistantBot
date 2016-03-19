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
using LearningAssistant.ViewModels;

namespace LearningAssistant
{
    public class HoTaExplorerViewModel : DetailsBaseViewModel<Hometask>
    {
        public HoTaExplorerViewModel() : base() { }

        public override async Task RefreshGrid()
        {
            try
            {
                using (IDataAccess mta = Factory.GetDataAccess)
                {
                    Items = await mta.GetHomeTasks();
                }
            }
            catch (Exception ex)
            {
                OnError(ex.Message);
            }
        }

        public override async Task RefreshGrid(IDataAccess mta)
        {
            try
            {
                Items = await mta.GetHomeTasks();
            }
            catch (Exception ex)
            {
                OnError(ex.Message);
            }
        }

        public override async void RemoveBut(object obj)
        {
            if (SelectedItem == null)
            {
                OnError("Select something!");
                return;
            }
            try
            {
                ButEnabled = false;
                using (IDataAccess da = Factory.GetDataAccess)
                {
                    await da.RemoveHometask(SelectedItem);
                    await RefreshGrid(da);
                }
                ButEnabled = true;
            }
            catch (Exception ex)
            {
                OnError(ex.Message);
            }
        }
    }
}
