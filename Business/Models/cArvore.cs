using Business.DAO;
using Newtonsoft.Json;

namespace Business.Models
{
	public class cArvore
	{
		[JsonProperty(PropertyName = "Codigo")]
		public int arv_in_codigo { get; set; }

		[JsonProperty(PropertyName = "Descricao")]
		public string arv_st_descricao { get; set; }

		[JsonProperty(PropertyName = "Idade")]
		public int arv_in_idade { get; set; }

		[JsonProperty(PropertyName = "Especie")]
		public cEspecie Especie { get; set; }
	}
}