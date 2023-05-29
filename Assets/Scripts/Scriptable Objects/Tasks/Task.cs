using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : TaskObject
{
    public bool isUnlocked;
    public bool isCompleted;
    public string displayedName;
    [TextArea(5, 20)]
    public string taskDescriptionToPlayer;

    public ItemObject[] requiredTasks = new ItemObject[0];
    public ItemObject[] followingTasks = new ItemObject[0];
}