using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEditor;
using TMPro;
[RequireComponent(typeof(Button))]

public class opciones2 : MonoBehaviour
{
    private TextMeshProUGUI texto=null;
    private Button answerBox1 = null;
    
    public opciones opcion { get; set; }
    private void Awake()
    {
        answerBox1 = GetComponent<Button>();
        texto = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void menu(opciones opciones, Action<opciones2>llamar)
    {
        texto.text = opcion.text;
        opcion = opciones;
        answerBox1.enabled = true;
        answerBox1.onClick.AddListener(delegate
        { llamar(this); 
        });
    }
}
