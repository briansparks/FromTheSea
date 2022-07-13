using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public abstract class AbstractJsonFileRepository<T> : MonoBehaviour where T : class, new()
{
    protected T DeserialzeJsonFileAtPath(string filePath)
    {
        var fullFilePath = filePath.EndsWith(".json") ? Application.dataPath + '/' + filePath :
            Application.dataPath + '/' + filePath + ".json";

        if (!File.Exists(fullFilePath))
        {
            return null;
        }

        var data = File.ReadAllText(fullFilePath);
        var obj = JsonConvert.DeserializeObject<T>(data);
        return obj;
    }

    protected IEnumerable<T> DeserializeJsonFilesAtPath(string filePath)
    {
        var objects = new List<T>();
        var filePaths = Directory.GetFiles(Application.dataPath + '/' + filePath, "*.json")?.ToList();

        foreach (var path in filePaths)
        {
            var data = File.ReadAllText(path);
            var obj = JsonUtility.FromJson<T>(data);
            objects.Add(obj);
        }

        return objects;
    }
}
