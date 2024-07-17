using UnityEngine;
using Zenject;

namespace TestTask.Factory
{
    public class BulletFactory : IFactory
    {
        private GameObject _objectToCreate;
        private DiContainer _container;
        public GameObject Create()
        {
            var cloneOFObject = GameObject.Instantiate(_objectToCreate);
            InitObject(cloneOFObject);
            return cloneOFObject;
        }

        public void PrepareFactory(DiContainer diContainer)
        {
            _container = diContainer;
            _objectToCreate = (GameObject)Resources.Load("Prefabs/Bullet");
        }
        private void InitObject(GameObject objectToInit)
        {
            if (objectToInit.TryGetComponent(out Bullet bullet))
            {
                MainData mainData = _container.Resolve<MainData>();
                bullet.Init(mainData.BulletSpeed, mainData.ShootDamage);
            }
        }

    }
}
