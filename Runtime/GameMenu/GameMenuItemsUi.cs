using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage.GameMenu
{
    public class GameMenuItemsUi : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Awake: " + this.name);
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy: " + this.name);
        }
    }
}
