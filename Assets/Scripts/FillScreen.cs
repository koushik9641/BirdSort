using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FillScreen : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Camera _camera;

    public Sprite[] backgroundlist;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _camera = Camera.main;

        //if(_renderer != null)
        //   _renderer.sprite= backgroundlist[UnityEngine.Random.Range(0, backgroundlist.Length)];


        // Retrieve the stored background set name from PlayerPrefs
        string bgsetName = PlayerPrefs.GetString("bgset", "defaultBackgrounds"); // Use "defaultBackground" if no value is set

        // Find the corresponding sprite from the backgroundlist
        Sprite selectedSprite = Array.Find(backgroundlist, sprite => sprite.name == bgsetName);

        // If a matching sprite is found, set it; otherwise, use a default sprite or handle the case appropriately
        if (selectedSprite != null)
        {
            _renderer.sprite = selectedSprite;
        }
        else
        {
            Debug.LogWarning($"Sprite with name '{bgsetName}' not found in the background list.");
            // Optionally assign a default sprite here if needed
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFillScreen();
    }

    private void UpdateFillScreen()
    {
        var height = _camera.orthographicSize*2;
        var width = height*Screen.width/Screen.height;

        var aspect = (float)Screen.width / Screen.height;
        var imgAspect =  _renderer.sprite.bounds.extents.x/ _renderer.sprite.bounds.extents.y;

        if (aspect >= imgAspect)
        {
            transform.localScale = Vector3.one*width/(2*_renderer.sprite.bounds.extents.x);
        }
        else
        {
            transform.localScale = Vector3.one * height / (2 * _renderer.sprite.bounds.extents.y);
        }
        

    }
}
