using Zenject;
using UnityEngine;
using Serfe.MVVM;
using Serfe.Models;
using Serfe.EventBusSystem;
public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RunningData _runningData;
    [SerializeField] private View _view;

    public override void InstallBindings()
    {
        BindEventBus();
        BindAnimator();
        InjectPlayerAnimation();
        BindRinningData();
        BindMVVM();

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
}
