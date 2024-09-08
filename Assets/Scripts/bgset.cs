using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void buyBgset()
    {
        PlayerPrefs.SetString("bgset", gameObject.name);
    }
}
