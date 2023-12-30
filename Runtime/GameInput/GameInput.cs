using System;
using UnityEngine;

namespace YusamPackage.GameInput
{
    public abstract class GameInput : MonoBehaviour, IGameInput
    {
        [Header("Game Input")]
        [SerializeField] protected GameInputProxy gameInputProxy;

        public GameInputProxy GetGameInputProxy()
        {
            return gameInputProxy;
        }
        public bool HasGameInputProxy()
        {
            return gameInputProxy != null;
        }
    }
}