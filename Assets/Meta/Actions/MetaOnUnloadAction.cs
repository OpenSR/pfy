using Leopotam.EcsLite;
using PFY.Game.Model.SubModel.Components;
using PFY.Scenes.Actions;
using PFY.Utils;

namespace PFY.Meta.Actions
{
    public sealed class MetaOnUnloadAction : SceneActionOnUnload
    {
        public static SceneActionOnUnload Create()
        {
            return new MetaOnUnloadAction();
        }

        private MetaOnUnloadAction() { }

        public override void OnAction(EcsWorld world)
        {
            world.Filter<ComponentCurrentSubModel>().End().DeleteAllEntitiesFromWorld();
        }
    }
}