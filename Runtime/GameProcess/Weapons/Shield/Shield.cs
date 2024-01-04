using System;
using System.Collections;
using UnityEngine;

namespace YusamPackage
{
    [RequireComponent(typeof(YusamDebugProperties))]
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
        
        private YusamDebugProperties _debugProperties;
        private bool _weaponActionInProcess;
        
        private void Awake()
        {
            _debugProperties = GetComponent<YusamDebugProperties>();
        }

        public void WeaponAction(Transform sourceTransform)
        {
            if (!_weaponActionInProcess)
            {
                StartCoroutine(ExecuteCoroutine(sourceTransform));
            }
        }

        private IEnumerator ExecuteCoroutine(Transform sourceTransform)
        {
            _weaponActionInProcess = true;
            
            float timer = shieldSo.prefabLifeTime;

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