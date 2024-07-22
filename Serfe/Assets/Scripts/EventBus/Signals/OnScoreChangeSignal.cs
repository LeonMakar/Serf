namespace Serfe.EventBusSystem.Signals
{
    public class OnScoreChangeSignal
    {
        public int ScoreToAdd { get; private set; }
        public void Init(int scoreToAdd)
        {
            ScoreToAdd = scoreToAdd;
        }
    }
}