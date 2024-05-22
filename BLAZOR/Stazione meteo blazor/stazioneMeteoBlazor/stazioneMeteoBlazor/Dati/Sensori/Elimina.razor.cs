namespace stazioneMeteoBlazor.Dati.Sensori
{
    public partial class Elimina
    {
        private Sensori? _sensore;
        private bool finestra;
        protected override void OnInitialized()
        {
            _sensore = sensoriQuery.ottieneSensoriById(id);
        }
        private void EliminaRecord()
        {
            sensoriQuery.EliminaSensore(id);
            finestra = true;
        }
    }
}
