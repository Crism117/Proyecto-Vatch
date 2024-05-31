using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckAnswer : MonoBehaviour
{
    [SerializeField] Button _sceneChangeButton;
    [SerializeField] private AudioClip buttonClick;

    private void Start()
    {
        _sceneChangeButton.onClick.AddListener(CompareAnswer);
    }
    // Update is called once per frame
    public void CompareAnswer()
    {
        TextMeshProUGUI tmp = GetComponentInChildren<TextMeshProUGUI>();

        // Call the CompareAnswer method on the singleton instance of TriviaLog
        if (TriviaLog.instance != null)
        {
            TriviaLog.instance.CompareAnswer(tmp, TriviaLog.instance.currentQuestion);
        }
        else
        {
            Debug.LogError("TriviaLog instance is not set.");
        }
    }
}
