
using UnityEngine;
using Zenject;

public interface IFactory
{
    void PrepareFactory(DiContainer diContainer);
    GameObject Create();
}
