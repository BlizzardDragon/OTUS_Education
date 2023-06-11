using System.Collections.Generic;


namespace FrameworkUnity.Subsystems
{
    public enum LocalizationTypes
    {
        English,
        Russian,
    }

    public class LocalizationSystem
    {
        public static LocalizationTypes Language { get; set; }

        public static string GetValueFromDictionary(string key)
        {
            CurrentDictionary.TryGetValue(key, out string value);
            return value;
        }

        public const string KEY_LOADING = "Loading...";
        public const string KEY_TRY_AGAIN = "TRY AGAIN!";
        public const string KEY_LEVEL_COMPLETE = "Level" + INDENT + "Complete";
        public const string KEY_LEVEL = "Level ";
        public const string KEY_FREE = "Free";
        public const string KEY_LEVEL_INDENT = "Level" + INDENT;

        private const string INDENT = "\n";

        public static Dictionary<string, string> CurrentDictionary
        {
            get
            {
                Dictionary<string, string> dictionary = new();
                switch (Language)
                {
                    case LocalizationTypes.English:
                        dictionary = new()
                    {
                        { KEY_LOADING, KEY_LOADING},
                        { KEY_TRY_AGAIN, KEY_TRY_AGAIN},
                        { KEY_LEVEL_COMPLETE, KEY_LEVEL_COMPLETE},
                        { KEY_LEVEL, KEY_LEVEL},
                        { KEY_FREE, KEY_FREE},
                        { KEY_LEVEL_INDENT, KEY_LEVEL_INDENT},
                    };
                        break;
                    case LocalizationTypes.Russian:
                        dictionary = new()
                    {
                        { KEY_LOADING, "Загрузка..."},
                        { KEY_TRY_AGAIN, "ПОПРОБУЙ СНОВА!"},
                        { KEY_LEVEL_COMPLETE, "Уровень" + INDENT + "Завершен"},
                        { KEY_LEVEL, "Уровень "},
                        { KEY_FREE, "Бесплатно"},
                        { KEY_LEVEL_INDENT, "Уровень" + INDENT},
                    };
                        break;
                }
                return dictionary;
            }
        }
    }
}
