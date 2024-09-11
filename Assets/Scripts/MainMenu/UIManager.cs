using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MainMenu
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private LevelsPanel _levelsPanel;
        [SerializeField] private GameModePanel _gameModePanel;

        [SerializeField] private TextMeshProUGUI _coinValue;
        public GameModePanel GameModePanel => _gameModePanel;
        public LevelsPanel LevelsPanel => _levelsPanel;


        private void Awake()
        {
            Instance = this;

            updateCoinvalue();
            //CrossPromo.Instance.AutoShowPopupWhenReady();


            //RateGame.Instance.ShowRatePopup();


            //Advertisements.Instance.Initialize();
            //Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);

        }

        public void updateCoinvalue()
        {
            int coins = PlayerPrefs.GetInt("coinsValue");
            _coinValue.text = coins.ToString();
        }

    }
}