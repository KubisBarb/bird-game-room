using UnityEngine;

[CreateAssetMenu(fileName = "New Bird", menuName = "Birds")]
public class BirdObject : ScriptableObject
{
    public bool isUnlocked;
    public int level;
    public Location[] specialLocations;

    [Tooltip("Represents maximum number of items a bird can bring from a location, even if it produces more.")]
    [Range(0, 25)]
    public int capacity;

    [Tooltip("When deciding what materials does a bird bring back from a location, it sorts them from most rare to least rare. It transfers only the first i (intelligence) items to the actual loot and rest of the capacity is filled randomly.")]
    [Range(0, 10)]
    public int intelligence;

    [Tooltip("Modifier representing decrease in search duration. Final duration is 1/speed of original duration.")]
    [Range(1, 2)]
    public float speed;

    private void Awake()
    {
        if (level == 0)
        {
            isUnlocked = false;
        }
        else
        {
            isUnlocked = true;
        }
    }
}