using System;
using UnityEngine;
using UnityEngine.Events;

namespace YusamPackage
{
    [Serializable]
    public class GameObjectStringUnityEvent : UnityEvent<GameObject,string> {}
}