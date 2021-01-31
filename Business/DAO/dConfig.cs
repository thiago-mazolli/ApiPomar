using Business.Models;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using System.Windows.Forms;

namespace Business.DAO
{
	public class dConfig
	{
		public static cConfig ObterConteudo()
		{
			string texto;

			if(HttpRuntime.AppDomainAppId != null)
				texto = File.ReadAllText(HttpContext.Current.Request.PhysicalApplicationPath + @"Config.json");
			else
				texto = File.ReadAllText(Application.StartupPath + @"\Config.json");

			cConfig objConfig = JsonConvert.DeserializeObject<cConfig>(texto);
			return objConfig;
		}
	}
}
