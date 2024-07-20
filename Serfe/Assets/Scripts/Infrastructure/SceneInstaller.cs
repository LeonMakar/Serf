using Zenject;
public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindEventBus();
    }

    private void BindEventBus()
    {
        Container
            .Bind<EventBus>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}
