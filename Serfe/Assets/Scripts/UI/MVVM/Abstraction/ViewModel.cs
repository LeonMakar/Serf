using Serfe.Models;
using Zenject;

namespace Serfe.MVVM
{
    public abstract class ViewModel 
    {
        protected RunningData _runningData;


        public ReactiveProperty<int> MoneyView = new ReactiveProperty<int>();
        public ReactiveProperty<int> ScoreView = new ReactiveProperty<int>();

        [Inject]
        public void Construct(RunningData runningData)
        {
            _runningData = runningData;

            _runningData.Money.OnChange += OnModelMoneyChange;
            _runningData.Score.OnChange += OnModelScoreChange;
        }

        public virtual void Dispose()
        {
            _runningData.Money.OnChange -= OnModelMoneyChange;
            _runningData.Score.OnChange -= OnModelScoreChange;
        }


        private void OnModelMoneyChange(int value) => MoneyView.Value = value;
        private void OnModelScoreChange(int value) => ScoreView.Value = value;
    }
}