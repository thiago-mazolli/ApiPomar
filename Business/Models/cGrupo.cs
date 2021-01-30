using Business.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Business.DAO
{
	public class cGrupo
	{
		[JsonProperty(PropertyName = "Codigo")]
		public int gru_in_codigo { get; set; }

		[JsonProperty(PropertyName = "Descricao")]
		public string gru_st_descricao { get; set; }

		[JsonProperty(PropertyName = "Arvores")]
		public List<cArvore> Arvores { get; set; }
	}
}