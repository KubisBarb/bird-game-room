using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task Object", menuName = "Task System/Task/Resource Task")]
public class ResourceTask : Task
{
    public List<TaskSlot> RequiredItems = new List<TaskSlot>();
    
    private void Awake()
    {
        type = TaskType.Resource;
    }
}

[System.Serializable]
public class TaskSlot
{
    //public string name;
    public ResourceObject item;
    public int amount;
}