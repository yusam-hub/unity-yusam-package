using System.Collections;
using UnityEngine;

namespace YusamPackage
{
    public class Shield : MonoBehaviour, IWeaponAction, IShield
    {
        [Space(10)]
        [YusamHelpBox("Настройки обекта", 2)]
        [Space(10)]
        [SerializeField] private ShieldSo shieldSo;
        
        [Space(10)]
        [YusamHelpBox("Объект который будет создаваться", 2)]
        [Space(10)]
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
            float timer = shieldSo.scriptLifeTime;
            
            GameObject newGameObject = Instantiate(prefabToBeSpawn, sourceTransform);
            
            while (timer > 0)
            {
                timer -= Time.deltaTime;
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