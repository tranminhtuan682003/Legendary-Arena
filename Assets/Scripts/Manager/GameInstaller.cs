using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Bind các Manager dưới dạng Singleton với Lazy để chỉ khởi tạo khi cần
        Container.Bind<UIManager>().AsSingle().Lazy();
        Container.Bind<SkillManager>().AsSingle().Lazy();

        // Và bind các Manager khác tương tự...
    }
}
