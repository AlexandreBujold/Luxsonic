using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public static class ScenarioSaveDataSerializer
{
    private static readonly string FilePath = Application.persistentDataPath + "/LuxsonicSaves";

    public static void SaveData(Dictionary<Task, TaskMetrics> metrics, Scenario scenario)
    {
        if (metrics == null)
        {
            Debug.LogError("Could not save! Metrics are null.");
            return;
        }

        //Convert Dictionary to List<TaskMetricPair>
        List<TaskMetricPair> taskMetricPairs = new List<TaskMetricPair>();
        foreach (KeyValuePair<Task, TaskMetrics> item in metrics)
        {
            taskMetricPairs.Add(new TaskMetricPair(item.Key, item.Value));
        }

        string scenarioName = scenario == null ? "Undefined Scenario" : scenario.ScenarioName;

        //Get file count
        DirectoryInfo dirInfo = Directory.CreateDirectory(FilePath);
        int fileNumber = dirInfo.GetFiles().Length;

        string saveName = string.Format("{0} {1}.json", scenarioName, fileNumber + 1);

        ScenarioSaveData saveData = new ScenarioSaveData(taskMetricPairs, scenarioName);

        string data = JsonUtility.ToJson(saveData, true);
        string completePath = FilePath + "/" + saveName;

        StreamWriter writer = new StreamWriter(completePath);
        writer.Write(data);
        writer.Close();

        Debug.Log(string.Format("Saved Data to: {0} \nPath:{1}", saveName, completePath));
    }

    public static ScenarioSaveData LoadSave(string fullPath)
    {
        StreamReader reader = new StreamReader(fullPath);

        string json = reader.ReadToEnd();

        return JsonUtility.FromJson<ScenarioSaveData>(json);
    }

    public static ScenarioSaveData LoadLatestSaveData()
    {
        //Get last file saved
        DirectoryInfo dirInfo = new DirectoryInfo(FilePath);
        var lastFile = dirInfo.GetFiles("*.json").OrderBy(file => file.LastWriteTime).Last();

        Debug.Log(string.Format("Loading Save: {0} \nPath: {1}", lastFile.Name, lastFile.FullName));

        return LoadSave(lastFile.FullName);
    }

    public static List<ScenarioSaveData> LoadMultipleSaveData(int count)
    {
        //Get count files ordered by latest
        DirectoryInfo dirInfo = new DirectoryInfo(FilePath);
        IEnumerable<FileInfo> files = dirInfo.GetFiles("*.json").OrderByDescending(file => file.LastWriteTime);
        count = Mathf.Clamp(count, 0, files.Count());
        List<FileInfo> listOfFiles = files.Take(3).ToList();

        List<ScenarioSaveData> saves = new List<ScenarioSaveData>();
        for (int i = 0; i < count; i++)
        {
            Debug.Log(string.Format("Loading Save: {0} \nPath: {1}", listOfFiles[i].Name, listOfFiles[i].FullName));
            saves.Add(LoadSave(listOfFiles[i].FullName));
        }
        
        return saves;
    }
}
