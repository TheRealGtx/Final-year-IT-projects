namespace stazioneMeteoBlazor.Dati.Sensori
{
    public partial class Modifica
    {
        public Sensori? _sensore;

        private bool scelta;
        private bool finestra;

        protected override void OnInitialized()
        {
            _sensore = sensoriQuery.ottieneSensoriById(id);

            if(_sensore.camera == 1)
            {
                scelta = true;
            }
            else
            {
                scelta = false;
            }
        }

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

            sensoriQuery.ModificaSensori(_sensore);
            finestra = true;
        }
    }
}
