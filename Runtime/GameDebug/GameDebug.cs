using System;
using UnityEngine;

namespace YusamPackage
{
    public static class GameDebug
    {
        public static bool IsEnabled = true;
        
        /*
         * string val0 = "val0"
         * string val1 = "val1"
         * Log($"Key0: {val0}, key1: {val1}")
         */
        public static void Log(object message)
        {
            if (!IsEnabled) return;
            Debug.Log(message);
        }

        /*
         * LogFormat("Key0: {0}, key1: {1}","val0","val1")
         */
        public static void LogFormat(string format, params object[] args)
        {
            if (!IsEnabled) return;
            Debug.Log(String.Format(format, args));
        }
    }
}