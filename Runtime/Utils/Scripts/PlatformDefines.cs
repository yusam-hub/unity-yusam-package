using UnityEngine;

namespace YusamPackage
{
    public class PlatformDefines : MonoBehaviour
    {
        private void Awake() 
        {

#if UNITY_EDITOR
            Debug.Log( $"{GetType()} - You are running in [ UNITY_EDITOR ]");
#endif

#if UNITY_IOS
            Debug.Log( $"{GetType()} - You are running in [ UNITY_IOS ]");
#endif

#if UNITY_STANDALONE_OSX
            Debug.Log( $"{GetType()} - You are running in [ UNITY_STANDALONE_OSX ]");
#endif

#if UNITY_STANDALONE_WIN
            Debug.Log( $"{GetType()} - You are running in [ UNITY_STANDALONE_WIN ]");
#endif

        }    
    }
}