using Leopotam.EcsLite;
using PFY.Commands;
using PFY.Play.Model.Bomb.Drop.Components;
using UnityEngine;

namespace PFY.Play.Model.Bomb.Drop.Commands
{
    public sealed class CommandBombDrop : Command
    {
        private Vector3 _targetPosition;

        public static Command Create(Vector3 targetPosition)
        {
            return new CommandBombDrop(targetPosition);
        }

        private CommandBombDrop(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }
        
        public override void Apply(EcsWorld world)
        {
            var bombDropEntityId = world.NewEntity();
            world.GetPool<ComponentBombDropTag>().Add(bombDropEntityId);
            world.GetPool<ComponentBombDropTargetPosition>().Add(bombDropEntityId).TargetPosition = _targetPosition;
        }
    }
}