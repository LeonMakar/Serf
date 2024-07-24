using Serfe.EventBusSystem;
using Serfe.TileContainer;
using Zenject;
public class ProjectsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindEventBus();
        BindTileFoundationContainer();
    }

    private void BindTileFoundationContainer()
    {
        Container
                    .Bind<TileFoundationContainer>()
                    .FromNew()
                    .AsSingle()
                    .NonLazy();
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
