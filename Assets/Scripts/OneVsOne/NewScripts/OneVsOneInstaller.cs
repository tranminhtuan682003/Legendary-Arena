using UnityEngine;
using Zenject;

public class OneVsOneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindManager();
    }
    private void BindManager()
    {
        Container.Bind<SpawnHeroSystem>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("SpawnHeroSystem")
            .AsSingle()
            .NonLazy();

        Container.Bind<GameOneVsOneController>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("GameOneVsOneController")
            .AsSingle()
            .NonLazy();
    }
}
