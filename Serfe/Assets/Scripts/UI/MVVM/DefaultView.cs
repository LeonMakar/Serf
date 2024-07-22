using TMPro;
using UnityEngine;

namespace Serfe.MVVM
{
    public class DefaultView : View
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _scoreText;

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