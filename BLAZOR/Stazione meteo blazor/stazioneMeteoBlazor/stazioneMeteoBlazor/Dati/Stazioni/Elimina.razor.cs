namespace stazioneMeteoBlazor.Dati.Stazioni
{
    public partial class Elimina
    {
        private Stazioni? _stazione;
        private bool finestra = false;
        protected override void OnInitialized()
        {
            _stazione = stazioniQuery.ottieneStazioniById(id);
        }
        private void EliminaRecord()
        {
            stazioniQuery.EliminaStazione(id);
            finestra = true;
        }
    }
}
