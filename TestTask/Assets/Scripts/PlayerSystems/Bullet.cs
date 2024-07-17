using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    public Coroutine CachedCoroutine;
    private int _activateBomb = Animator.StringToHash("ActivateBomb");
    private WaitForSeconds _delay = new WaitForSeconds(0.5f);
    private int _bulletDamage;
    private float _bulletSpeed;
    private bool _isDestroyStarted;


    public void Init(float bulletSpeed, int bulletDamage)
    {
        _bulletDamage = bulletDamage;
        _bulletSpeed = bulletSpeed;
    }
     
    public IEnumerator MoovingToAimCoroutine(Vector2 aimPosition, Vector2 startPosition)
    {
        transform.position = startPosition;
        float distance = Vector2.Distance(aimPosition, startPosition);
        float remeningDistance = distance;

        while (remeningDistance > 0)
        {
            transform.position = Vector2.Lerp(startPosition, aimPosition, 1 - (remeningDistance / distance));
            remeningDistance -= _bulletSpeed * Time.deltaTime;
            yield return null;
        }
        if (!_isDestroyStarted)
            StartCoroutine(WaitToDestroyCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constans.ENEMY_LAYER_VALUE)
        {
            _isDestroyStarted = true;
            StopCoroutine(CachedCoroutine);
            StartCoroutine(WaitToDestroyCoroutine());
            if (collision.transform.parent.TryGetComponent(out IDamagable damagableObject))
            {
                damagableObject.GetDamage(_bulletDamage);
            }
        }
    }

    private IEnumerator WaitToDestroyCoroutine()
    {
        _animator.SetTrigger(_activateBomb);
        yield return _delay;
        gameObject.SetActive(false);
        _isDestroyStarted = false;
    }
}
