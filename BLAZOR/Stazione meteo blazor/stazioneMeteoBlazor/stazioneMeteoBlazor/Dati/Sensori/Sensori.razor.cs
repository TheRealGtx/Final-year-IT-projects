using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace stazioneMeteoBlazor.Dati.Sensori
{
    public partial class Sensori : ComponentBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selezionare una grandezza")]
        public string grandezzaFisica { get; set; }
        public byte camera { get; set; }

        [Required(ErrorMessage = "Inserire un nome per il sensore")]
        [StringLength(30, ErrorMessage = "Il nome del sensore deve essere di massimo 30 caratteri")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Selezionare un tipo di sensore")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Inserire le caratteristiche del sensore")]
        [StringLength(40, ErrorMessage = "Le caratteristiche del sensore devono essere di massimo 40 caratteri")]
        public string caratteristiche { get; set; }

        [StringLength(50, ErrorMessage = "Le note devono essere di massimo 50 caratteri")]
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
