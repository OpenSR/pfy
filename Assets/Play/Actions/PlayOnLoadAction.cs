using Leopotam.EcsLite;
using PFY.Game.Model.SubModel.Components;
using PFY.Level;
using PFY.Level.Settings;
using PFY.Level.View;
using PFY.Play.Model;
using PFY.Scenes.Actions;
using UnityEngine.SceneManagement;

namespace PFY.Play.Actions
{
    public sealed class PlayOnLoadAction : SceneActionOnLoad
    {
        private LevelSettings _levelSettings;
        
        public static SceneActionOnLoad Create(LevelSettings levelSettings)
        {
            return new PlayOnLoadAction(levelSettings);
        }

        private PlayOnLoadAction(LevelSettings levelSettings)
        {
            _levelSettings = levelSettings;
        }


        public override void OnAction(EcsWorld world)
        {
            var levelLayout = SceneManager.GetActiveScene().GetRootGameObjects()[0].GetComponent<LevelLayout>();
            var poolCurrentSubModel = world.GetPool<ComponentCurrentSubModel>();
            var playModel = PlayModel.Create(world, levelLayout, _levelSettings);
            poolCurrentSubModel.Add(world.NewEntity()).SubModel = playModel;
            playModel.Init();
            _levelSettings = null;
        }
    }
}