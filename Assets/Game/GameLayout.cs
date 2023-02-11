using PFY.Game.Model;
using PFY.Game.Settings.Scripts;
using PFY.Loader;
using PFY.Loader.View;
using PFY.Share;
using UnityEngine;

namespace PFY.Game
{
    public sealed class GameLayout : Layout
    {
        [SerializeField]
        private LoaderLayout loaderLayout;
        [SerializeField]
        private GameSettings gameSettings;

        private IGameModel _gameModel;
        
        private void Awake()
        {
            UnityEngine.Application.targetFrameRate = 60;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _gameModel = GameModel.Create(loaderLayout, gameSettings);
        }

        private void Update()
        {
            _gameModel?.Update();
        }

        private void OnDestroy()
        {
            _gameModel?.Destroy();
            _gameModel = null;
        }
    }
}