using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class scripptUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ntexto;
    [SerializeField] private List<opciones2> answerbox;
    public void menu(Preguntas p ,Action<opciones2> llamar )
    {
        ntexto.text = p.ptext;
        for (int i = 0; i < answerbox.Count; i++)
        {
            answerbox[i].menu(p.opciones[i], llamar);
        }
    }
}
