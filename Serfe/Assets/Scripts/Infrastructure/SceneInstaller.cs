using Zenject;
using UnityEngine;
using Serfe.MVVM;
using Serfe.Models;
using Serfe.Factory;
using Serfe.TileContainer;
using Serfe.Pools;
public class SceneInstaller : MonoInstaller, IInitializable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RunningData _runningData;
    [SerializeField] private View _view;
    [SerializeField] private FoundationTile _forestTile;


    public override void InstallBindings()
    {
        BindSceneInstallerInterfaces();
        BindAnimator();
        InjectPlayerAnimation();
        BindRinningData();
        BindMVVM();
        BindForestFoundationTile();
        BindTileFactory();
        BindForestTilePool();

    }

    private void BindForestFoundationTile()
    {
        Container
            .Bind<FoundationTile>()
            .WithId(TileType.Forest)
            .FromInstance(_forestTile)
            .AsSingle()
            .NonLazy();
    }

    public void Initialize()
    {
        CustomePool<FoundationTile> forestFoundationPool = Container.TryResolveId<CustomePool<FoundationTile>>(TileType.Forest);
        forestFoundationPool.InitPool(Container.ResolveId<Serfe.Factory.IFactory>(typeof(TileFactory)), 10);
    }

    private void BindForestTilePool()
    {
        Container
            .Bind<CustomePool<FoundationTile>>()
            .WithId(TileType.Forest)
            .To<ForestFoundationTilePool>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }


    private void BindSceneInstallerInterfaces()
    {
        Container
            .BindInterfacesTo<SceneInstaller>()
            .FromInstance(this)
            .AsSingle()
            .NonLazy();
    }

    private void BindTileFactory()
    {
        Container
            .Bind<Serfe.Factory.IFactory>()
            .WithId(typeof(TileFactory))
            .To<TileFactory>()
            .AsSingle()
            .NonLazy();
    }


    private void BindMVVM()
    {
        Container
            .Bind<ViewModel>()
            .To<DefaultViewModel>()
            .FromNew()
            .AsSingle()
            .NonLazy();


        Container
        .Bind<View>()
        .FromInstance(_view)
        .AsSingle()
        .NonLazy();
    }

    private void BindRinningData()
    {
        Container
            .Bind<RunningData>()
            .FromInstance(_runningData)
            .AsSingle()
            .NonLazy();

        Container.QueueForInject(_runningData);
    }

    private void InjectPlayerAnimation()
    {
        PlayerAnimation playerAnimation = new PlayerAnimation();
        Container.QueueForInject(playerAnimation);
    }

    private void BindAnimator()
    {
        Container
            .Bind<Animator>()
            .FromInstance(_animator)
            .AsSingle()
            .NonLazy();
    }


}
