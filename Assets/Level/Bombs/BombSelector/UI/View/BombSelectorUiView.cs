using System.Collections.Generic;
using PFY.Commands.Service;
using PFY.Level.Bombs.Bomb.UI.View;
using PFY.Level.Bombs.Settings.Scripts;
using PFY.Play.Model.Bomb.Selector.Commands;

namespace PFY.Level.Bombs.BombSelector.UI.View
{
    public sealed class BombSelectorUiView : IBombSelectorUiView
    {
        private Dictionary<int, IBombUiView> _bombUiViews;
        private IBombUiView _selectedBombUiView;
        private ICommandsService _commandsService;
        private int _waitActivationCount;

        public static IBombSelectorUiView Create(ICommandsService commandsService, BombSelectorUiLayout layout, BombsSettings bombsSettings)
        {
            return new BombSelectorUiView(commandsService, layout, bombsSettings);
        }
        
        private BombSelectorUiView(ICommandsService commandsService, BombSelectorUiLayout layout, BombsSettings bombsSettings)
        {
            _commandsService = commandsService;
            _selectedBombUiView = null;
            var bombSettingsList = bombsSettings.bombSettingsList;
            _bombUiViews = new Dictionary<int, IBombUiView>(bombSettingsList.Count);
            _waitActivationCount = bombSettingsList.Count;
            
            foreach (var bombSettings in bombSettingsList)
            {
                var bombUiView = BombUiView.Create(_bombUiViews.Count, bombSettings, layout.ScrollRect.content);
                bombUiView.EventOnClick += BombUiViewOnEventOnClick;
                bombUiView.EventOnActivation += BombUiViewOnEventOnActivation;
                _bombUiViews.Add(bombUiView.Id, bombUiView);
                bombUiView.Load();
            }
        }

        void IBombSelectorUiView.SelectBomb(int id)
        {
            _selectedBombUiView?.Unselected();
            if (_bombUiViews.TryGetValue(id, out _selectedBombUiView))
            {
                _selectedBombUiView.Selected();
            }
        }

        void IBombSelectorUiView.Destroy()
        {
            foreach (var bombUiView in _bombUiViews.Values)
            {
                bombUiView.EventOnActivation -= BombUiViewOnEventOnActivation;
                bombUiView.EventOnClick -= BombUiViewOnEventOnClick;
                bombUiView.Destroy();
            }
            _bombUiViews.Clear();
            _bombUiViews = null;

            _commandsService = null;
        }
        
        private void BombUiViewOnEventOnClick(IBombUiView obj)
        {
            _commandsService.Enqueue(CommandBombTypeSelect.Create(obj.Id, obj.BombType));
        }
        
        private void BombUiViewOnEventOnActivation()
        {
            _waitActivationCount--;
            if (_waitActivationCount <= 0 && _bombUiViews.TryGetValue(0, out var bombUiView))
            {
                _commandsService.Enqueue(CommandBombTypeSelect.Create(bombUiView.Id, bombUiView.BombType));
            }
        }
    }
}