using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] UIOverlays;
    public bool clearUIOnStart;

    public GameObject mapBirdPortraitHolder;
    public GameObject[] scheduleSlotHolders;
    public Sprite lockIcon;

    FlightManager flightManager;

    private void Start()
    {
        foreach (GameObject overlay in UIOverlays)
        {
            overlay.SetActive(false);
        }

        if (clearUIOnStart)
        {
            UIOverlays[0].SetActive(true);
        }

        flightManager = this.gameObject.GetComponent<FlightManager>();
    }

    public void HideObject(GameObject targetObject)
    {
        targetObject.SetActive(false);

        if (targetObject.tag == "Map overlay")
        {
            for (int i = 0; i < 5; i++)    // For capacity times it shows the holders
            {
                scheduleSlotHolders[i].SetActive(false);
                scheduleSlotHolders[i].GetComponent<Image>().sprite = null;
            }
        }
    }

    public void ShowObject(GameObject targetObject)
    {
        targetObject.SetActive(true);

        if (targetObject.tag == "Map overlay")
        {
            // Update Ui image with sprite from active bird
            mapBirdPortraitHolder.GetComponent<Image>().sprite = this.gameObject.GetComponent<Player>().activeBird.portrait;

            // Create instances for schedule slots
            int capacity = this.gameObject.GetComponent<Player>().activeBird.level;

            for (int i = 0; i < (capacity + 1); i++)    // For capacity times it shows the holders
            {
                scheduleSlotHolders[i].SetActive(true);

                GameObject parentObject = scheduleSlotHolders[i];
                GameObject childObject = parentObject.transform.Find("Cross")?.gameObject;

                if (childObject != null)
                {
                    childObject.SetActive(false);
                }

                if (i == capacity)  // At last shows lock icon on the capacity + 1 slot
                {
                    scheduleSlotHolders[i].GetComponent<Image>().sprite = lockIcon;

                }
            }

            RedrawQueuePanelIcons();
            flightManager.UpdateQueueLengthUI();
        }
    }

    public void RedrawQueuePanelIcons(bool clearCompletely = false)
    {
        int queueSize = flightManager.destinationQueue.Count;

        for (int i = 0; i < queueSize; i++)
        {
            // Change each queue size icon to appropriate icon
            Location location = flightManager.destinationQueue[i];

            if (clearCompletely)
            {
                scheduleSlotHolders[i].GetComponent<Image>().sprite = null;

                // Clears crosses
                GameObject parentObject = scheduleSlotHolders[i];
                GameObject childObject = parentObject.transform.Find("Cross")?.gameObject;

                if (childObject != null)
                {
                    childObject.SetActive(false);
                }
            }
            else
            {
                scheduleSlotHolders[i].GetComponent<Image>().sprite = location.icon;

                // Adds the cross UI
                GameObject parentObject = scheduleSlotHolders[i];
                GameObject childObject = parentObject.transform.Find("Cross")?.gameObject;

                if (childObject != null)
                {
                    childObject.SetActive(true);
                }
            }
        }
    }
}
