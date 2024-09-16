using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using System.Linq;

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


        [SerializeField] private TextMeshProUGUI levelno;

        private void Awake()
        {
            Instance = this;

            updateCoinvalue();
                    
            //levelno.text = "Play " + (completedLevel + 1).ToString();

            //CrossPromo.Instance.AutoShowPopupWhenReady();


            //RateGame.Instance.ShowRatePopup();


            //Advertisements.Instance.Initialize();
            //Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);

        }
        private void Start()
        {
            var levelsPanel = UIManager.Instance.LevelsPanel;
            levelsPanel.GameMode = (GameMode)4;

            int completedLevelTile = levelsPanel.levelnoUI();
 
            levelno.text = "Play " + (completedLevelTile + 1).ToString();

        }


        public void updateCoinvalue()
        {
            int coins = PlayerPrefs.GetInt("coinsValue");
            _coinValue.text = coins.ToString();
        }


       
    }
}