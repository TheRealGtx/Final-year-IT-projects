using stazioneMeteoBlazor.AltriComponenti;

namespace stazioneMeteoBlazor.Dati.Rilevazioni
{
    public partial class Crea
    {
        private Rilevazione? _rilevazione;

        private bool finestra = false;

        protected override void OnInitialized() => _rilevazione ??= new();

        private void Submit()
        {
            rilevazioniQuery.CreaRilevazione(_rilevazione);
            finestra = true;
        }
    }
}
