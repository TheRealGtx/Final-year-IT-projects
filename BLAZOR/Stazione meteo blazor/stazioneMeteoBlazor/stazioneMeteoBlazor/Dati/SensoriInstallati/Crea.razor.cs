namespace stazioneMeteoBlazor.Dati.SensoriInstallati
{
    public partial class Crea
    {
        private SensoriInstallati? _installazione;

        private bool finestra = false;

        protected override void OnInitialized() => _installazione ??= new();

        private void Submit()
        {
            sensoriInstallatiQuery.CreaInstallazione(_installazione);
            finestra = true;
        }
    }
}
