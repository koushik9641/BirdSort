using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class branchScreen : MonoBehaviour
{

    private SpriteRenderer _renderer;

    public Sprite[] branchlist; // Total of 27 sprites in this array
    private void Awake()
    {
        // Get the background setting from PlayerPrefs
        string bgsetName = PlayerPrefs.GetString("bgset", "defaultBackgrounds");

        // Get the SpriteRenderer component
        _renderer = GetComponent<SpriteRenderer>();

        // Check different background sets and assign a random sprite from the corresponding range
        switch (bgsetName)
        {
            case "defaultBackgrounds": // bgset1
                _renderer.sprite = branchlist[Random.Range(0, 3)]; // Random between branchlist[0] - branchlist[2]
                break;

            case "bgset2": // bgset2
                _renderer.sprite = branchlist[Random.Range(3, 6)]; // Random between branchlist[0] - branchlist[2]
                break;

            case "bgset3": // bgset3
                _renderer.sprite = branchlist[Random.Range(3, 6)]; // Random between branchlist[0] - branchlist[2]
                break;

            case "bgset4": // bgset4
                _renderer.sprite = branchlist[Random.Range(6, 9)]; // Random between branchlist[3] - branchlist[4]
                break;

            case "bgset5": // bgset5
                _renderer.sprite = branchlist[Random.Range(6, 9)]; // Random between branchlist[3] - branchlist[4]
                break;

            case "bgset6": // bgset6
                _renderer.sprite = branchlist[Random.Range(9, 12)]; // Random between branchlist[5] - branchlist[6]
                break;

            case "bgset7": // bgset7
                _renderer.sprite = branchlist[Random.Range(9, 12)]; // Random between branchlist[5] - branchlist[6]
                break;

            case "bgset8": // bgset8
                _renderer.sprite = branchlist[Random.Range(12, 15)]; // Random between branchlist[7] - branchlist[8]
                break;

            case "bgset9": // bgset9
                _renderer.sprite = branchlist[Random.Range(12, 15)]; // Random between branchlist[7] - branchlist[8]
                break;

            case "bgset10": // bgset10
                _renderer.sprite = branchlist[Random.Range(15, 18)]; // Random between branchlist[9] - branchlist[10]
                break;

            case "bgset11": // bgset11
                _renderer.sprite = branchlist[Random.Range(15, 18)]; // Random between branchlist[9] - branchlist[10]
                break;

            case "bgset12": // bgset12
                _renderer.sprite = branchlist[Random.Range(18, 21)]; // Random between branchlist[11] - branchlist[12]
                break;

            case "bgset13": // bgset13
                _renderer.sprite = branchlist[Random.Range(18, 21)]; // Random between branchlist[11] - branchlist[12]
                break;

            case "bgset14": // bgset14
                _renderer.sprite = branchlist[Random.Range(21, 24)]; // Random between branchlist[13] - branchlist[14]
                break;

            case "bgset15": // bgset15
                _renderer.sprite = branchlist[Random.Range(21, 24)]; // Random between branchlist[13] - branchlist[14]
                break;

            case "bgset16": // bgset16
                _renderer.sprite = branchlist[Random.Range(24, 27)]; // Random between branchlist[15] - branchlist[16]
                break;

            case "bgset17": // bgset17
                _renderer.sprite = branchlist[Random.Range(24, 27)]; // Random between branchlist[15] - branchlist[16]
                break;

            default:
                Debug.LogWarning("No valid background set found.");
                break;
        }
    }

    void Update()
    {
        // Optional updates if necessary
    }
}
