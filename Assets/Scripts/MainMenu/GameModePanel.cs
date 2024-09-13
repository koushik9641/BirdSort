
namespace MainMenu
{
    public class GameModePanel : ShowHidable
    {

        public void OnClickButton(int mode)
        {
            var levelsPanel = UIManager.Instance.LevelsPanel;
            levelsPanel.GameMode = (GameMode)mode;

            //levelsPanel.Show();
            // Get the completed level tile
            LevelTileUI completedLevelTile = levelsPanel.GetCompletedLevelTileUI();

            // Pass the completed level tile to loadlevelDirect
            levelsPanel.loadlevelDirect(completedLevelTile);
            //levelsPanel.Show();
        }

    }
}