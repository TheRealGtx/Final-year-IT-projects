namespace stazioneMeteoBlazor.Dati.Stazioni
{
    public partial class Crea
    {
        private Stazioni? _stazione;

        private bool finestra = false;

        protected override void OnInitialized() => _stazione ??= new();

        private void Submit()
        {
            stazioniQuery.CreaStazione(_stazione);
            finestra = true;
        }
    }
}
