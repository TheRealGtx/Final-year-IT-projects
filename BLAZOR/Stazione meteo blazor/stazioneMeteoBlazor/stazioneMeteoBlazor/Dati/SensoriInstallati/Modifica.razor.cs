namespace stazioneMeteoBlazor.Dati.SensoriInstallati
{
    public partial class Modifica
    {
        private SensoriInstallati? _installazione;
        private bool finestra = false;

        protected override void OnInitialized()
        {
            _installazione = sensoriInstallatiQuery.ottieneInstallazioniById(id!);
        }

        private void Submit()
        {
            sensoriInstallatiQuery.ModificaInstallazioni(_installazione);
            finestra = true;
        }
    }
}
