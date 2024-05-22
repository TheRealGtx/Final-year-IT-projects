using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace stazioneMeteoBlazor.AltriComponenti
{
    public partial class FinestraDati
    {
        public Dictionary<string, string> messaggio = new Dictionary<string, string>()
        {
            {"Modifica", "Dato modificato con successo" },
            {"Crea", "Nuovo dato aggiunto alla tabella" },
            {"Elimina", "Il dato è stato eliminato" }
        };

        public Dictionary<string, string> pagine = new Dictionary<string, string>()
        {
            {"Rilevazioni", "/Rilevazioni" },
            {"Sensori", "/Sensori" },
            {"GrandezzaFisica", "/GrandezzaFisica" },
            {"SensoriInstallati", "/SensoriInstallati" },
            {"Stazioni", "/Stazioni" },
        };

        void Ricarica()
        {
            NavigationManager.NavigateTo(pagine[Tabella],
            new NavigationOptions { ForceLoad = true });
        }
    }
}
