using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task Object", menuName = "Task System/Task/Resource Task")]
public class ResourceTask : Task
{
    public List<TaskSlot> RequiredItems = new List<TaskSlot>();
    [HideInInspector] public InventoryObject requiredItemsInventory;

    private void Awake()
    {
        type = TaskType.Resource;
    }

    public void Initialize()
    {
        requiredItemsInventory = ScriptableObject.CreateInstance<InventoryObject>();

        foreach (TaskSlot taskSlot in RequiredItems)
        {
            InventorySlot newSlot = new InventorySlot(taskSlot.item, taskSlot.amount);
            requiredItemsInventory.Container.Add(newSlot);
        }
    }
}

[System.Serializable]
public class TaskSlot
{
    //public string name;
    public ResourceObject item;
    public int amount;
}