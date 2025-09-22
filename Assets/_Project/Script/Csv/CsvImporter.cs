using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class GenericCsvImporter : AssetPostprocessor
{
    static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        foreach (string path in importedAssets)
        {
            if (Path.GetExtension(path) != ".csv") continue;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) continue;

            string[] headers = lines[0].Split(',');

            // Tạo một ScriptableObject mới dựa trên tên file CSV
            string className = CsvFileNameToClassName(path);
            Type soType = GetTypeByName(className);
            if (soType == null || !typeof(ScriptableObject).IsAssignableFrom(soType))
            {
                Debug.LogWarning($"No ScriptableObject class found for {className}");
                continue;
            }

            ScriptableObject so = ScriptableObject.CreateInstance(soType);

            // Tìm field kiểu List<T> trong ScriptableObject
            FieldInfo listField = null;
            foreach (var field in soType.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (field.FieldType.IsGenericType &&
                    field.FieldType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.List<>))
                {
                    listField = field;
                    break;
                }
            }
            if (listField == null)
            {
                Debug.LogWarning($"No List<> field found in {className}");
                continue;
            }

            Type elementType = listField.FieldType.GetGenericArguments()[0];
            var listInstance = Activator.CreateInstance(typeof(System.Collections.Generic.List<>).MakeGenericType(elementType));

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                string[] values = lines[i].Split(',');

                var element = Activator.CreateInstance(elementType);
                for (int j = 0; j < headers.Length && j < values.Length; j++)
                {
                    var prop = elementType.GetField(headers[j], BindingFlags.Public | BindingFlags.Instance);
                    if (prop != null)
                    {
                        object convertedValue = Convert.ChangeType(values[j], prop.FieldType);
                        prop.SetValue(element, convertedValue);
                    }
                }
                listField.FieldType.GetMethod("Add").Invoke(listInstance, new[] { element });
            }

            listField.SetValue(so, listInstance);

            string assetPath = Path.ChangeExtension(path, ".asset");
            string assetName = Path.GetFileNameWithoutExtension(assetPath);
            so.name = assetName;

            ScriptableObject existing = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            if (existing == null)
            {
                AssetDatabase.CreateAsset(so, assetPath);
            }
            else
            {
                EditorUtility.CopySerialized(so, existing);
            }
            AssetDatabase.SaveAssets();
        }
    }

    static Type GetTypeByName(string className)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var type = assembly.GetType(className);
            if (type != null)
                return type;
        }
        return null;
    }

    static string CsvFileNameToClassName(string filePath)
    {
        // Chuyển "enemy_config" thành "EnemyConfig"
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        var parts = fileName.Split('_');
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i].Length > 0)
                parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1);
        }
        return string.Join("", parts);
    }
}