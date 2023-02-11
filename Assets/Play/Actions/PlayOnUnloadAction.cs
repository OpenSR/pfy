using Leopotam.EcsLite;
using PFY.Game.Model.SubModel.Components;
using PFY.Scenes.Actions;
using PFY.Utils;

namespace PFY.Play.Actions
{
    public sealed class PlayOnUnloadAction : SceneActionOnUnload
    {
        public static SceneActionOnUnload Create()
        {
            return new PlayOnUnloadAction();
        }

        private PlayOnUnloadAction() { }

        public override void OnAction(EcsWorld world)
        {
            world.Filter<ComponentCurrentSubModel>().End().DeleteAllEntitiesFromWorld();
        }
    }
}