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
    public Sprite emptySlotSprite;
    public GameObject collectLootButton;

    FlightManager flightManager;
    [HideInInspector] public Player player;
    [HideInInspector] public Button buttonToMapOverlay;

    private void Start()
    {
        flightManager = this.gameObject.GetComponent<FlightManager>();
        player = this.gameObject.GetComponent<Player>();

        foreach (GameObject overlay in UIOverlays)
        {
            overlay.SetActive(false);
        }

        if (clearUIOnStart)
        {
            UIOverlays[0].SetActive(true);
        }

        UpdateCollectLootButton();
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

        if (targetObject.name == "RoomOverlay")
        {
            UpdateCollectLootButton();
        }

        if (targetObject.name == "BirdSelectionOverlay" && player.flightStatus != FlightStatus.Waiting)
        {
            buttonToMapOverlay.onClick.Invoke();
        }
    }

    public void RedrawQueuePanelIcons(bool clearCompletely = false)
    {
        int queueSize = flightManager.destinationQueue.Count;
        int maxQueueSize = this.gameObject.GetComponent<Player>().activeBird.level;

        for (int i = 0; i < maxQueueSize; i++)
        {
            if (i < queueSize) // Draws slots with scheduled locations
            {
                // Change each queue size icon to appropriate icon
                Location location = flightManager.destinationQueue[i];

                scheduleSlotHolders[i].GetComponent<Image>().sprite = location.icon;

                if (player.flightStatus == FlightStatus.Waiting)
                {
                    // Adds the cross UI
                    GameObject parentObject = scheduleSlotHolders[i];
                    GameObject childObject = parentObject.transform.Find("Cross")?.gameObject;

                    if (childObject != null)
                    {
                        childObject.SetActive(true);
                    }
                }
                else
                {
                    // Removes the cross UI
                    GameObject parentObject = scheduleSlotHolders[i];
                    GameObject childObject = parentObject.transform.Find("Cross")?.gameObject;

                    if (childObject != null)
                    {
                        childObject.SetActive(false);
                    }
                }
            }
            else // Draws clear slots for unscheduled locations
            {
                scheduleSlotHolders[i].GetComponent<Image>().sprite = emptySlotSprite;

                // Clears crosses
                GameObject parentObject = scheduleSlotHolders[i];
                GameObject childObject = parentObject.transform.Find("Cross")?.gameObject;

                if (childObject != null)
                {
                    childObject.SetActive(false);
                }
            }
        }
    }

    public void UpdateCollectLootButton()
    {
        if (player.flightStatus == FlightStatus.BirdReturned)
        {
            collectLootButton.SetActive(true);
        }
        else
        {
            collectLootButton.SetActive(false);
        }
    }
}
