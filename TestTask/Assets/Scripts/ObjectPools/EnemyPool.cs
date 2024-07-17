using TestTask.EnemySystems;

namespace TestTask.Pools
{
    public class EnemyPool : CustomePool<EnemyData>
    {
        public void Init(IFactory factory, int startCountOfObjects)
        {
            InitPool(factory, startCountOfObjects);
        }
    } 
}
