using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    Default,
    Resource
}

public abstract class TaskObject : ScriptableObject
{
    public GameObject prefab;
    public TaskType type;
    [TextArea(5, 20)]
    public string description;
}
