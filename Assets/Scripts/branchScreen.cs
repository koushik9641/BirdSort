using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class branchScreen : MonoBehaviour
{

    private SpriteRenderer _renderer;

    public Sprite[] branchlist;
    private void Awake()
    {
        // Get the background setting from PlayerPrefs
        string bgsetName = PlayerPrefs.GetString("bgset", "defaultBackgrounds");

        // Get the SpriteRenderer component
        _renderer = GetComponent<SpriteRenderer>();

        // Check different background sets and assign the corresponding sprite
        switch (bgsetName)
        {
            case "defaultBackgrounds": // bgset1
                _renderer.sprite = branchlist[0];
                break;

            case "bgset2":
                _renderer.sprite = branchlist[1];
                break;

            case "bgset3":
                _renderer.sprite = branchlist[1];
                break;

            case "bgset4":
                _renderer.sprite = branchlist[2];
                break;

            case "bgset5":
                _renderer.sprite = branchlist[2];
                break;

            case "bgset6":
                _renderer.sprite = branchlist[3];
                break;

            case "bgset7":
                _renderer.sprite = branchlist[3];
                break;

            case "bgset8":
                _renderer.sprite = branchlist[4];
                break;

            case "bgset9":
                _renderer.sprite = branchlist[4];
                break;

            case "bgset10":
                _renderer.sprite = branchlist[5];
                break;

            case "bgset11":
                _renderer.sprite = branchlist[5];
                break;

            case "bgset12":
                _renderer.sprite = branchlist[6];
                break;

            case "bgset13":
                _renderer.sprite = branchlist[6];
                break;

            case "bgset14":
                _renderer.sprite = branchlist[7];
                break;

            case "bgset15":
                _renderer.sprite = branchlist[7];
                break;

            case "bgset16":
                _renderer.sprite = branchlist[8];
                break;

            case "bgset17":
                _renderer.sprite = branchlist[8];
                break;

            default:
                Debug.LogWarning("No valid background set found.");
                break;
        }
    }



    void Update()
    {
        
    }
}
