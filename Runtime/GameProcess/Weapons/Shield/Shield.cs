using System.Collections;
using UnityEngine;

namespace YusamPackage
{
    public class Shield : MonoBehaviour, IWeaponAction
    {
        [SerializeField] private ShieldSo shieldSo;
        public GameObject prefabToBeSpawn;
        
        private bool _weaponActionInProcess;
        
        public void WeaponAction(Transform sourceTransform)
        {
            if (!_weaponActionInProcess)
            {
                _weaponActionInProcess = true;
                StartCoroutine(ExecuteCoroutine(sourceTransform));
            }
        }

        private IEnumerator ExecuteCoroutine(Transform sourceTransform)
        {
            var activeLifeTime = shieldSo.activeLifeTime;
            
            var newGameObject = Instantiate(prefabToBeSpawn, sourceTransform);
            
            while (activeLifeTime > 0)
            {
                activeLifeTime -= Time.deltaTime;
                yield return null;
            }
            
            Destroy(newGameObject);
            
            _weaponActionInProcess = false;
        }

        private void Update()
        {
            //OverlapSphere будет использоваться при разрушение шита чтобы собрать все объекты и их уничтожить типа
            // Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, 0);
        }
    }
}