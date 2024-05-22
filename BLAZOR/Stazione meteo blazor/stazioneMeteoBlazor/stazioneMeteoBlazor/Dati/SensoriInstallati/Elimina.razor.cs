using stazioneMeteoBlazor.Dati.Rilevazioni;

namespace stazioneMeteoBlazor.Dati.SensoriInstallati
{
    public partial class Elimina
    {
        private SensoriInstallati? _installazione;
        private bool finestra = false;
        protected override void OnInitialized()
        {
            _installazione = sensoriInstallatiQuery.ottieneInstallazioniById(id);
        }
        private void EliminaRecord()
        {
            sensoriInstallatiQuery.EliminaInstallazione(id);
            finestra = true;
        }
    }
}
