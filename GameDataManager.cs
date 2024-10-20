using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace WinFormsApp1
{
    public class GameDataManager
    {
        private readonly string dataFilePath = "./data.json";
        public List<GameData> GameDataList { get; private set; } = new List<GameData>();

        public void LoadGameData()
        {
            if (!File.Exists(dataFilePath))
                return;

            using StreamReader reader = new(dataFilePath);
            var json = reader.ReadToEnd();
            var jobject = JObject.Parse(json);

            GameDataList.Clear(); // Clear the list before loading new data

            if (jobject["data"] != null)
            {
                foreach (var item in jobject["data"])
                {
                    GameData? data = item.ToObject<GameData>();
                    if (data != null)
                    {
                        GameDataList.Add(data);
                    }
                }
            }
        }

        public void SaveGameData()
        {
            var json = JsonConvert.SerializeObject(new { data = GameDataList }, Formatting.Indented);
            File.WriteAllText(dataFilePath, json);
        }

        public void AddNewGame(GameData newGame)
        {
            GameDataList.Add(newGame);
            SaveGameData();
        }

        public void RemoveGame(GameData game)
        {
            GameDataList.Remove(game);
            SaveGameData();
        }
    }
}
