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
        DrawCircle();

        // Update UI information about the queue length
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

        // Update UI information about the queue length
        UpdateQueueLengthUI();
    }

    // Update the UI information about the queue length
    private void UpdateQueueLengthUI()
    {
        if (queueLengthText != null)
        {
            queueLengthText.text = "Queue Length: " + destinationQueue.Count.ToString() + "/" + this.gameObject.GetComponent<Player>().activeBird.level;
        }
    }

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
}
