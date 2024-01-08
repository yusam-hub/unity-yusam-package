using System;
using UnityEngine;
using UnityEngine.Events;

namespace YusamPackage
{
    [DisallowMultipleComponent]
    public class Experience : MonoBehaviour
    {
        public static Experience Instance { get; private set; }
        
        [Serializable]
        public struct ExperienceStruct
        {
            public HealthSo healthSo;
            public SwordSo swordSo;
            public ShieldSo shieldSo;
            public ShootBulletSo shootBulletSo;
        }
        
        [Serializable]
        public struct BonusToExperienceStruct
        {
            public int experienceIndex;
            public int bonusFrom;
            public int bonusTo;
        }
        
        [SerializeField] private ExperienceStruct[] experienceStructArray;
        [SerializeField] private BonusToExperienceStruct[] bonusToExperienceStructArray;

        [SerializeField] private int currentExperienceIndex;
        [SerializeField] private int currentBonus;
        
        public StringUnityEvent onChangeExperience;
        public StringUnityEvent onChangeBonus;

        public class OnChangedExperienceEventArgs : EventArgs
        {
            public ExperienceStruct CurrentExperienceStruct;
        }
        
        public event EventHandler<OnChangedExperienceEventArgs> OnChangedExperience; //для подписки компонентов
        
        private ExperienceStruct _currentExperienceStruct;
        private BonusToExperienceStruct _currentBonusToExperienceStruct;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(Instance);
            }

            Instance = this;
            UpdateCurrent();
        }
        
        public ExperienceStruct GetCurrentExperienceStruct()
        {
            return _currentExperienceStruct;
        }

        private void UpdateCurrent()
        {
            _currentExperienceStruct = experienceStructArray[currentExperienceIndex];
            
            var ind = FindBonusToExperienceStructIndex();
            if (ind >= 0 && ind < bonusToExperienceStructArray.Length)
            {
                _currentBonusToExperienceStruct = bonusToExperienceStructArray[ind];
            }
            
            onChangeExperience?.Invoke(currentExperienceIndex.ToString());
            OnChangedExperience?.Invoke(this, new OnChangedExperienceEventArgs
            {
                CurrentExperienceStruct = _currentExperienceStruct
            });
        }

        private int FindBonusToExperienceStructIndex()
        {
            var index = 0;
            foreach (var bonusToExperienceStruct in bonusToExperienceStructArray)
            {
                if (currentBonus >= bonusToExperienceStruct.bonusFrom &&
                    currentBonus < bonusToExperienceStruct.bonusTo)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public void AddBonus(int volume)
        {
            currentBonus += volume;
            
            onChangeBonus?.Invoke(currentBonus.ToString());
            
            if (currentBonus > _currentBonusToExperienceStruct.bonusTo)
            {
                currentExperienceIndex++;
                if (currentExperienceIndex >= experienceStructArray.Length)
                {
                    currentExperienceIndex = experienceStructArray.Length - 1;
                    return;
                }
                
                UpdateCurrent();
            }
        }
    }
}