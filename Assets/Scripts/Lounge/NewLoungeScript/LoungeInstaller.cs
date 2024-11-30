using UnityEngine;
using Zenject;

public class LoungeInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindManager();
    }
    private void BindManager()
    {
        Container.Bind<UILoungeManager>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("UILoungeManager")
            .AsSingle()
            .NonLazy();

        Container.Bind<LoungeManager>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("LoungeManager")
            .AsSingle()
            .NonLazy();
    }
}
