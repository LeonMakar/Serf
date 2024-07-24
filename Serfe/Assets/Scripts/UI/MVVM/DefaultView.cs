using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Serfe.MVVM
{
    public class DefaultView : View
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _restartButtone;
        [SerializeField] private GameObject _gameOverPanel;

        private void Start()
        {
            _restartButtone.onClick.AddListener(_viewModel.OnViewRestartGameClicked);
        }
        public override void OnViewModelIsGameStartConditionChange(bool isGameStart)
        {
            if (isGameStart)
                _gameOverPanel.SetActive(false);
            else
                _gameOverPanel.SetActive(true);

        }

        public override void OnViewModelMoneyChange(int value)
        {
            _moneyText.text = $"Money: {value.ToString()}";
        }

        public override void OnViewModelScoreChange(int value)
        {
            _scoreText.text = $"Score: {value.ToString()}";
        }
    }
}