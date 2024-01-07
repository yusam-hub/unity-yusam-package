using UnityEngine;

namespace YusamPackage
{
    public class PrefabSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawnPoint;
        
        private void Awake()
        {
            Spawn();
        }
        private void Spawn()
        {
            if (prefab != null && spawnPoint != null)
            {
                Instantiate(prefab, spawnPoint);
            } 
        }
    }
}