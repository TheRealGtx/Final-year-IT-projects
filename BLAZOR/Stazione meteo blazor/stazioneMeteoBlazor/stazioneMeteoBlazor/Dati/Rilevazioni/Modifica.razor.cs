namespace stazioneMeteoBlazor.Dati.Rilevazioni
{
    public partial class Modifica
    {
        private Rilevazione? _rilevazione;
        private bool finestra = false;


        protected override void OnInitialized()
        {
            _rilevazione = rilevazioniQuery.ottieneRilevazioniById(id!);
        }

        private void Submit()
        {
            rilevazioniQuery.ModificaRilevazioni(_rilevazione);
            finestra = true;
        }
    }
}
