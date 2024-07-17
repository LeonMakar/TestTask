using System.Collections;
using TestTask.Pools;
using UnityEngine;
using Zenject;

namespace TestTask.PlayerSystems
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private float _shootingFlashDuration;
        [SerializeField] private GameObject _shootingLight;
        [SerializeField] private AudioClip _shootClip;

        private bool _isEnemyFinded;
        private WaitForSeconds _flashLightDelay;
        private WaitForSeconds _shootingDelay;
        private BulletPool _bulletPool;
        private MainData _gameData;
        private AudioSource _audioSource;
        private Vector2 _enemyPosition;

        [Inject]
        private void Construct(EventBus eventBus, BulletPool bulletPool, MainData mainData, AudioSource audioSource)
        {
            _bulletPool = bulletPool;
            _gameData = mainData;
            _audioSource = audioSource;
            eventBus.OnEnemyFinded += enemyRaycastHit2DArray =>
            {
                if (enemyRaycastHit2DArray.Length > 0)
                {
                    _isEnemyFinded = true;
                    _enemyPosition = enemyRaycastHit2DArray[0].point;
                }
                else
                {
                    _isEnemyFinded = false;
                    _enemyPosition = Vector2.zero;
                }
            };
        }

        private void Start()
        {
            _flashLightDelay = new WaitForSeconds(_shootingFlashDuration);
            _shootingDelay = new WaitForSeconds(_gameData.ShootRate);

            StartCoroutine(ShootingCoroutine());
        }

        private IEnumerator ShootingCoroutine()
        {
            while (true)
            {
                if (_isEnemyFinded && _gameData.GameIsActive)
                {
                    StartCoroutine(ShowShootingLightCoroutine());
                    _audioSource.PlayOneShot(_shootClip);
                    Bullet bullet = _bulletPool.GetFromPool();
                    bullet.CachedCoroutine = bullet.StartCoroutine(bullet.MoovingToAimCoroutine(_enemyPosition, transform.position + Vector3.up));
                }
                yield return _shootingDelay;
            }
        }
        private IEnumerator ShowShootingLightCoroutine()
        {
            _shootingLight.SetActive(true);
            yield return _flashLightDelay;
            _shootingLight.SetActive(false);
        }
    }
}
