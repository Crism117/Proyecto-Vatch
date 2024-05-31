using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }
    #region Hints
    public int TotalHints { get; private set; }
    [HideInInspector]
    public int maxHints = 5;
    #endregion
    #region Detect Game Over
    private bool areDamagePointsHealed;
    private bool isEnemyAlive;
    private bool isPatientAlive;
    private bool winGame;
    private bool loseGame;
    private bool gameOver;
    [HideInInspector]
    public GameObject patient;
    [HideInInspector]
    public Health patientHealth;
    private bool wasPatientAlive = true; // Track the previous state of isPatientAlive
    public GameObject winMenu;
    public GameObject loseMenu;
    [SerializeField] private AudioClip GameOverSound;
    #endregion
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Cuidado! Mas de un GameManager en escena.");
        }
    }
    private void Start()
    {
        patient = GameObject.FindGameObjectWithTag("Patient");
        patientHealth = patient.GetComponent<Health>();
        winGame = false;
        loseGame = false;
        gameOver = false;
    }
    // Update is called once per frame
    void Update()
    {
        if ( !gameOver)
        {
            areDamagePointsHealed = GameObject.FindGameObjectsWithTag("Damage Spot").Length == 0;
            isPatientAlive = patientHealth.curHealth != 0;
            isEnemyAlive = GameObject.FindGameObjectsWithTag("Enemy").Length != 0;
            winGame = !isEnemyAlive && areDamagePointsHealed;
            loseGame = !isPatientAlive;
            // Update the previous state
            wasPatientAlive = isPatientAlive;
            if (loseGame)
            {
                LoseGame();
            }
            else if (winGame)
            {

                WinGame();
            }
        }
    } 
    public void AddHint(int addHint)
    {
        TotalHints += Mathf.Clamp(addHint, 0, maxHints);
    }

    private void StackLoseScene()
    {
        ScenesManager.instance.StackScene(ScenesManager.Scene.Lose);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void LoseGame()
    {
        if (GameOverSound != null)
        {
            SoundFXManager.instance.PlaySoundEffectClip(GameOverSound, transform, 1f);
        }
        Time.timeScale = 0;
        loseMenu.SetActive(true);
        loseGame = false;
        gameOver = true;
    }
    public void WinGame()
    {
        Time.timeScale = 0;
        winMenu.SetActive(true);
        winGame = false;
        gameOver = true;
    }
}
