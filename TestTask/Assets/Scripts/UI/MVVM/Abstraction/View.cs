using TestTask.MVVM;
using UnityEngine;

namespace TestTask.MVVM
{
    public abstract class View : MonoBehaviour
    {
        protected ViewModel _viewModel;


        public virtual void Init(ViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.Health.OnValueChange += PrintHealthValue;
            _viewModel.GameOverPanelEnabled.OnValueChange += OnGamePanelEnabled;
            _viewModel.GameCondition.OnValueChange += PrintGameCondition;
        }

        protected abstract void PrintGameCondition(bool obj);

        protected abstract void OnGamePanelEnabled(bool boolian);

        protected abstract void PrintHealthValue(int value);
    } 
}
