namespace TestTask.Pools
{
    public class BulletPool : CustomePool<Bullet>
    {
        public void Init(IFactory factory, int startCountofObjects)
        {
            InitPool(factory, startCountofObjects);
        }
    } 
}
