namespace stazioneMeteoBlazor.Dati.GrandezzaFisica
{
    public partial class Crea
    {
        private GrandezzaFisica? _grandezza;

        private bool scelta;
        private bool finestra;

        protected override void OnInitialized() => _grandezza ??= new();

        private void Submit()
        {
            grandezzaFisicaQuery.CreaGrandezzaFisica(_grandezza);
            finestra = true;
        }
    }
}
