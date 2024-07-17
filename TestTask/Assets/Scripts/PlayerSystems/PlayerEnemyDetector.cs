using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TestTask.PlayerSystems
{
    public class PlayerEnemyDetector : MonoBehaviour
    {
        [SerializeField] private float _delayForScanEnemy;
        [SerializeField] private ContactFilter2D _contactFilter;
        private MainData _gameData;
        private EventBus _eventBus;
        private List<RaycastHit2D> _rayHit2D = new List<RaycastHit2D>();

        [Inject]
        private void Construct(EventBus eventBus, MainData gameData)
        {
            _gameData = gameData;
            _eventBus = eventBus;
        }

        private void Start()
        {
            StartCoroutine(FindEnemyCoroutine());
        }

        private IEnumerator FindEnemyCoroutine()
        {
            while (true)
            {
                if (Physics2D.Raycast(transform.position, Vector2.up, _contactFilter, _rayHit2D, _gameData.ShootRange) != 0)
                    _eventBus.OnEnemyFinded?.Invoke(_rayHit2D.ToArray());
                else
                {
                    _rayHit2D.Clear();
                    _eventBus.OnEnemyFinded?.Invoke(_rayHit2D.ToArray());
                }

                yield return new WaitForSeconds(_delayForScanEnemy);
            }
        }
    }
}

