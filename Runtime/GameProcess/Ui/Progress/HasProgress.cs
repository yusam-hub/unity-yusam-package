using System;
using UnityEngine;

namespace YusamPackage
{
    public class HasProgress : MonoBehaviour, IHasProgress
    {
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

        public void DoProgressChanged(float t)
        {
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                ProgressNormalized = t
            });
        }
    }
}