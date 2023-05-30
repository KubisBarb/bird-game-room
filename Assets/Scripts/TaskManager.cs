using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public GameObject singleTaskPanelPrefab;

    public Canvas overlayCanvas;
    InventoryObject resourceInventory;

    RectTransform iconsPanel;
    public GameObject squarePrefab;
    public int numberOfSquares;
    public float iconSize = 100f;
    public float iconSpacing = 20f;

    private void Start()
    {
        resourceInventory = this.gameObject.GetComponent<InventoryManager>().resourceInventory;
    }

    public void DisplayResourceTask(ResourceTask resourceTask)
    {
        if (!resourceTask.isUnlocked)
        {
            Debug.Log("This task (" + resourceTask + ") has not been unlocked yet!!!");
        }
        else
        {
            GameObject singlePanel = Instantiate(singleTaskPanelPrefab, Vector3.zero, Quaternion.identity, overlayCanvas.transform);
            singlePanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            TaskUI panelDetails = singlePanel.GetComponent<TaskUI>();
            panelDetails.header.text = resourceTask.displayedName;
            panelDetails.description.text = resourceTask.taskDescriptionToPlayer;
            iconsPanel = panelDetails.iconPanel.GetComponent<RectTransform>();

            Sprite[] arrayOfIcons = new Sprite[resourceTask.RequiredItems.Count];
            string[] arrayOfAmounts = new string[resourceTask.RequiredItems.Count];

            for (int i = 0; i < resourceTask.RequiredItems.Count; i++)
            {
                arrayOfIcons[i] = resourceTask.RequiredItems[i].item.icon;
                arrayOfAmounts[i] = resourceInventory.GetMaterialAmount(resourceTask.RequiredItems[i].item).ToString() + "/" + resourceTask.RequiredItems[i].amount.ToString();
            }

            DrawSquareGrid(resourceTask.RequiredItems.Count, arrayOfIcons, arrayOfAmounts);
        }
    }

    private void DrawSquareGrid(int numberOfIcons, Sprite[] iconsToDraw, string[] amountsToWrite)
    {
        int numSquares = Mathf.Clamp(numberOfIcons, 1, 6);

        int numRows = Mathf.CeilToInt(numSquares / 3f);
        int numColumns = Mathf.CeilToInt(numSquares / (float)numRows);

        float totalWidth = numColumns * iconSize + (numColumns - 1) * iconSpacing;
        float totalHeight = numRows * iconSize + (numRows - 1) * iconSpacing;

        Vector2 panelSize = new Vector2(totalWidth, totalHeight);
        iconsPanel.sizeDelta = panelSize;

        float startX = -totalWidth / 2f + iconSize / 2f;
        float startY = totalHeight / 2f - iconSize / 2f;

        int squareCount = 0;

        for (int row = 0; row < numRows; row++)
        {
            for (int column = 0; column < numColumns; column++)
            {
                if (squareCount >= numSquares)
                    break;

                float x = startX + column * (iconSize + iconSpacing);
                float y = startY - row * (iconSize + iconSpacing);

                Vector3 position = new Vector3(x, y, 0f);
                CreateSquare(position, iconsToDraw[squareCount], amountsToWrite[squareCount]);

                squareCount++;
            }
        }
    }

    private void CreateSquare(Vector3 position, Sprite icon, string amount)
    {
        GameObject iconPlaceholder = Instantiate(squarePrefab, iconsPanel);
        RectTransform squareRectTransform = iconPlaceholder.GetComponent<RectTransform>();
        squareRectTransform.localPosition = position;
        squareRectTransform.sizeDelta = new Vector2(iconSize, iconSize);
        iconPlaceholder.GetComponent<Image>().sprite = icon;
        iconPlaceholder.GetComponentInChildren<TextMeshProUGUI>().text = amount;
    }
}
