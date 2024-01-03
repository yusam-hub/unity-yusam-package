using System;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(YusamDebugProperties))]
    public class Shield : MonoBehaviour
    {
        private YusamDebugProperties _debugProperties;

        private void Awake()
        {
            _debugProperties = GetComponent<YusamDebugProperties>();
        }

        private void Update()
        {
            //OverlapSphere будет использоваться при разрушение шита чтобы собрать все объекты и их уничтожить типа
            // Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, 0);
        }
    }
}