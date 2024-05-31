using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SendToSceneButton : MonoBehaviour
{
    [SerializeField] Button _sceneChangeButton;
    [SerializeField] private AudioClip buttonClick;
    public ScenesManager.Scene goToScene;

    private void Start()
    {
        _sceneChangeButton.onClick.AddListener(ChangeScene);
    }
    // Update is called once per frame
    public void ChangeScene()
    {
        ScenesManager.instance.LoadScene(goToScene);
        if (buttonClick != null)
        {
            SoundFXManager.instance.PlaySoundEffectClip(buttonClick, transform, 1f);
        }
    }
}
