using Leopotam.EcsLite;
using PFY.Game.Model.SubModel;
using PFY.Level.Settings;
using PFY.Level.View;
using PFY.Play.Model.Bomb.Base.Systems;
using PFY.Play.Model.Bomb.Cluster.AttackUnit.Systems;
using PFY.Play.Model.Bomb.Cluster.Systems;
using PFY.Play.Model.Bomb.Ordinary.Systems;
using PFY.Play.Model.Bomb.Selector.Systems;
using PFY.Play.Model.Bomb.Types.Creators;
using PFY.Play.Model.Bomb.Types.Systems;
using PFY.Play.Model.Bomb.Ui.Selected.Systems;
using PFY.Play.Model.Enemy.Systems;
using PFY.Play.Model.GameEnd.Systems;
using PFY.Play.Model.Ground.Systems;
using PFY.Play.Model.Layouts.Systems;
using PFY.Play.Model.UI.Systems;

namespace PFY.Play.Model
{
    public sealed class PlayModel : SubModel
    {
        public static SubModel Create(EcsWorld world, LevelLayout layout, LevelSettings levelSettings)
        {
            return new PlayModel(world, layout, levelSettings);
        }
        
        private PlayModel(EcsWorld world, LevelLayout layout, LevelSettings levelSettings) : base(world)
        {
            var fabricBombType = FabricBombType.Create();
            fabricBombType.RegistrationCreatorBombType(CreatorBombTypeOrdinary.Create());
            fabricBombType.RegistrationCreatorBombType(CreatorBombTypeCluster.Create());
            fabricBombType.CheckTypes();
            
            // Create
            AddSystem(SystemBombTypeCreate.Create(fabricBombType, levelSettings.bombsSettings));
            AddSystem(SystemPlayLevelLayoutCreate.Create(layout));
            AddSystem(SystemPlayLevelUiViewCreate.Create(layout.Ui));
            AddSystem(SystemBombSelectorUiViewCreate.Create(layout.Ui.BombSelectorUiLayout, levelSettings.bombsSettings));
            AddSystem(SystemPlayEnemiesCreate.Create(layout, levelSettings));
            AddSystem(SystemGroundCreate.Create(layout.Ground));

            // Run
            // UI
            AddSystem(SystemBombUiSelectedUpdate.Create());
            
            // Bomb
            AddSystem(SystemBombOrdinaryCreate.Create());
            AddSystem(SystemBombClusterCreate.Create());
            
            AddSystem(SystemBombCheckIsActive.Create());
            AddSystem(SystemBombUpdateViewPosition.Create());
            
            AddSystem(SystemBombOrdinaryCheckDetonation.Create());
            AddSystem(SystemBombClusterAttackUnitCheckDetonation.Create());
            AddSystem(SystemBombClusterCheckSeparate.Create());
            AddSystem(SystemBombClusterSeparating.Create());
            
            AddSystem(SystemBombDetonationTakeDamage.Create());
            AddSystem(SystemBombDetonationDestroy.Create());
            
            // Enemy
            AddSystem(SystemPlayEnemiesDie.Create());
            AddSystem(SystemPlayEnemiesCheckIsActive.Create());
            AddSystem(SystemPlayEnemiesUpdateNavigation.Create());

            // Game end
            AddSystem(SystemPlayGameEndButtonIsClick.Create());
            AddSystem(SystemPlayGameEndAllEnemyDie.Create());
            AddSystem(SystemPlayGameEnd.Create());
            
            // Destroy
            AddSystem(SystemBombDestroy.Create());
            AddSystem(SystemGroundDestroy.Create());
            AddSystem(SystemPlayEnemiesDestroy.Create());
            AddSystem(SystemBombSelectorUiViewDestroy.Create());
            AddSystem(SystemPlayLevelUiViewDestroy.Create());
            AddSystem(SystemPlayLevelLayoutDestroy.Create());
            AddSystem(SystemBombTypeDestroy.Create());
            
            Inject();
        }
    }
}