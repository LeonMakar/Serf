namespace Serfe.EventBusSystem.Signals
{
    public class OnGameOverSignal
    {
        public bool IsGameOver { get; private set; }

        public OnGameOverSignal(bool isGameOver)
        {
            IsGameOver = isGameOver;
        }
    }
}