using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace stazioneMeteoBlazor.Dati.GrandezzaFisica
{
    public partial class GrandezzaFisica : ComponentBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Inserire il tipo di grandezza")]
        [StringLength(20, ErrorMessage = "La grandezza deve essere di massimo 20 caratteri")]
        public string grandezzaFisica { get; set; }

        [StringLength(6, ErrorMessage = "Il simbolo deve essere di massimo 6 caratteri")]
        public string simbolo { get; set; } = "";

        [Required(ErrorMessage = "Inserire il simbolo adottato")]
        [StringLength(6, ErrorMessage = "Il simbolo adottato deve essere di massimo 6 caratteri")]
        public string simboloAdottato { get; set; }

        [StringLength(45, ErrorMessage = "Le note devono essere di massimo 45 caratteri")]
        public string note { get; set; } = "";

        private string Url(string tabella, string azione, string parametro = null)
        {
            var url = $"/{tabella}/{azione}";
            if (parametro != null)
                url += "/" + parametro;
            return url;
        }
    }
}
