using System.Collections;
using UnityEngine;
using Zenject;

public class GameKnightInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ButtonPlayKnightManager>().AsSingle();
        Container.Bind<GameKnightManager>().FromNewComponentOnNewGameObject().WithGameObjectName("GameKnightManager").AsSingle().NonLazy();
        Container.Bind<UIKnightManager>().FromNewComponentOnNewGameObject().WithGameObjectName("UIKnightManager").AsSingle().NonLazy();
        Container.Bind<SoundKnightManager>().FromNewComponentOnNewGameObject().WithGameObjectName("SoundKnightManager").AsSingle().NonLazy();

        Container.Bind<IScreenKnightManager>().To<KnightStartScreenManager>().FromMethod(context =>
            {
                var uiKnightManager = context.Container.Resolve<UIKnightManager>();
                return Container.InstantiatePrefabForComponent<KnightStartScreenManager>(
                    uiKnightManager.GetKnightStartScreenPrefab()
                );
            }).AsSingle();

        Container.Bind<IScreenKnightManager>().To<KnightPlayScreenManager>().FromMethod(context =>
            {
                var uiKnightManager = context.Container.Resolve<UIKnightManager>();
                return Container.InstantiatePrefabForComponent<KnightPlayScreenManager>(
                    uiKnightManager.GetKnightStartScreenPrefab()
                );
            }).AsSingle();

    }
}
