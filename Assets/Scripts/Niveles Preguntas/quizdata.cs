using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quizdata : MonoBehaviour
{
    [SerializeField] private List<Preguntas> Plista = null;
    private List<Preguntas> backup = null;

    private void Awake()
    {
        backup = Plista;
    }
    public Preguntas GetPreguntas(bool remove = true)
    {
        if (Plista.Count == 0)
        {
            restorebackup();
        }
        int index = Random.Range(0, Plista.Count);
        
            if (!remove)
            { return Plista[index]; }

        Preguntas p = Plista[index];
        Plista.RemoveAt(index);
        return p;
    }
    private void restorebackup()
    {
        Plista=backup;


    }
}
