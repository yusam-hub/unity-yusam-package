using System;
using UnityEngine;

namespace YusamPackage
{
    public class DeathZone : MonoBehaviour, IDeathZone
    {
        [SerializeField] private DeathZoneSo deathZoneSo;

        private void Awake()
        {
            if (deathZoneSo == null)
            {
                Debug.LogError("Death Zone So prefab not found in [ " + this + "]");
                gameObject.SetActive(false);
            }
            
            //Physics.BoxCast()
        }
    }
}