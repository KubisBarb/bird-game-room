using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    Default
}

public abstract class TaskObject : ScriptableObject
{
    public GameObject prefab;
    public TaskType type;
    [TextArea(15, 20)]
    public string description;
}
