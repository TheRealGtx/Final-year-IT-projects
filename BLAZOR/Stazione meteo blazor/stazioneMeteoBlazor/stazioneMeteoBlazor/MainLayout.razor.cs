namespace stazioneMeteoBlazor
{
    public partial class MainLayout
    {
        public string PercorsoPaginaCorrente => NavigationManager.Uri;

        private Dictionary<string, string> pagineSecondarie = new Dictionary<string, string>
        {
            {"", "https://www.ispascalcomandini.it/"},
            {"/SistemaMetereologico", "https://www.ispascalcomandini.it/"},
            {"/Rilevazioni", "https://www.ispascalcomandini.it/"},
            {"/ChiSiamo", "https://www.ispascalcomandini.it/"},
            {"/ComponentiElettronici", "https://www.ispascalcomandini.it/" },
            {"/Encoder", "https://www.ispascalcomandini.it/" },
            {"/ComponentiVari", "https://www.ispascalcomandini.it/" }
        };
    }
}
