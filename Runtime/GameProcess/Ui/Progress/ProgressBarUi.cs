using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YusamPackage
{
    public class ProgressBarUi : MonoBehaviour
    {
        [SerializeField] private GameObject hasProgressGameObject;
        [SerializeField] private Image barImage;

        private IHasProgress _hasProgress;

        private void Awake()
        {
            _hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

            if (_hasProgress == null)
            {
                Debug.LogError("Game Object " + hasProgressGameObject +
                               " does not have a component that implements IHasProgress!");
                return;
            }
            _hasProgress.OnProgressChanged += HasProgressOnProgressChanged;
        }

        private void HasProgressOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
        {
            barImage.fillAmount = e.ProgressNormalized;

            if (e.ProgressNormalized <= 0f)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}