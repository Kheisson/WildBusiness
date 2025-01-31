using System.IO;
using UnityEditor;
using UnityEngine;

namespace LlamaEditor
{
public class SaveFileManagerEditor : EditorWindow
{
    private string savePath;
    private string[] saveFiles;

    [MenuItem("Tools/Save File Manager")]
    public static void ShowWindow()
    {
        var window = GetWindow<SaveFileManagerEditor>("Save File Manager");
        window.minSize = new Vector2(400, 300);
        window.Show();
    }

    private void OnEnable()
    {
        savePath = Application.persistentDataPath;
        RefreshSaveFiles();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Save File Manager", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Save Path", savePath);
        
        if (GUILayout.Button("Refresh File List"))
        {
            RefreshSaveFiles();
        }
        
        EditorGUILayout.Space();

        if (saveFiles == null || saveFiles.Length == 0)
        {
            EditorGUILayout.LabelField("No .sav files found.");
        }
        else
        {
            EditorGUILayout.LabelField("Save Files:");
            
            foreach (var file in saveFiles)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(Path.GetFileName(file), GUILayout.ExpandWidth(true));

                if (GUILayout.Button("Delete", GUILayout.Width(80)))
                {
                    File.Delete(file);
                    Debug.Log($"Deleted save file: {file}");
                    RefreshSaveFiles();
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Delete All Saves"))
            {
                if (EditorUtility.DisplayDialog("Confirm Delete", 
                    "Are you sure you want to delete all .sav files?", "Delete All", "Cancel"))
                {
                    DeleteAllSaveFiles();
                    RefreshSaveFiles();
                }
            }
        }
    }

    private void RefreshSaveFiles()
    {
        if (!Directory.Exists(savePath))
        {
            saveFiles = new string[0];
            return;
        }

        saveFiles = Directory.GetFiles(savePath, "*.sav");
    }

    private void DeleteAllSaveFiles()
    {
        foreach (var file in saveFiles)
        {
            File.Delete(file);
        }

        Debug.Log("Deleted all save files.");
    }
}

}