using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task Object", menuName = "Task System/Task/Default")]
public class DefaultTask : TaskObject
{
    public bool isCompleted;
    public string displayedName;
    [TextArea(15, 20)]
    public string taskDescriptionToPlayer;

    public ItemObject[] requiredTasks = new ItemObject[0];
    public ItemObject[] followingTasks = new ItemObject[0];

    public Dictionary<string, int> requiredMaterials = new Dictionary<string, int>();


    private void Awake()
    {
        type = TaskType.Default;
    }
}
