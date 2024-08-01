using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsSortingController : MonoBehaviour
{
    public static BirdsSortingController instance;

    public int birdTypes = 2;
    public int birdsSorted = 0;
    public List<GameObject> selectedbirds;
    public Transform lastTarget;
    public GameObject levelCompletePanal,gamePlayPanal;
    bool levelCompleted = false;

    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void getbirdscount(int birds)
    {
        birdTypes = birds;
    }
    public void levelcomplete()
    {
        if (birdsSorted == birdTypes && !levelCompleted)
        {
            levelCompleted = true;
            LevelManager.Instance.OverTheGame();
            //GameManager.instance.LevelComplete();
            //levelCompletePanal.SetActive(true);
            //gamePlayPanal.SetActive(false);
        }
    }
}
