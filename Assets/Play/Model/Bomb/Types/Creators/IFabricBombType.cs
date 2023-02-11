using Leopotam.EcsLite;
using PFY.Level.Bombs.Bomb.Settings.Scripts;
using PFY.Level.Bombs.Bomb.Types;

namespace PFY.Play.Model.Bomb.Types.Creators
{
    public interface IFabricBombType
    {
        void RegistrationCreatorBombType(CreatorBombType creator);
        void CheckTypes();
        void ApplyCreatorBombType(EcsWorld world, BombSettings settings);
    }
}