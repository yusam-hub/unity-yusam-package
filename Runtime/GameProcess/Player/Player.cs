using System;
using UnityEngine;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private Experience experience;
        
        private void Awake()
        {
            experience.OnChangedExperience += InstanceOnOnChangedExperience;
        }

        private void InstanceOnOnChangedExperience(object sender, Experience.OnChangedExperienceEventArgs e)
        {
            if (TryGetComponent(out Health health))
            {
                health.SetHealthSo(e.CurrentExperienceStruct.healthSo);
            }
            if (TryGetComponent(out ShieldController shieldController))
            {
                shieldController.SetShieldSo(e.CurrentExperienceStruct.shieldSo);
            }
            if (TryGetComponent(out SwordController swordController))
            {
                swordController.SetSwordSo(e.CurrentExperienceStruct.swordSo);
            }
            if (TryGetComponent(out ShootController shootController))
            {
                shootController.SetShootBulletSo(e.CurrentExperienceStruct.shootBulletSo);
            }
        }

        private void OnDestroy()
        {
            experience.OnChangedExperience -= InstanceOnOnChangedExperience;
        }
    }
}