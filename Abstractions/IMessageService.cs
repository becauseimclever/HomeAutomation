namespace BecauseImClever.HomeAutomation.Abstractions
{
    public interface IMessageService
    {
        bool Enqueue(string message);
    }
}
