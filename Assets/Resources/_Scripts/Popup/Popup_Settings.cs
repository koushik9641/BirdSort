using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Settings : Popup
{
    [SerializeField] private Toggle _toggleMusic = null;
    [SerializeField] private Toggle _toggleSFX = null;
    [SerializeField] private Text _textMusic = null;
    [SerializeField] private Text _textSFX = null;
    [SerializeField] private Color _colorOn = Color.white;
    [SerializeField] private Color _colorOff = Color.white;


    public override void OnOpen()
    {
        base.OnOpen();

        _toggleSFX.isOn = PlayerPrefs.GetInt(KeyName.settingSFX, 1) == 1;
        _toggleMusic.isOn = PlayerPrefs.GetInt(KeyName.settingMusic, 1) == 1;

        SetMusic(_toggleMusic.isOn);
        SetSFX(_toggleSFX.isOn);

        _toggleSFX.onValueChanged.RemoveAllListeners();
        _toggleSFX.onValueChanged.AddListener(ToggleSFX);

        _toggleMusic.onValueChanged.RemoveAllListeners();
        _toggleMusic.onValueChanged.AddListener(ToggleMusic);

        //_buttonFacebook.onClick.RemoveAllListeners();
        //_buttonFacebook.onClick.AddListener(GoFacebook);

        //_buttonInstagram.onClick.RemoveAllListeners();
        //_buttonInstagram.onClick.AddListener(GoInstagram);
    }

    private void ToggleMusic(bool value)
    {
        value = _toggleMusic.isOn;

        AudioManager.Click();

        PlayerPrefs.SetInt(KeyName.settingMusic, value ? 1 : 0);
        SetMusic(value);
    }

    private void SetMusic(bool value)
    {
        //Debug>Logge
        if (value)
        {
            _textMusic.text = "ON";
            _textMusic.color = _colorOn;
        }
        else
        {
            _textMusic.text = "OFF";
            _textMusic.color = _colorOff;
        }

        AudioManager.Instance.ToggleMusic(value);
    }

    private void ToggleSFX(bool value)
    {
        value = _toggleSFX.isOn;

        PlayerPrefs.SetInt(KeyName.settingSFX, value ? 1 : 0);
        SetSFX(value);

        AudioManager.Click();
    }

    private void SetSFX(bool value)
    {
        if (value)
        {
            _textSFX.text = "ON";
            _textSFX.color = _colorOn;
        }
        else
        {
            _textSFX.text = "OFF";
            _textSFX.color = _colorOff;
        }
        AudioManager.Instance.ToggleSFX(value);
    }
}
