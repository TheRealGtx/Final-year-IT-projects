using Microsoft.AspNetCore.Components;

namespace stazioneMeteoBlazor.AltriComponenti
{
    public partial class FinestraModaleErrore
    {
        void Ricarica()
        {
            NavigationManager.NavigateTo(
            "/",
            new NavigationOptions { ForceLoad = true });
        }
    }
}
