using UnityEngine;
using UnityEngine.UI;

public class SquareGridDrawer : MonoBehaviour
{
    public RectTransform panel;
    public GameObject squarePrefab;
    public int numberOfSquares;
    public float squareSize = 100f;
    public float spacing = 20f;

    private void Start()
    {
        DrawSquareGrid();
    }

    private void DrawSquareGrid()
    {
        int numSquares = Mathf.Clamp(numberOfSquares, 1, 6);

        int numRows = Mathf.CeilToInt(numSquares / 3f);
        int numColumns = Mathf.CeilToInt(numSquares / (float)numRows);

        float totalWidth = numColumns * squareSize + (numColumns - 1) * spacing;
        float totalHeight = numRows * squareSize + (numRows - 1) * spacing;

        Vector2 panelSize = new Vector2(totalWidth, totalHeight);
        panel.sizeDelta = panelSize;

        float startX = -totalWidth / 2f + squareSize / 2f;
        float startY = totalHeight / 2f - squareSize / 2f;

        int squareCount = 0;

        for (int row = 0; row < numRows; row++)
        {
            for (int column = 0; column < numColumns; column++)
            {
                if (squareCount >= numSquares)
                    break;

                float x = startX + column * (squareSize + spacing);
                float y = startY - row * (squareSize + spacing);

                Vector3 position = new Vector3(x, y, 0f);
                CreateSquare(position);

                squareCount++;
            }
        }
    }

    private void CreateSquare(Vector3 position)
    {
        GameObject square = Instantiate(squarePrefab, panel);
        RectTransform squareRectTransform = square.GetComponent<RectTransform>();
        squareRectTransform.localPosition = position;
        squareRectTransform.sizeDelta = new Vector2(squareSize, squareSize);
    }
}
