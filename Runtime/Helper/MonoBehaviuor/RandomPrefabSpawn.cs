using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YusamPackage
{
    public class RandomPrefabSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int maxCount = 5;
        [SerializeField] private Bounds bounds = new Bounds(Vector3.zero, new Vector3(20,0,20));
        
        private void Awake()
        {
            MaxRandomSpawnXZ();
        }

        public void MaxRandomSpawnXZ()
        {
            for (int i = 0; i < maxCount; i++)
            {
                SingleRandomSpawn();
            }
        }

        private void SingleRandomSpawn()
        {
            if (prefab == null) return;
            
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);
            Vector3 newPosition = new Vector3(x, 0, z);
            Instantiate(prefab, newPosition, Quaternion.identity);
        }
    }
}