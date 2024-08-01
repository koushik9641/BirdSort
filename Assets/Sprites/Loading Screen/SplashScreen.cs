using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public Image splashFillImage;
    public float loadingTime=0.1f;

    private void Start()
    {
        splashFillImage.fillAmount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(splashFillImage.fillAmount<1)
        {
            splashFillImage.fillAmount += Time.deltaTime*loadingTime;
        }
        else
        {
            SceneManager.LoadScene(1);
        }

    }
}
