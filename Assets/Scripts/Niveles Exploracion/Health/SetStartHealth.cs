using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartHealth : MonoBehaviour
{
    public int setHealthTo;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.TryGetComponent(out Health health))
        {
            health.curHealth = setHealthTo;
        }
    }
}
