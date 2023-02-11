using Leopotam.EcsLite;
using PFY.Game.Model.SubModel.Components;
using PFY.Meta.Model;
using PFY.Meta.View;
using PFY.Scenes.Actions;
using UnityEngine.SceneManagement;

namespace PFY.Meta.Actions
{
    public sealed class MetaOnLoadAction : SceneActionOnLoad
    {
        public static SceneActionOnLoad Create()
        {
            return new MetaOnLoadAction();
        }

        private MetaOnLoadAction() { }

        public override void OnAction(EcsWorld world)
        {
            var metaLayout = SceneManager.GetActiveScene().GetRootGameObjects()[0].GetComponent<MetaLayout>();
            var poolCurrentSubModel = world.GetPool<ComponentCurrentSubModel>();
            var metaModel = MetaModel.Create(world, metaLayout);
            poolCurrentSubModel.Add(world.NewEntity()).SubModel = metaModel;
            metaModel.Init();
        }
    }
}