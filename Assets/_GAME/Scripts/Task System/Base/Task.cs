using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public string TaskName = "Task A";
    [TextArea(5, 10)]
    public string Description = "Do X to complete.";
}
