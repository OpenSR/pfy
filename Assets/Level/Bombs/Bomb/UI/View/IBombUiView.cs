using System;
using PFY.Level.Bombs.Bomb.Types;

namespace PFY.Level.Bombs.Bomb.UI.View
{
    public interface IBombUiView
    {
        event Action EventOnActivation;
        event Action<IBombUiView> EventOnClick;
        bool IsActive { get; }
        int Id { get; }
        BombTypes BombType { get; }

        void Load();
        void Selected();
        void Unselected();
        void Destroy();
    }
}