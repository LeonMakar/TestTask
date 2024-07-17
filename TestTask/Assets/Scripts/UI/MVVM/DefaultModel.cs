
using TestTask.Pools;

namespace TestTask.MVVM
{
    public class DefaultModel : Model
    {
        public override void Init(MainData mainData, EventBus eventBus, EnemyPool enemyPool)
        {
            base.Init(mainData, eventBus, enemyPool);
        }
    } 
}
