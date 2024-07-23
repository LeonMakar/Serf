using Zenject;
using UnityEngine;
using Serfe.MVVM;
using Serfe.Models;
using Serfe.EventBusSystem;
using System.Collections.Generic;
using Serfe.Factory;
public class SceneInstaller : MonoInstaller, IInitializable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RunningData _runningData;
    [SerializeField] private View _view;
    [SerializeField] private List<TileAdjuster> _tileAdjusters;
    [SerializeField] private List<TileBonusPattern> _tileBonuses;


    public override void InstallBindings()
    {
        BindSceneInstallerInterfaces();
        BindEventBus();
        BindAnimator();
        InjectPlayerAnimation();
        BindRinningData();
        BindMVVM();
        BindTileAdjusterContainer();
        BindTileFactory();
        BindTileMemoryPool();
        BindTileBonusesContainer();
        BindTileBonusFactory();
        BindTileBonusMemoryPool();

    }

    private void BindTileBonusMemoryPool()
    {
        Container
            .Bind<MemoryPool<TileBonusPattern>>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }

    private void BindTileBonusFactory()
    {
        Container
            .Bind<Serfe.Factory.IFactory>()
            .WithId(typeof(BonusTileFactory))
            .To<BonusTileFactory>()
            .AsSingle()
            .NonLazy();
    }

    private void BindTileBonusesContainer()
    {
        Container
            .Bind<List<TileBonusPattern>>()
            .FromInstance(_tileBonuses)
            .AsSingle()
            .NonLazy();

        foreach (var item in _tileBonuses)
        {
            Container.QueueForInject(item);
        }
    }

    private void BindSceneInstallerInterfaces()
    {
        Container
            .BindInterfacesTo<SceneInstaller>()
            .FromInstance(this)
            .AsSingle()
            .NonLazy();
    }

    private void BindTileMemoryPool()
    {
        Container
            .Bind<MemoryPool<TileAdjuster>>()
            .FromNew()
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

    private void BindTileAdjusterContainer()
    {
        Container
            .Bind<List<TileAdjuster>>()
            .FromInstance(_tileAdjusters)
            .AsSingle()
            .NonLazy();

        foreach (var item in _tileAdjusters)
        {
            Container.QueueForInject(item);
        }
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

    private void BindEventBus()
    {
        Container
            .Bind<EventBus>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }

    public void Initialize()
    {
        TileFactory tileFactory = Container.TryResolveId<Serfe.Factory.IFactory>(typeof(TileFactory)) as TileFactory;
        BonusTileFactory bonusTileFactory = Container.TryResolveId<Serfe.Factory.IFactory>(typeof(BonusTileFactory)) as BonusTileFactory;
        //tileFactory.InitTileFactory(_tileAdjusters);
        Container.Resolve<MemoryPool<TileAdjuster>>().InitMemoryPool(tileFactory, tileFactory.PrefabsOfTileWhithType);
        Container.Resolve<MemoryPool<TileBonusPattern>>().InitMemoryPool(bonusTileFactory, bonusTileFactory.PrefabsOfTileWhithType);
    }
}
