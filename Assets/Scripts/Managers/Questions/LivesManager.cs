using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.CullingGroup;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public float maxLives = 3f;
    public float curLives = 3f;
    private void Start()
    {
        curLives = maxLives;
        Debug.Log(curLives);
    }
    public void LoseLife()
    {
        Debug.Log("Current lives before losing life: " + curLives);
        if (curLives > 1)
        {
            curLives -= 1; // Decrement curLives by 1
            Debug.Log("Current lives after losing life: " + curLives);
            if (gameObject.TryGetComponent(out Animator lowerLife))
            {
                lowerLife.SetTrigger("WrongAnswer");
            }
            else
                Debug.Log("no animator");
        }
        else
        {
            Debug.Log("No lives left, triggering game over.");
            GameManagerQuestions.instance.LoseGame();
        }
    }
}


