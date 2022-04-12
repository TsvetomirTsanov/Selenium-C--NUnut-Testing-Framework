using System;

namespace NunitProject.other
{
    public class BrowserSettings
    {
		private static readonly TimeSpan timeout = TimeSpan.FromMilliseconds(20000);
		private static readonly TimeSpan minTimeout = TimeSpan.FromMilliseconds(10000);
		private static readonly TimeSpan pollingInterval = TimeSpan.FromMilliseconds(200);

		private BrowserSettings(string browserType)
		{
			this.BrowserType = browserType;
		}

		public static BrowserSettings ChromeSettings => new BrowserSettings("chrome")
		{
			Timeout = timeout,
			MinTimeout = minTimeout,
			PollingInterval = pollingInterval
		};

		public string BrowserType { get; private set; }
		public TimeSpan Timeout { get; private set; }
		public TimeSpan MinTimeout { get; private set; }
		public TimeSpan PollingInterval { get; private set; }
	}
}
