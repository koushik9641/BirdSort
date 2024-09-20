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
    public GameObject levelCompletePanal, gamePlayPanal;
    bool levelCompleted = false;

    public float xMin = -10f; // Define the min X position for randomness
    public float xMax = 10f;  // Define the max X position for randomness

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Move lastTarget to a random X position on start
        MoveLastTargetRandomX();
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
            // levelCompletePanal.SetActive(true);
            // gamePlayPanal.SetActive(false);
        }
    }

    // Method to move lastTarget randomly along the X-axis
    public void MoveLastTargetRandomX()
    {
        if (lastTarget != null)
        {
            float randomX = Random.Range(xMin, xMax);
            lastTarget.position = new Vector3(randomX, lastTarget.position.y, lastTarget.position.z);
        }
    }
}
