namespace LearningAssistant.Database.EntitiesInterfaces
{
    public interface IUser
    {
        int ChatId { get; set; }
        string FullName { get; set; }
        int Id { get; set; }
    }
}