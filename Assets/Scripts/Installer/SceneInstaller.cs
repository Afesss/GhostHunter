using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Counter counter;
    public override void InstallBindings()
    {
        //Container.BindInstance(counter);
        Container.Bind<ICounter>().To<Counter>().FromInstance(counter).AsSingle();
    }
}