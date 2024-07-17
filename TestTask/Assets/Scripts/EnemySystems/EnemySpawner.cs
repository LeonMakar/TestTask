using System.Collections;
using System.Collections.Generic;
using TestTask.Pools;
using UnityEngine;
using Zenject;

namespace TestTask.EnemySystems
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        [SerializeField] private Transform _finishLineTransform;

        private EnemyPool _enemyPool;
        private MainData _gameData;
        private EventBus _eventBus;

        [Inject]
        private void Construct(EnemyPool enemyPool, MainData gameData, EventBus eventBus)
        {
            _enemyPool = enemyPool;
            _gameData = gameData;
            _eventBus = eventBus;
        }


        private void Start()
        {
            _gameData.GameIsActive = true;
            StartCoroutine(StartSpawningCoroutine());
        }

        private IEnumerator StartSpawningCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_gameData.EnemySpawningTimeInterval.MinTime, _gameData.EnemySpawningTimeInterval.MaxTime));
                if (_gameData.GameIsActive)
                {
                    int randomIndex = Random.Range(0, _spawnPoints.Count);
                    _enemyPool
                        .GetFromPool()
                        .Spawn(_finishLineTransform, _spawnPoints[randomIndex].position);
                }
            }
        }
    } 
}
