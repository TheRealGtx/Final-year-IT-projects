using System.ComponentModel.DataAnnotations;

namespace stazioneMeteoBlazor.Dati.Stazioni
{
    public partial class Stazioni
    {
        [Required(ErrorMessage = "Inserire un nome per la stazione")]
        [StringLength(10, ErrorMessage = "Il nome della stazione deve essere di massimo 10 caratteri")]
        public string Id { get; set; }

        [StringLength(39, ErrorMessage = "L'indirizzo IP deve essere di massimo 39 caratteri")]
        public string Ip { get; set; } = " ";

        [StringLength(50, ErrorMessage = "L'indirizzo deve essere di massimo 50 caratteri")]
        public string Indirizzo { get; set; } = " ";
        public decimal Latitudine { get; set; } = 0;
        public decimal Longitudine { get; set; } = 0;
        public int Altitudine { get; set; } = 0;

        [StringLength(50, ErrorMessage = "La descrizione deve essere di massimo 50 caratteri")]
        public string Descrizione { get; set; } = " ";

        [StringLength(50, ErrorMessage = "Le note devono essere di massimo 50 caratteri")]
        public string Note { get; set; } = " ";



        private string Url(string tabella, string azione, string parametro = null)
        {
            var url = $"/{tabella}/{azione}";
            if (parametro != null)
                url += "/" + parametro;
            return url;
        }
    }
}
