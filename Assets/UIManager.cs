using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] UIOverlays;
    public bool clearUIOnStart;

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
    }

    public void HideObject(GameObject targetObject)
    {
        targetObject.SetActive(false);
    }

    public void ShowObject(GameObject targetObject)
    {
        targetObject.SetActive(true);
    }
}
