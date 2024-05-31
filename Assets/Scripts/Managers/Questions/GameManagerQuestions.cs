using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerQuestions : MonoBehaviour
{
    public static GameManagerQuestions instance { get; private set; }
    public int TotalHints;
    public int CurHints;
    public AudioClip GameOverSound;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject pauseMenu;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SetMaxHint(int hintTop)
    {
        TotalHints = hintTop;
    }
    public void UseHint(int addHint)
    {
        CurHints -= Mathf.Clamp(addHint, 0, 5);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void LoseGame()
    {
        if (GameOverSound != null)
        {
            SoundFXManager.instance.PlaySoundEffectClip(GameOverSound, transform, 1f);
        }
        Time.timeScale = 0;
        loseMenu.SetActive(true);
    }
    public void WinGame()
    {
        Time.timeScale = 0;
        winMenu.SetActive(true);
    }
}
