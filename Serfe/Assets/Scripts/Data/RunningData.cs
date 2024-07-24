using Serfe.EventBusSystem;
using Serfe.EventBusSystem.Signals;
using Serfe.MVVM;
using System;
using UnityEngine;
using Zenject;

namespace Serfe.Models
{
    [CreateAssetMenu(fileName = "RunningData", menuName = "GameData")]
    public class RunningData : ScriptableObject
    {
        private EventBus _eventBus;

        public ReactiveProperty<int> Money { get; private set; } = new ReactiveProperty<int>();
        public ReactiveProperty<int> Score { get; private set; } = new ReactiveProperty<int>();
        public ReactiveProperty<bool> IsGameStart { get; private set; } = new ReactiveProperty<bool>();

        public int Health { get; private set; } = 1;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscrube<OnScoreChangeSignal>(ChangeScore);
            _eventBus.Subscrube<OnGameOverSignal>(ChangeGameCondition);
        }

        private void ChangeGameCondition(OnGameOverSignal signal)
        {
            if (!signal.IsGameOver)
                ResetData();
            IsGameStart.Value = !signal.IsGameOver;
        }
        public void ChangeGameCondition(bool isGameStart) => _eventBus.Invoke(new OnGameOverSignal(!isGameStart));
        private void ChangeScore(OnScoreChangeSignal signal) => Score.Value += signal.ScoreToAdd;
        public void ResetData() => Health = 1;


        public void MinusHealth()
        {
            Health--;
            if (Health == 0)
                _eventBus.Invoke(new OnGameOverSignal(true));
        }

        public void AddMoney(int money)
        {
            if (money > 0)
                Money.Value += money;
        }
        public void AddScore(int score)
        {
            if (score > 0)
                Score.Value += score;
        }
    }
}
