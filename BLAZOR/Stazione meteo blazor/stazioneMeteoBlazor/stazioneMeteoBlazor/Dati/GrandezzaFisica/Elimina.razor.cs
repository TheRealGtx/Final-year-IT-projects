namespace stazioneMeteoBlazor.Dati.GrandezzaFisica
{
    public partial class Elimina
    {
        private GrandezzaFisica? _grandezza;
        private bool finestra;
        protected override void OnInitialized()
        {
            _grandezza = grandezzaFisicaQuery.ottieneGrandezzaById(id);
        }
        private void EliminaRecord()
        {
            grandezzaFisicaQuery.EliminaGrandezzaFisica(id);
            finestra = true;
        }
    }
}
