using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DetailsPopupUI : MonoBehaviour
{
    public Button addLocationButton;
    public Location myLocation;

    public GameObject gameManager; // Reference to the GameManager script

    private void Start()
    {
        addLocationButton.onClick.AddListener(OnClickButton);
        gameManager = GameObject.Find("GameManager");
    }

    private void OnClickButton()
    {
        FlightManager flightManager = gameManager.GetComponent<FlightManager>();
        if (flightManager != null)
        {
            flightManager.AddDestination(myLocation);
        }

        Destroy(this.gameObject);
    }

    private void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonUp(0))
        {
            // Check if the clicked position is outside the panel
            if (!RectTransformUtility.RectangleContainsScreenPoint(this.gameObject.GetComponent<RectTransform>(), Input.mousePosition))
            {
                // Hide the panel
                Destroy(this.gameObject);
            }
        }
    }
}
