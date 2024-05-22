namespace stazioneMeteoBlazor.Dati.Sensori
{
    public partial class Crea
    {
        private Sensori? _sensore;

        private string stato = "Nessun nuovo sensore inserito";

        private bool scelta;
        private bool finestra;

        protected override void OnInitialized() => _sensore ??= new();

        private void Submit()
        {
            if (scelta == true)
            {
                _sensore.camera = 1;
            }
            else
            {
                _sensore.camera = 0;
            }

            sensoriQuery.CreaSensore(_sensore);
            finestra = true;
        }
    }
}
