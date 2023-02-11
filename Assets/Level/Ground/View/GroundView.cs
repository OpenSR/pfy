using PFY.Commands.Service;
using PFY.Play.Model.Bomb.Drop.Commands;
using UnityEngine;

namespace PFY.Level.Ground.View
{
    public sealed class GroundView : IGroundView
    {
        Transform IGroundView.GroundTransform => _layout.transform;
        
        private GroundLayout _layout;
        private ICommandsService _commandsService;
        
        public static IGroundView Create(GroundLayout layout, ICommandsService commandsService)
        {
            return new GroundView(layout, commandsService);
        }
        
        private GroundView(GroundLayout layout, ICommandsService commandsService)
        {
            _layout = layout;
            _commandsService = commandsService;
            
            _layout.EventOnClick += LayoutOnEventOnClick;
        }

        void IGroundView.Destroy()
        {
            if (_layout)
            {
                _layout.EventOnClick -= LayoutOnEventOnClick;
            }
            
            _layout = null;
            _commandsService = null;
        }
        
        private void LayoutOnEventOnClick(Vector3 obj)
        {
            _commandsService.Enqueue(CommandBombDrop.Create(obj));
        }
    }
}