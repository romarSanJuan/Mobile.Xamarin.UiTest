using System;
using Xamarin.UITest;

namespace Mobile.Xamarin.UiTest
{
    public class AppManager
    {
        /* Mobile Application Package ID */
        private const string AndroidPackageId = "<ADD ANDROID PACKAGE ID HERE>";
        private const string iOSPackageId = "<ADD IOS PACKAGE ID HERE>";

        static IApp _app;
        public static IApp App
        {
            get
            {
                if (_app == null)
                    throw new NullReferenceException("'AppManager.App' not set. Call 'AppInitializer.StartApp()' before trying to access it.");
                return _app;
            }
        }

        static Platform? platform;
        public static Platform Platform
        {
            get
            {
                if (platform == null)
                    throw new NullReferenceException("'AppManager.Platform' not set.");
                return platform.Value;
            }

            set
            {
                platform = value;
            }
        }

        /* Starting the app based on Platform */
        public static void StartApp()
        {
            if (Platform == Platform.Android)
            {
                _app = ConfigureApp
                    .Android
                    .InstalledApp(AndroidPackageId)
                    .EnableLocalScreenshots()
                    .StartApp();
            }

            if (Platform == Platform.iOS)
            {
                _app = ConfigureApp
                    .iOS
                    .InstalledApp(iOSPackageId)
                .EnableLocalScreenshots()
                .StartApp();
            }
        }
    }
}
