using UnityEngine;


namespace FrameworkUnity.Subsystems
{
    public static class SaveSystem
    {
        public static void Save<T>(string key, T saveData)
        {
            string jsonDataString = JsonUtility.ToJson(saveData, true);
            PlayerPrefs.SetString(key, jsonDataString);
        }

        public static T Load<T>(string key) where T : new()
        {
            if (PlayerPrefs.HasKey(key))
            {
                string jsonDataString = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<T>(jsonDataString);
            }
            else
            {
                return new T();
            }
        }
    }
}
