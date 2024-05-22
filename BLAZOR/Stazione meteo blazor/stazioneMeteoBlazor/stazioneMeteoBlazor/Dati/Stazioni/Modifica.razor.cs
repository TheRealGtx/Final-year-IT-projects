namespace stazioneMeteoBlazor.Dati.Stazioni
{
    public partial class Modifica
    {
        private Stazioni? _stazione;
        private bool finestra = false;

        protected override void OnInitialized()
        {
            _stazione = stazioniQuery.ottieneStazioniById(id!);
        }

        private void Submit()
        {
            stazioniQuery.ModificaStazioni(_stazione);
            finestra = true;
        }
    }
}
