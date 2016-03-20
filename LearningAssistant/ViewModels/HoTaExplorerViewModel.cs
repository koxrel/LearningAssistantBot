using System;
using System.Threading.Tasks;
using LearningAssistant.Classes;
using LearningAssistant.Database.Entities;
using LearningAssistant.Database.Interfaces;
using LearningAssistant.Database.EntitiesInterfaces;

namespace LearningAssistant.ViewModels
{
    public class HoTaExplorerViewModel : DetailsBaseViewModel<IHometask>
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
