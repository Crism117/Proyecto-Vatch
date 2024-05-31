using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Runtime.CompilerServices;

public class TriviaLog : MonoBehaviour
{
    public static TriviaLog instance;

    public void Awake()
    {
        if (instance == null)
        instance = this;
    }

    [SerializeField] Button[] answerBoxes = new Button[5];
     public string[] question1 = new string[5];
     public string[] question2 = new string[5];
     public string[] question3 = new string[5];
     public string[] question4 = new string[5];
     public string[] question5 = new string[5];
    [HideInInspector] public string[] currentQuestion = new string[5];
    [HideInInspector] public string[] shuffledQuestion = new string[5];
    [SerializeField] TextMeshProUGUI lifeCounter;

    void Start()
    {
        if (lifeCounter != null)
            lifeCounter.text = LivesManager.instance.curLives.ToString() + "/" + LivesManager.instance.maxLives.ToString();
        currentQuestion = question1;
        Debug.Log("Original Array: " + string.Join(", ", currentQuestion));

        shuffledQuestion = (string[])currentQuestion.Clone();
        ShuffleArray(shuffledQuestion);

        Debug.Log("Shuffled Array: " + string.Join(", ", shuffledQuestion));
        UpdateAnswerBoxes(shuffledQuestion);
    }
    string[] ShuffleArray(string[] ogArray)
    {
        string[] array = ogArray;
        for (int i = 4; i > 1; i--)
        {
            int randomIndex = UnityEngine.Random.Range(1, i + 1); // Ensure randomIndex is between 1 and i
            string temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
        return array;
    }
    void UpdateAnswerBoxes(string[] shuffledAnswers)
    {
        for (int i = 0; i < answerBoxes.Length; i++)
        {
            TextMeshProUGUI tmp = answerBoxes[i].GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = shuffledAnswers[i];
            }
            else
            {
                Debug.LogError("No TextMeshProUGUI component found in children of answerBox " + i);
            }
        }
    }
    public void CompareAnswer(TextMeshProUGUI answerText, string[] question)
    {
        if (answerText != null && question.Length > 1)
        {
            if (answerText.text == question[1])
            {
                if (currentQuestion[0] != question5[0])
                { ChangeQuestion(); }
                else 
                {
                    GameManagerQuestions.instance.WinGame();
                }
                Debug.Log("Answer is correct!");
            }
            else
            {
                    LivesManager.instance.LoseLife();
                if (lifeCounter != null)
                    lifeCounter.text = LivesManager.instance.curLives.ToString() + "/" + LivesManager.instance.maxLives.ToString();

            }
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is null or currentQuestion array is too short.");
        }
    }
    public void ChangeQuestion()
    {
        if (currentQuestion[0] == question1[0])
        {
            currentQuestion = question2;
        }
        else if (currentQuestion[0] == question2[0])
        {
            currentQuestion = question3;
        }
        else if (currentQuestion[0] == question3[0])
        {
            currentQuestion = question4;
        }
        else if (currentQuestion[0] == question4[0])
        {
            currentQuestion = question5;
        }
        else if (currentQuestion[0] == question5[0])
        {
            Debug.Log("You Win"); // Or handle end of questions as needed
        }

        // Create a copy of currentQuestion for shuffling
        shuffledQuestion = (string[])currentQuestion.Clone();
        ShuffleArray(shuffledQuestion);

        Debug.Log("New Current Question: " + string.Join(", ", currentQuestion));
        Debug.Log("Shuffled New Question: " + string.Join(", ", shuffledQuestion));

        UpdateAnswerBoxes(shuffledQuestion);
    }
}
