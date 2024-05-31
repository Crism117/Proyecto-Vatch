using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintCounter : MonoBehaviour
{
    public GameObject HintMagnet;
    public TextMeshProUGUI hintCount;
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        hintCount.text = GameManagerScript.Instance.TotalHints.ToString() + "/" + GameManagerScript.Instance.maxHints.ToString();
    }
}
