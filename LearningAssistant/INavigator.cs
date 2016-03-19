namespace LearningAssistant
{
    interface INavigator
    {
        void ErrorCaught(string ex);
        void NavigateTo(string name);
    }
}