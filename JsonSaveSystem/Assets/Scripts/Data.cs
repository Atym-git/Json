using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using Newtonsoft.Json;

public class Data
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Data(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
    public class DataBase
    {
        public List<Data> DataInstances { get; private set; } = new List<Data>();
    }

public static class CSVUtils
{
    private const string _resourcesFolderName = "Resources";

    private const string _csvFileName = "CSVInfo.csv";

    private const string _jsonFileName = "SoundData.txt";

    public static void ReadCSV()
    {
        DataBase dataBase = new DataBase();

        string csvFilePath = Path.Combine(Application.dataPath, _resourcesFolderName,
            _csvFileName);
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError($"No .csv file at {csvFilePath}");
            return;
        }

        string[] lines = File.ReadAllLines(csvFilePath);
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(';');

            string name = values[0].Trim();
            string description = values[1].Trim();
            Data data = new(name, description);
            dataBase.DataInstances.Add(data);
        }

        string json = JsonConvert.SerializeObject(dataBase);
        string jsonFilePath = Path.Combine(Application.dataPath, _resourcesFolderName,
            _jsonFileName);
        File.WriteAllText(jsonFilePath, json);
    }
    public static bool TryReadVolume(float volume)
    {
        DataBase dataBase = new DataBase();

        string jsonFilePath = Path.Combine(Application.dataPath, _resourcesFolderName,
            _jsonFileName);
        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError($"No json file at {jsonFilePath}");
            return false;
        }
        //string[] lines = File.ReadAllLines(jsonFilePath);
        //for (int i = 0; i < lines.Length; i++)
        //{
        //    string[] values = lines[i].Split(';');

        //    string name = values[0].Trim();
        //    string description = values[1].Trim();
        //    Data data = new(name, description);
        //    dataBase.DataInstances.Add(data);
        //}
        string json = JsonConvert.SerializeObject(dataBase);
        File.WriteAllText(jsonFilePath, json);

        return true;
    }
    public static float GetVolume()
    {
        string jsonFilePath = Path.Combine(Application.dataPath, _resourcesFolderName,
    _jsonFileName);
        File.ReadAllText(jsonFilePath);
        DataBase dataBase = JsonConvert.DeserializeObject<DataBase>(jsonFilePath);
        return float.Parse(dataBase.DataInstances[0].Description);
    }
    //[MenuItem("Json Tools/Read and debug JSON")]
    public static bool TryReadJson(out DataBase dataBase)
    {
        dataBase = new();
        string jsonFilePath = Path.Combine(Application.dataPath, _resourcesFolderName,
            _jsonFileName);

        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError($"No item file at {_jsonFileName}");
            return false;
        }
        string json = File.ReadAllText(jsonFilePath);
        dataBase = JsonConvert.DeserializeObject<DataBase>(json);

        foreach (Data data in dataBase.DataInstances)
        {
            Debug.Log($"{data.Name}: {data.Description}");
        }

        return true;
    }
}