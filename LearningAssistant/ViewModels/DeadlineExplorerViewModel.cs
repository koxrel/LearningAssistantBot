using System;
using System.Threading.Tasks;
using LearningAssistant.Database.Interfaces;
using LearningAssistant.Classes;
using LearningAssistant.Database.EntitiesInterfaces;

namespace LearningAssistant.ViewModels
{
    class DeadlineExplorerViewModel : DetailsBaseViewModel<IDeadline>
    {
        public DeadlineExplorerViewModel() : base() { }

        public override async Task RefreshGrid()
        {
            try
            {
                using (IDataAccess mta = Factory.GetDataAccess)
                {
                    Items = await mta.GetDeadlines();
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
                Items = await mta.GetDeadlines();
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
                    await da.RemoveDeadline(SelectedItem);
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
