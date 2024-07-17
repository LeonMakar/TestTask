using System.Collections;
using UnityEngine;

namespace TestTask.EnemySystems
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private EnemyData _enemyData;
        private float _speed;
        private Coroutine _cachedCoroutine;


        public void StartMooving()
        {
            _speed = Random.Range(_enemyData.GameData.EnemySpeedValueInterval.MinSpeed, _enemyData.GameData.EnemySpeedValueInterval.MaxSpeed);
            _cachedCoroutine = StartCoroutine(StartMovingCoroutine());
        }

        private IEnumerator StartMovingCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            while (_enemyData.GameData.GameIsActive)
            {
                _characterController.Move(Vector2.down * _speed * Time.fixedDeltaTime);
                if (transform.position.y < _enemyData.FinishTransform.position.y)
                {
                    gameObject.SetActive(false);
                    _enemyData.RestoreHealth();
                    _enemyData.EventBus.OnHealthChanged();
                }
                yield return null;
            }
        }
    }
}
