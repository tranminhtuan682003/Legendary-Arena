using UnityEngine;
using Zenject;

public class GameKnightInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Bind ButtonControlManager
        Container.Bind<ButtonControlManager>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("ButtonControlManager")
            .AsSingle()
            .NonLazy();

        // Bind PoolKnightSoundManager
        Container.Bind<PoolSoundKnightManager>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("PoolSoundKnightManager")
            .AsSingle()
            .NonLazy();

        // Bind SoundKnightManager
        Container.Bind<SoundKnightManager>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("SoundKnightManager")
            .AsSingle()
            .NonLazy();

        // Bind GameKnightManager
        Container.Bind<GameKnightManager>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("GameKnightManager")
            .AsSingle()
            .NonLazy();

        // Bind PoolKnightManager
        Container.Bind<PoolKnightManager>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("PoolKnightManager")
            .AsSingle()
            .NonLazy();

        // Bind PoolKnightManager
        Container.Bind<PoolEnemyKnightManager>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("PoolEnemyKnightManager")
            .AsSingle()
            .NonLazy();

        // Bind UIKnightManager
        Container.Bind<UIKnightManager>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("UIKnightManager")
            .AsSingle()
            .NonLazy();

        // Bind KnightStartScreenManager
        Container.Bind<KnightStartScreenManager>()
            .FromMethod(context =>
            {
                var uiKnightManager = context.Container.Resolve<UIKnightManager>();
                return Container.InstantiatePrefabForComponent<KnightStartScreenManager>(
                    uiKnightManager.GetKnightStartScreenPrefab()
                );
            })
            .AsSingle();

        // Bind KnightPlayScreenManager
        Container.Bind<KnightPlayScreenManager>()
            .FromMethod(context =>
            {
                var uiKnightManager = context.Container.Resolve<UIKnightManager>();
                return Container.InstantiatePrefabForComponent<KnightPlayScreenManager>(
                    uiKnightManager.GetKnightPlayScreenPrefab()
                );
            })
            .AsSingle();
    }
}
