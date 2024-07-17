using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Zenject;
namespace TestTask.MVVM
{
    public class DefaultView : View
    {
        [SerializeField] private Button _restartButtone;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _gameConditionText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private AudioClip WinClip;
        [SerializeField] private AudioClip LoseClip;
        private AudioSource _audioSource;

        private void Start()
        {
            PrintHealthValue(_viewModel.Health.Value);
        }

        [Inject]
        private void Construct(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
        public override void Init(ViewModel viewModel)
        {
            base.Init(viewModel);
            _restartButtone.onClick.AddListener(_viewModel.OnGameRestartButtoneClicked);
        }
        protected override void OnGamePanelEnabled(bool boolian)
        {
            _gameOverPanel.SetActive(boolian);
        }

        protected override void PrintGameCondition(bool isGameWin)
        {
            if (isGameWin)
            {
                _gameConditionText.text = "Game is Won";
                _audioSource.PlayOneShot(WinClip);
            }
            else
            {
                _audioSource.PlayOneShot(LoseClip);
                _gameConditionText.text = "Game is Lost";
            }
        }

        protected override void PrintHealthValue(int value)
        {
            _healthText.text = $"Health  = {value}";
        }
    }
}
