using Leopotam.EcsLite;
using PFY.Commands;
using PFY.Level.Bombs.Bomb.Types;
using PFY.Play.Model.Bomb.Types.Components;
using PFY.Play.Model.Bomb.Ui.Selected.Components;

namespace PFY.Play.Model.Bomb.Selector.Commands
{
    public sealed class CommandBombTypeSelect : Command
    {
        private readonly int _id;
        private readonly BombTypes _type;

        public static Command Create(int id, BombTypes type)
        {
            return new CommandBombTypeSelect(id, type);
        }
        
        private CommandBombTypeSelect(int id, BombTypes type)
        {
            _id = id;
            _type = type;
        }
        
        public override void Apply(EcsWorld world)
        {
            var bombUiSelectedEntityId = world.NewEntity();
            world.GetPool<ComponentBombUiSelectedTag>().Add(bombUiSelectedEntityId);
            world.GetPool<ComponentBombUiSelectedId>().Add(bombUiSelectedEntityId).Id = _id;

            var filterBombTypeSelected = world.Filter<ComponentBombTypeSelectedTag>().End();
            var poolBombTypeSelected = world.GetPool<ComponentBombTypeSelectedTag>();
            var filterBombType = world.Filter<ComponentBombType>().End();
            var poolBombType = world.GetPool<ComponentBombType>();
            
            foreach (var bombTypeSelectedEntityId in filterBombTypeSelected)
            {
                poolBombTypeSelected.Del(bombTypeSelectedEntityId);
            }

            foreach (var bombTypeEntityId in filterBombType)
            {
                if (poolBombType.Get(bombTypeEntityId).BombType == _type)
                {
                    poolBombTypeSelected.Add(bombTypeEntityId);
                    break;
                }
            }
        }
    }
}