using UnityEngine;
using Zenject;

namespace Game.Components.Movements
{
    public class MovementInstaller : Installer<MovementInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Contracts.ISmoothTargetMovement>()
                .To<SmoothTargetMovement>()
                .AsSingle();
            Container
                .Bind<Contracts.IMovementDetection>()
                .To<MovementDetection>()
                .AsSingle();
        }
    }
}