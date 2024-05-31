using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintCounterQs : MonoBehaviour
{
    int MaxHints;
    [SerializeField] private TextMeshProUGUI Counter;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManagerScript.Instance!=null)
        {
            MaxHints = GameManagerScript.Instance.TotalHints; 
        }
        GameManagerQuestions.instance.SetMaxHint(MaxHints);
    }

    // Update is called once per frame
    void Update()
    {
        Counter.text = (GameManagerQuestions.instance.CurHints + "/" + GameManagerQuestions.instance.TotalHints);
    }
}
