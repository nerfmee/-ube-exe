using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    public bool clickedIs = false;
    void OnMouseDown()
    {
        clickedIs = true;
    }
    void OnMouseUp()
    {
        clickedIs = false;
    }
}
