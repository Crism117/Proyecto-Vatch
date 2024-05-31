using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSelectedLevel : MonoBehaviour
{
    [SerializeField] Button _Button;
    [SerializeField] private AudioClip buttonClick;
    LevelSelection.Level levelSelected;

    private void Start()
    {
        _Button.onClick.AddListener(StartLevel);
    }
    // Update is called once per frame
    public void StartLevel()
    {
        levelSelected = LevelSelection.instance.selectedLevel;
        switch (levelSelected) 
        {
            case LevelSelection.Level.NoSeleccion: 
                break;
            case LevelSelection.Level.Heart:
                ScenesManager.instance.LoadScene(ScenesManager.Scene.Corazon_Exploracion);
                break;
            case LevelSelection.Level.Stomach:
                ScenesManager.instance.LoadScene(ScenesManager.Scene.Estomago_Exploracion);
                break;
            case LevelSelection.Level.Pancreas:
                ScenesManager.instance.LoadScene(ScenesManager.Scene.Pancreas_Exploracion);
                break;
            case LevelSelection.Level.Kidney:
                ScenesManager.instance.LoadScene(ScenesManager.Scene.Riñon_Exploracion);
                break;
        }
        if (buttonClick != null && SoundFXManager.instance != null)
        {
            SoundFXManager.instance.PlaySoundEffectClip(buttonClick, transform, 1f);
        }
    }
}
