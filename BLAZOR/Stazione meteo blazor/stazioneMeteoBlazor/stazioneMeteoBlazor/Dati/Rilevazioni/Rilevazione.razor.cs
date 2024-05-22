using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace stazioneMeteoBlazor.Dati.Rilevazioni
{
    public partial class Rilevazione : ComponentBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selezionare un sensore")]
        public string Sensore { get; set; }

        [Required(ErrorMessage = "Inserire una data")]
        public DateTime Data { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Inserire un rilevamento")]
        [StringLength(15, ErrorMessage = "Il rilevamento deve essere di massimo 15 caratteri")]
        public string Rilevamento { get; set; }
        
        private string Url(string tabella, string azione, string parametro = null)
        {
            var url = $"/{tabella}/{azione}";
            if (parametro != null)
                url += "/" + parametro;
            return url;
        }
    }
}
