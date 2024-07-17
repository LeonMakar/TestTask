using TestTask.Infrastructure;

namespace TestTask.MVVM
{
    public abstract class ViewModel
    {
        protected Model _model;

        public ReactiveProperty<bool> GameCondition = new ReactiveProperty<bool>();
        public ReactiveProperty<int> Health = new ReactiveProperty<int>();


        public ReactiveProperty<bool> GameOverPanelEnabled = new ReactiveProperty<bool>();

        public virtual void InitViewModel(Model model)
        {
            _model = model;

            _model.Health.OnValueChange += OnModelHealthChange;
            _model.IsGameWin.OnValueChange += OnModelWinConditionChange;
        }
        public void OnGameRestartButtoneClicked()
        {
            _model.RestartGame();
            GameOverPanelEnabled.Value = false;
        }
        public void OnModelWinConditionChange(bool boolian)
        {
            GameCondition.Value = boolian;
            GameOverPanelEnabled.Value = true;
        }

        private void OnModelHealthChange(int value)
        {
            Health.Value = value;
        }


    } 
}
