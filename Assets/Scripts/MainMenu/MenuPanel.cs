

using UnityEngine;

namespace MainMenu
{
    public class MenuPanel : MonoBehaviour
    {

        public string privacyurl;
        public string androidstoreurl;
        public string iosstoreurl;

        private void Start()
        {

            if (ResourceManager.GetCompletedLevel(GameMode.All) > 10)
            {

            }

        }


            public void OnClickPlay()
        {
            UIManager.Instance.GameModePanel.Show();
        }

        public void OnClickurl()
        {

            

        }

        public void GotoUrl()
        {
            Application.OpenURL(privacyurl);

        }

        public void OpenStoreUrl()
        {


            if ((Application.platform == RuntimePlatform.Android) || (Application.platform == RuntimePlatform.WindowsEditor))
            {

                Application.OpenURL(androidstoreurl);
            }
            else
            {
                Application.OpenURL(iosstoreurl);

            }
        }


        public void OpenGameUrl()
        {
            Application.OpenURL(Application.platform == RuntimePlatform.IPhonePlayer ? $"http://itunes.apple.com/app/id{GameSettings.Default.IosAppId}" :
             $"market://details?id={Application.identifier}"
             );
        }
    }
}