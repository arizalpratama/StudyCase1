namespace PaymentService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}