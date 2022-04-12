using System.Collections.Generic;
using System.Configuration;

namespace NunitProject.Tests.Home
{
	public static class HomePageConstants
	{
		public static readonly IList<KeyValuePair<string, string>> OurServicesLinks = new List<KeyValuePair<string, string>>
		{
            new KeyValuePair<string, string>("IT Infrastructure", ConfigurationManager.AppSettings["StrypesEuUrl"] + "it-infra-devops/"),

			new KeyValuePair<string, string>("Mobility and Transportation", ConfigurationManager.AppSettings["StrypesEuUrl"] + "mobility-and-transportation/"),

			new KeyValuePair<string, string>("Remote Diagnostics Monitoring and Predictive Maintenance Applications", ConfigurationManager.AppSettings["StrypesEuUrl"] + "remote-diagnostics/"),

			new KeyValuePair<string, string>("Modularity services", ConfigurationManager.AppSettings["StrypesEuUrl"] + "modularity-services/"),

			new KeyValuePair<string, string>("Digital Transformation", ConfigurationManager.AppSettings["StrypesEuUrl"] + "digital-transformation/"),

			new KeyValuePair<string, string>("Consultancy", ConfigurationManager.AppSettings["StrypesEuUrl"] + "consultancy/"),

			new KeyValuePair<string, string>("Smart Applications - Applications Development, Modernization & Management", ConfigurationManager.AppSettings["StrypesEuUrl"] + "smart-applications/"),
		};

		public static readonly List<string> OurServicesImgSourceUrl = new()
        {
			"https://strypes.eu/wp-content/uploads/2021/07/services1.svg",
			"https://strypes.eu/wp-content/uploads/2021/07/services2.svg",
			"https://strypes.eu/wp-content/uploads/2021/07/services5.svg",
			"https://strypes.eu/wp-content/uploads/2021/08/icon-training-2.svg",
			"https://strypes.eu/wp-content/uploads/2021/07/services4.svg",
			"https://strypes.eu/wp-content/uploads/2021/08/icon-consultancy-2.svg",
			"https://strypes.eu/wp-content/uploads/2021/07/services6.svg"
		};

		public static readonly List<string> OurServicesImgRedirectUrl = new()
        {
			"https://strypes.eu/it-infra-devops/",
			"https://strypes.eu/mobility-and-transportation/",
			"https://strypes.eu/remote-diagnostics/",
			"https://strypes.eu/services/",
			"https://strypes.eu/digital-transformation/",
			"https://strypes.eu/consultancy/",
			"https://strypes.eu/smart-applications/"
		};

		public static readonly List<string> WorkWithCompanyUrls = new()
		{
			"https://www.arvos-group.com/",
			"https://www.asml.com/",
			"https://www.greenflux.com/",
			"https://www.epra.com/",
			"https://www.valid.nl/en/",
			"https://www.lely.com/",
			"https://www.viasatconnect.be/en/company/",
			"https://www.degiro.eu/"
		};
	}
}
