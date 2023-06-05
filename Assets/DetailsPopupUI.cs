using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}
