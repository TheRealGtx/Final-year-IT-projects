namespace stazioneMeteoBlazor.Dati.GrandezzaFisica
{
    public partial class Modifica
    {
        private GrandezzaFisica? _grandezza;

        private bool scelta;
        private bool finestra;

        protected override void OnInitialized()
        {
            _grandezza = grandezzaFisicaQuery.ottieneGrandezzaById(id!);
        }

        private void Submit()
        {
            grandezzaFisicaQuery.ModificaGrandezzaFisica(_grandezza);
            finestra = true;
        }
    }
}
