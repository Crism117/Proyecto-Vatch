using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{

    [SerializeField] Button _SelectionButton;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] LevelSelection.Level levelSelected;

    private void Start()
    {
        _SelectionButton.onClick.AddListener(SwitchSelection);
    }
    // Update is called once per frame
    public void SwitchSelection()
    {
        LevelSelection.instance.SetTextTo(levelSelected);
        if (buttonClick != null &&SoundFXManager.instance!=null)
        {
            SoundFXManager.instance.PlaySoundEffectClip(buttonClick, transform, 1f);
        }
    }
}
