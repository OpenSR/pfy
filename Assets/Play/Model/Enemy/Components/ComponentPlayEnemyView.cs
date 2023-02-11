using Leopotam.EcsLite;
using PFY.Level.Enemy;
using PFY.Level.Enemy.View;

namespace PFY.Play.Model.Enemy.Components
{
    public struct ComponentPlayEnemyView : IEcsAutoReset<ComponentPlayEnemyView>
    {
        public ILevelEnemyView EnemyView;

        public void AutoReset(ref ComponentPlayEnemyView c)
        {
            c.EnemyView?.Destroy();
            c.EnemyView = null;
        }
    }
}