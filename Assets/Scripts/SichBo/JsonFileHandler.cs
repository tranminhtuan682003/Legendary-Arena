// using System.Collections.Generic;
// using System.IO;
// using Newtonsoft.Json;
// using UnityEngine;

// public static class JsonFileHandler
// {
//     public static bool SaveToFile<T>(T data, string fileName)
//     {
//         try
//         {
//             string path = Path.Combine(Application.persistentDataPath, fileName);
//             string json = JsonConvert.SerializeObject(data, Formatting.Indented); // Lưu trực tiếp danh sách dưới dạng mảng JSON
//             File.WriteAllText(path, json);
//             return true;
//         }
//         catch (System.Exception ex)
//         {
//             Debug.LogError($"Failed to save data to {fileName}: {ex.Message}");
//             return false;
//         }
//     }

//     public static T LoadFromFile<T>(string fileName)
//     {
//         try
//         {
//             string path = Path.Combine(Application.persistentDataPath, fileName);
//             if (File.Exists(path))
//             {
//                 string json = File.ReadAllText(path);
//                 return JsonConvert.DeserializeObject<T>(json); // Tải trực tiếp danh sách từ mảng JSON
//             }
//             else
//             {
//                 Debug.LogWarning($"File {fileName} not found, returning default value");
//                 return default(T);
//             }
//         }
//         catch (System.Exception ex)
//         {
//             Debug.LogError($"Failed to load data from {fileName}: {ex.Message}");
//             return default(T);
//         }
//     }
// }
