using UnityEngine;

namespace TestTask.EnemySystems
{
    public class EnemyData : MonoBehaviour, IDamagable
    {
        public Transform FinishTransform { get; private set; }
        public MainData GameData { get; private set; }
        public EventBus EventBus { get; private set; }
        private int _health;


        [SerializeField] private EnemyMovement _enemyMovement;

        public void Init(MainData gameData, EventBus eventBus)
        {
            GameData = gameData;
            EventBus = eventBus;
            _health = gameData.EnemyHealth;
        }
        public EnemyData Spawn(Transform finishLineTransfrom, Vector3 transferGameObjectPosition)
        {
            FinishTransform = finishLineTransfrom;
            transform.position = transferGameObjectPosition;
            _enemyMovement.StartMooving();
            return this;
        }
        public void RestoreHealth() => _health = GameData.EnemyHealth;

        public void GetDamage(int damageValue)
        {
            _health = Mathf.Clamp(_health - damageValue, 0, 10000);
            if (_health == 0)
            {
                gameObject.SetActive(false);
                EventBus.OnEnemyKilled();
                RestoreHealth();
            }
        }
    }
}
