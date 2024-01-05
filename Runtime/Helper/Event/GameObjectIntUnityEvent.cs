using System;
using UnityEngine;
using UnityEngine.Events;

namespace YusamPackage
{
    [Serializable]
    public class GameObjectIntUnityEvent : UnityEvent<GameObject,int> {}
}