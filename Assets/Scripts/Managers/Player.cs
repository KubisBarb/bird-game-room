using System.Collections.Generic;
using UnityEngine;

public enum FlightStatus
{
    Waiting,
    BirdOutside,
    BirdReturned
}

public class Player : MonoBehaviour
{
    public int playerLevel;
    public BirdObject activeBird;

    public List<BirdObject> theNest = new List<BirdObject>();

    public FlightStatus flightStatus;

    public GameObject activeBirdOverlay;

    private void Start()
    {
        if (activeBird == null)
        {
            activeBird = theNest[0];

            activeBirdOverlay.GetComponent<ActiveBirdUI>().birdPortrait.sprite = activeBird.portrait;
            activeBirdOverlay.GetComponent<ActiveBirdUI>().nameText.text = activeBird.name;
        }
    }


    public void EquipBird(BirdObject bird)
    {
        if (theNest.Contains(bird))
        {
            activeBird = bird;
            Debug.Log("Equipped bird: " + bird.name);
        }
        else
        {
            Debug.Log("Player doesn't have this bird in their nest!");
        }
    }

    public void AddBirdToNest(BirdObject bird)  // Also takes care of the duplicates
    {
        if (theNest.Contains(bird))
        {
            int birdIndex = theNest.IndexOf(bird);
            if (theNest[birdIndex].level < 5)
            {
                theNest[birdIndex].level++;
                Debug.Log("Increased level of bird: " + bird.name + " to " + theNest[birdIndex].level);
            }
            else
            {
                Debug.Log("Bird: " + bird.name + " is already at the maximum level!");
            }
        }
        else
        {
            theNest.Add(bird);
            Debug.Log("Added bird to the nest: " + bird.name);
        }
    }

    public void SetActiveBirdByArrow(bool rightArrow)
    {
        if (theNest.Count == 0)
        {
            Debug.Log("No birds in the nest!");
            return;
        }

        if (rightArrow)
        {
            int currentBirdIndex = theNest.IndexOf(activeBird);
            int nextBirdIndex = (currentBirdIndex + 1) % theNest.Count;
            activeBird = theNest[nextBirdIndex];
        }
        else
        {
            int currentBirdIndex = theNest.IndexOf(activeBird);
            int previousBirdIndex = (currentBirdIndex - 1 + theNest.Count) % theNest.Count;
            activeBird = theNest[previousBirdIndex];
        }

        activeBirdOverlay.GetComponent<ActiveBirdUI>().birdPortrait.sprite = activeBird.portrait;
        activeBirdOverlay.GetComponent<ActiveBirdUI>().nameText.text = activeBird.name;

        Debug.Log("Active bird changed to: " + activeBird.name);
    }
}
