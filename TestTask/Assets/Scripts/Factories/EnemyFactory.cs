using TestTask.EnemySystems;
using UnityEngine;
using Zenject;

namespace TestTask.Factory
{
    public class EnemyFactory : IFactory
    {
        private DiContainer _diContainer;
        private GameObject _objectToCreate;
        public GameObject Create()
        {
            var cloneOfObjects = GameObject.Instantiate(_objectToCreate);
            InitObject(cloneOfObjects);
            return cloneOfObjects;
        }

        public void PrepareFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _objectToCreate = (GameObject)Resources.Load("Prefabs/Enemy");

        }

        private void InitObject(GameObject objectToInit)
        {
            if (objectToInit.TryGetComponent(out EnemyData enemyData))
            {
                enemyData.Init(_diContainer.Resolve<MainData>(), _diContainer.Resolve<EventBus>());
            }
        }
    }
}
