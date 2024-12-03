using Zenject;
using PlayFab;
using PlayFab.ClientModels;

public class GameGlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindManager();
    }

    private void BindManager()
    {
        Container.Bind<UILoadScreenManager>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("UILoadScreenManager")
            .AsSingle()
            .NonLazy();

    }
}
