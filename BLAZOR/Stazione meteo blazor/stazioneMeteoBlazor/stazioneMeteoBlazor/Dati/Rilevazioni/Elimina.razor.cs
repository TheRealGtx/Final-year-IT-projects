namespace stazioneMeteoBlazor.Dati.Rilevazioni
{
    public partial class Elimina
    {
        private Rilevazione? _rilevazione;
        private bool finestra = false;
        protected override void OnInitialized()
        {
            _rilevazione = rilevazioniQuery.ottieneRilevazioniById(id);
        }
        private void EliminaRecord()
        {
            rilevazioniQuery.EliminaRilevazione(id);
            finestra = true;
        }
    }
}
