using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    public void HideObject(GameObject targetObject)
    {
        targetObject.SetActive(false);
    }

    public void ShowObject(GameObject targetObject)
    {
        targetObject.SetActive(true);
    }
}
