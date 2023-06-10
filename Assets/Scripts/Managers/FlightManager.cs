using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlightManager : MonoBehaviour
{
    public int maxQueueSize = 0;
    public List<Location> destinationQueue = new List<Location>();
    public TextMeshProUGUI queueLengthText;
    public GameObject circlePrefab;
    List<GameObject> circleInstances = new List<GameObject>();
    public GameObject detailsPopupPlaceholder;
    GameObject detailsPanelInstance;
    UIManager uIManager;
    TimeManager timeManager;
    Player player;

    private void Start()
    {
        uIManager = this.gameObject.GetComponent<UIManager>();
        timeManager = this.gameObject.GetComponent<TimeManager>();
        player = this.gameObject.GetComponent<Player>();
    }

    // Function to add a destination to the queue
    public void AddDestination(Location destination)
    {
        BirdObject bird = this.gameObject.GetComponent<Player>().activeBird;

        // Check if the queue is full
        if (destinationQueue.Count >= bird.level)
        {
            Debug.Log("Destination queue is full!");
            return;
        }

        // Add the destination to the queue
        destinationQueue.Add(destination);
        Debug.Log("Added destination: " + destination);

        // Draw UI indicator of queue
        // DrawCircle();    // Bug: the circle now gets drawn over circle in the ui and not over map
        // Instead we redraw the queue panel after each add/remove
        uIManager.RedrawQueuePanelIcons();

        // Adds waiting time to time manager
        timeManager.timerDurationMinutes += destination.searchDurationMinutes;

        // Update UI information about the queue length
        UpdateQueueLengthUI();
    }

    public void RemoveDestination(int index)
    {
        if (destinationQueue.Count == 1)
        {
            uIManager.RedrawQueuePanelIcons(true);
        }

        var destination = destinationQueue[index];

        destinationQueue.RemoveAt(index); // Index is assigned to each button specifically in the editor based on the slot index
        Debug.Log("Removed destination: " + destination + " at index " + index);
        Debug.Log("queue size = " + destinationQueue.Count);

        uIManager.RedrawQueuePanelIcons();

        if(destination.searchDurationMinutes > timeManager.timerDurationMinutes)
        {
            timeManager.timerDurationMinutes = 0f;
        }
        else
        {
            timeManager.timerDurationMinutes -= destination.searchDurationMinutes;
        }

        UpdateQueueLengthUI();
    }

    // Function to reset the destination queue
    public void ResetQueue()
    {
        destinationQueue.Clear();
        Debug.Log("Destination queue has been reset!");

        // Clear instantiated prefabs or update UI elements to reflect the empty queue
        foreach (GameObject circle in circleInstances)
        {
            Destroy(circle);
        }

        uIManager.RedrawQueuePanelIcons(true);

        timeManager.timerDurationMinutes = 0f;

        // Update UI information about the queue length
        UpdateQueueLengthUI();
    }

    // Update the UI information about the queue length
    public void UpdateQueueLengthUI()
    {
        if (queueLengthText != null)
        {
            int minutes = Mathf.FloorToInt(timeManager.timerDurationMinutes);
            int seconds = Mathf.FloorToInt((timeManager.timerDurationMinutes - minutes) * 60f);

            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            queueLengthText.text = destinationQueue.Count.ToString() + "/" + this.gameObject.GetComponent<Player>().activeBird.level + " stops scheduled \nFlight time: " + formattedTime;
        }
    }

    /*
    public void DrawCircle()
    {
        // Instantiate a prefab or update UI elements to reflect the new destination
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (button != null)
        {
            Vector3 buttonPosition = button.transform.position;
            GameObject newCircle = Instantiate(circlePrefab, buttonPosition, Quaternion.identity);
            circleInstances.Add(newCircle);

            // Parent it to UI
            newCircle.transform.SetParent(button.transform.parent);

            // Change the index of the prefab circle
            newCircle.GetComponentInChildren<TextMeshProUGUI>().text = destinationQueue.Count.ToString();
        }
    }
    */

    public void ShowLocationPanel(Location location)
    {
        if (!detailsPanelInstance)
        {
            detailsPanelInstance = Instantiate(location.panelPrefab, detailsPopupPlaceholder.transform);
        }

        // Show or hide the button based on flight status
        if (player.flightStatus == FlightStatus.Waiting)
        {
            detailsPanelInstance.GetComponent<DetailsPopupUI>().addLocationButton.gameObject.SetActive(true);

            // Disable or enable based queue capacity
            if (destinationQueue.Count == this.gameObject.GetComponent<Player>().activeBird.level)
            {
                detailsPanelInstance.GetComponent<DetailsPopupUI>().addLocationButton.interactable = false;
            }
            else
            {
                detailsPanelInstance.GetComponent<DetailsPopupUI>().addLocationButton.interactable = true;
            }
        }
        else
        {
            detailsPanelInstance.GetComponent<DetailsPopupUI>().addLocationButton.gameObject.SetActive(false);
        }
    }
    
}
