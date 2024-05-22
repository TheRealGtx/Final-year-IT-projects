using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace stazioneMeteoBlazor.Dati.SensoriInstallati
{
    public partial class SensoriInstallati : ComponentBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selezionare un sensore")]
        public string Sensore { get; set; }

        [Required(ErrorMessage = "Selezionare una stazione")]
        public string Stazione { get; set; }

        [StringLength(50, ErrorMessage = "Le note devono essere di massimo 50 caratteri")]
        public string Note { get; set; } = "";

        private string Url(string tabella, string azione, string parametro = null)
        {
            var url = $"/{tabella}/{azione}";
            if (parametro != null)
                url += "/" + parametro;
            return url;
        }
    }
}
