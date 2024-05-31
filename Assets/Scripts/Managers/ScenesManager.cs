using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    private void Awake()
    {
        instance = this;
    }
    public enum Scene
    {
        Menu_Principal,
        Menu_Seleccion_De_Niveles,
        Menu_Opciones,
        Menu_Ajustes,
        Menu_Tutorial,
        Menu_Pausa,
        Win,
        Lose,
        Arterias_Exploracion,
        Arterias_Preguntas,
        Corazon_Exploracion,
        Corazon_Preguntas,
        Estomago_Exploracion,
        Estomago_Preguntas,
        Pancreas_Exploracion,
        Pancreas_Preguntas,
        Riñon_Exploracion,
        Riñon_Preguntas
    }
    public void LoadScene(Scene scene) 
    {
        SceneManager.LoadScene(scene.ToString());
    }
    public void LoadNewScene()
    {
        SceneManager.LoadScene(Scene.Menu_Seleccion_De_Niveles.ToString());
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.Menu_Principal.ToString());
    }
    public AsyncOperation StackScene(Scene scene)
    {
        return SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
    }
    public AsyncOperation UnstackScene(Scene scene)
    {
        return SceneManager.UnloadSceneAsync(scene.ToString());
    }
}
