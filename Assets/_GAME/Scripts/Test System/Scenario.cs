using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scenario", menuName = "Scriptables/Scenario")]
public class Scenario : ScriptableObject
{
    public List<Task> Tasks = new List<Task>();
}
