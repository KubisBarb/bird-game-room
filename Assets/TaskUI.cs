using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskUI : MonoBehaviour
{
    public TextMeshProUGUI header;
    public TextMeshProUGUI description;
    public GameObject iconPanel;
    public GameObject giveButton;
    public ResourceTask resourceTaskDisplaying;

    TaskManager taskManager;

    private void Start()
    {
        taskManager = GameObject.Find("GameManager").GetComponent<TaskManager>();
    }

    public void GiveMaterials()
    {
        taskManager.SendMessage("CompleteTask", this.gameObject);
    }

    public void ClosePanel()
    {
        Destroy(this.gameObject);
    }
}
