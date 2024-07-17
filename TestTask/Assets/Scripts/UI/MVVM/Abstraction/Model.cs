
using System;
using TestTask.Infrastructure;
using TestTask.Pools;

namespace TestTask.MVVM
{
    public abstract class Model
    {
        public readonly ReactiveProperty<bool> IsGameWin = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<int> Health = new ReactiveProperty<int>();
        public readonly ReactiveProperty<int> EnemyKilled = new ReactiveProperty<int>();

        private MainData _gameData;
        private EnemyPool _enemyPool;

        public virtual void Init(MainData mainData, EventBus eventBus, EnemyPool enemyPool)
        {
            _gameData = mainData;
            _enemyPool = enemyPool;
            eventBus.OnHealthChanged += ChangeHealth;
            eventBus.OnEnemyKilled += ChangeScore;
            Health.Value = _gameData.PlayerHealth;
            EnemyKilled.Value = UnityEngine.Random.Range(_gameData.EnemyToKillInterval.MinCount, _gameData.EnemyToKillInterval.MaxCount);
        }

        private void ChangeScore()
        {
            EnemyKilled.Value--;
            if (EnemyKilled.Value == 0)
                SetGameWinCondition(true);
        }

        private void ChangeHealth()
        {
            Health.Value--;
            if (Health.Value == 0)
            {
                SetGameWinCondition(false);
            }
        }
        public void RestartGame()
        {
            Health.Value = _gameData.PlayerHealth;
            EnemyKilled.Value = UnityEngine.Random.Range(_gameData.EnemyToKillInterval.MinCount, _gameData.EnemyToKillInterval.MaxCount);
            _gameData.GameIsActive = true;
            _enemyPool.RemooveAllObject();

        }
        public void SetGameWinCondition(bool isGameWin)
        {
            IsGameWin.Value = isGameWin;
            _gameData.GameIsActive = false;
        }
    }
}
