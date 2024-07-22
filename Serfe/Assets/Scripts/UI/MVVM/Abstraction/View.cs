using UnityEngine;
using Zenject;

namespace Serfe.MVVM
{
    public abstract class View : MonoBehaviour
    {
        protected ViewModel _viewModel;

        [Inject]
        private void Construct(ViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.MoneyView.OnChange += OnViewModelMoneyChange;
            _viewModel.ScoreView.OnChange += OnViewModelScoreChange;
        }

        private void OnDestroy()
        {
            _viewModel.Dispose();

            _viewModel.MoneyView.OnChange -= OnViewModelMoneyChange;
            _viewModel.ScoreView.OnChange -= OnViewModelScoreChange;
        }
        public abstract void OnViewModelMoneyChange(int value);
        public abstract void OnViewModelScoreChange(int value);
    }
}