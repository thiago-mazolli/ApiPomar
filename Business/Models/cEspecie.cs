using Business.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Business.DAO
{
	public class cEspecie
	{
		[JsonProperty(PropertyName = "Codigo")]
		public int esp_in_codigo { get; set; }

		[JsonProperty(PropertyName = "Descricao")]
		public string esp_st_descricao { get; set; }
	}
}