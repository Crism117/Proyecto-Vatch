using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    private scripptUi nscript = null;
    private quizdata quiz = null;
    [SerializeField] private AudioClip correctsound = null;
    [SerializeField] private AudioClip nocorrectsound = null;
    private AudioSource naudio = null;
   [SerializeField] private float wait = 0.0f;
    public void Start()
    {
        quiz = GameObject.FindObjectOfType<quizdata>();
        nscript = GameObject.FindObjectOfType<scripptUi>();
        naudio = GetComponent<AudioSource>();

        siguientePregunta();
    }
    private void siguientePregunta()
    {
        nscript.menu(quiz.GetPreguntas(), respuestac);

    }
    private void respuestac(opciones2 boton)
    {
    StartCoroutine(respuestaroutine(boton));

    }
    private IEnumerator respuestaroutine(opciones2 boton)
    {
        if (naudio.isPlaying)
        {
            naudio.Stop();
        }
        naudio.clip = boton.opcion.correct ? correctsound : nocorrectsound;
        yield return new WaitForSeconds(wait);
        if (boton.opcion.correct) 
        {
            siguientePregunta();
        }
       
    
    }
}
