using UnityEngine;
using Zenject;

namespace Game.Components.Rotations
{
    public class RotationInstaller : Installer<RotationInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Contracts.ISmoothTargetRotation>()
                .To<SmoothTargetRotation>()
                .AsSingle();
        }
    }
}