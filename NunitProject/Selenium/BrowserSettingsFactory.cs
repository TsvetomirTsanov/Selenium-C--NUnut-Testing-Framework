using System;
using System.Configuration;

namespace NunitProject.other
{
	public static class BrowserSettingsFactory
	{
        public static BrowserSettings CreateBrowserSettings()
        {
            string browserType = ConfigurationManager.AppSettings["BrowserType"].ToLower().Trim();

            if (browserType.Equals("chrome"))
            {
                return BrowserSettings.ChromeSettings;
            }
            else
            {
                throw new NotImplementedException("Not supported browser type.");
            }
        }
    }
}
