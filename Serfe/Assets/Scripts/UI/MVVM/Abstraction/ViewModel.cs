using Serfe.Models;
using System;
using Zenject;

namespace Serfe.MVVM
{
    public abstract class ViewModel
    {
        protected RunningData _runningData;


        public ReactiveProperty<int> MoneyView = new ReactiveProperty<int>();
        public ReactiveProperty<int> ScoreView = new ReactiveProperty<int>();
        public ReactiveProperty<bool> IsGameStartView = new ReactiveProperty<bool>();

        [Inject]
        public void Construct(RunningData runningData)
        {
            _runningData = runningData;

            _runningData.Money.OnChange += OnModelMoneyChange;
            _runningData.Score.OnChange += OnModelScoreChange;
            _runningData.IsGameStart.OnChange += OnModelIsGameStartChange;
        }

        private void OnModelIsGameStartChange(bool boolian) => IsGameStartView.Value = boolian;
        public void OnViewRestartGameClicked() => _runningData.ChangeGameCondition(true);


        public virtual void Dispose()
        {
            _runningData.Money.OnChange -= OnModelMoneyChange;
            _runningData.Score.OnChange -= OnModelScoreChange;
            _runningData.IsGameStart.OnChange -= OnModelIsGameStartChange;
        }


        private void OnModelMoneyChange(int value) => MoneyView.Value = value;
        private void OnModelScoreChange(int value) => ScoreView.Value = value;
    }
}