using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[SerializeField] class LevelSelection : MonoBehaviour
{
    public static LevelSelection instance;
    private void Awake()
    {
        if(instance == null)
        instance = this;
    }

    [Header("Text to change")]
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI body;
    [Header("No level selected")]
    [SerializeField] string noSelectionTitle;
    [SerializeField] string noSelectionHeader;
    [SerializeField] string noSelectionBody;
    [Header("heart")]
    [SerializeField] string heartTitle;
    [SerializeField] string heartHeader;
    [SerializeField] string heartBody;
    [Header("stomach")]
    [SerializeField] string stomachTitle;
    [SerializeField] string stomachHeader;
    [SerializeField] string stomachBody;
    [Header("pancreas")]
    [SerializeField] string pancreasTitle;
    [SerializeField] string pancreasHeader;
    [SerializeField] string pancreasBody;
    [Header("kidney")]
    [SerializeField] string kidneyTitle;
    [SerializeField] string kidneyHeader;
    [SerializeField] string kidneyBody;
    public Level selectedLevel;
    public enum Level
    {   
        NoSeleccion,
        Heart,
        Stomach,
        Pancreas,
        Kidney
    }

   
    // Start is called before the first frame update
    void Start()
    {
        SetTextTo(Level.NoSeleccion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetTextTo(Level levelReference) 
    {
        selectedLevel = levelReference;
        switch (levelReference) 
        {
            case Level.NoSeleccion:
                title.text = noSelectionTitle;
                header.text = noSelectionHeader;
                body.text = noSelectionBody;
                break;
            case Level.Heart:
                title.text = heartTitle;
                header.text = heartHeader;
                body.text = heartBody;
                break;
            case Level.Stomach:
                title.text = stomachTitle;
                header.text = stomachHeader;
                body.text = stomachBody;
                break;
            case Level.Pancreas:
                title.text = pancreasTitle;
                header.text = pancreasHeader;
                body.text = pancreasBody;
                break;
            case Level.Kidney:
                title.text = kidneyTitle;
                header.text = kidneyHeader;
                body.text = kidneyBody;
                break;
            default:
            break;
        }
    }
}
