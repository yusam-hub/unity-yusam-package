using UnityEngine;

namespace YusamPackage
{
    public static class PlayerPrefsHelper
    {
        public static void SetParameter(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public static void SetParameter(string key, int index, float value)
        {
            PlayerPrefs.SetFloat(key + "_" + index, value);
            PlayerPrefs.Save();
        }

        public static void SetParameter(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public static void SetParameter(string key, int index, int value)
        {
            PlayerPrefs.SetInt(key + "_" + index, value);
            PlayerPrefs.Save();
        }

        public static void SetParameter(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static void SetParameter(string key, string id, bool value)
        {
            PlayerPrefs.SetInt(key + "_" + id, value ? 1 : 0);
            PlayerPrefs.Save();
        }
		
        public static void SetParameter(string key, int index, bool value)
        {
            PlayerPrefs.SetInt(key + "_" + index, value ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static void SetParameter(string key, Vector3 value)
        {
            string nameX = key + "_" + "X";
            string nameY = key + "_" + "Y";
            string nameZ = key + "_" + "Z";

            PlayerPrefs.SetFloat(nameX, value.x);
            PlayerPrefs.SetFloat(nameY, value.y);
            PlayerPrefs.SetFloat(nameZ, value.z);

            PlayerPrefs.Save();
        }

        public static void SetParameter(string key, string id, Vector3 value)
        {
            string nameX = key + "_" + id + "_" + "X";
            string nameY = key + "_" + id + "_" + "Y";
            string nameZ = key + "_" + id + "_" + "Z";

            PlayerPrefs.SetFloat(nameX, value.x);
            PlayerPrefs.SetFloat(nameY, value.y);
            PlayerPrefs.SetFloat(nameZ, value.z);

            PlayerPrefs.Save();
        }

        public static float GetParameter(string key, float defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }
            return PlayerPrefs.GetFloat(key);
        }

        public static float GetParameter(string key, int index, float defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }
            return PlayerPrefs.GetFloat(key + "_" + index);
        }

        public static int GetParameter(string key, int defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }
            return PlayerPrefs.GetInt(key);
        }

        public static int GetParameter(string key, int index, int defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }
            return PlayerPrefs.GetInt(key + "_" + index);
        }

        public static bool GetParameter(string key, bool defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }
            return PlayerPrefs.GetInt(key) == 1;
        }

        public static bool GetParameter(string key, int index, bool defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }
            return PlayerPrefs.GetInt(key + "_" + index) == 1;
        }
		
        public static bool GetParameter(string key, string id, bool defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }
            return PlayerPrefs.GetInt(key + "_" + id) == 1;
        }

        public static Vector3 GetParameter(string key, Vector3 defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }
            
            Vector3 result = Vector3.zero;

            result.x = PlayerPrefs.GetFloat(key + "_" + "X");
            result.y = PlayerPrefs.GetFloat(key + "_" + "Y");
            result.z = PlayerPrefs.GetFloat(key + "_" + "Z");

            return result;
        }

        public static Vector3 GetParameter(string key, string id, Vector3 defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return defaultValue;
            }
            Vector3 result = Vector3.zero;

            result.x = PlayerPrefs.GetFloat(key + "_" + id + "_" + "X");
            result.y = PlayerPrefs.GetFloat(key + "_" + id + "_" + "Y");
            result.z = PlayerPrefs.GetFloat(key + "_" + id + "_" + "Z");

            return result;
        }
    }
}