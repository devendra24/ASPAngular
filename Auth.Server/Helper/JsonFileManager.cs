using System;
using System.IO;
using System.Text.Json;

public class JsonFileManager
{
    private readonly string filePath;
    private readonly object lockObject = new object();

    public JsonFileManager(string filePath)
    {
        this.filePath = filePath;
    }

    public T ReadFromFile<T>()
    {
        lock (lockObject)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(json);
            }
        }

        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
            lock (lockObject)
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }
            }
        }

        T emptyData = Activator.CreateInstance<T>();
        WriteToFile(emptyData); // Create the file with empty data
        return emptyData;
    }

    public void WriteToFile<T>(T data)
    {
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        lock (lockObject)
        {
            File.WriteAllText(filePath, json);
        }
    }
}
