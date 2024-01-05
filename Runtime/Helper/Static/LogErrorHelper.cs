using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace YusamPackage
{
    public static class LogErrorHelper
    {

        /*
         * LogErrorHelper.NotFoundWhatIn(typeof(class).ToString(), this);
         */
        public static void NotFoundWhatIn(string what, Object inObj)
        {
            Debug.LogError(String.Format("{0} not found in [ {1} : {2} ]", what, inObj.GetType(), inObj.name));
        }
        
        public static void NotFoundWhatInIf(bool condition, string what, Object inObj)
        {
            if (condition)
            {
                Debug.LogError(String.Format("{0} not found in [ {1} : {2} ]", what, inObj.GetType(), inObj.name));
            }
        }
       
        /*
         * LogErrorHelper.NotImplementedWhatIn(typeof(class).ToString(), this);
         */
        public static void NotImplementedWhatIn(string what, Object inObj)
        {
            Debug.LogError(String.Format("{0} not implemented in [ {1} : {2} ]", what, inObj.GetType(), inObj.name));
        }
    }
}