using System;
using UnityEngine;

namespace YusamPackage.GameInput
{
    public abstract class GameInput : MonoBehaviour, IGameInput
    {
        [SerializeField] protected GameInputProxy gameInputProxy;

        public GameInputProxy GetGameInputProxy()
        {
            return gameInputProxy;
        }
    }
}