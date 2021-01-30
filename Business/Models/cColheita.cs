using Business.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.DAO
{
	public class cColheita
	{
		[JsonProperty(PropertyName = "Codigo")]
		public int col_in_codigo { get; set; }

		[JsonProperty(PropertyName = "Data")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime col_dt_datacolheita { get; set; }

		[JsonProperty(PropertyName = "Peso")]
		public decimal col_re_peso { get; set; }

		[JsonProperty(PropertyName = "Arvore")]
		public cArvore Arvore { get; set; }
	}
}