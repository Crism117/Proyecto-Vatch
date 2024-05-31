using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaThreshhold : MonoBehaviour
{
    public Image[] theButtons;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < theButtons.Length; i++)
        {
            theButtons[i].alphaHitTestMinimumThreshold = 1f;
        }
    }
}
