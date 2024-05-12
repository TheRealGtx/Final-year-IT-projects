using Microsoft.AspNetCore.Components;

namespace ElencoClassi2.Classi
{

    public class Classi : ComponentBase
    {
        protected string testoRicerca = "testo di prova che non seleziona nulla";
        protected List<Classe> classi = new List<Classe> {
            new Classe { Id = 1, Nome = "3i", AnnoScolastico = "2023/24" },
            new Classe { Id = 2, Nome = "4i", AnnoScolastico = "2023/24" },
            new Classe { Id = 3, Nome = "5i", AnnoScolastico = "2023/24" }
        };
        // Ottiene la lista cercata.
        public List<Classe> ClassiSelezionate =>
        testoRicerca == null || testoRicerca == ""
        ? classi
        : (from c in classi
           where
           c.Nome.Contains(testoRicerca, StringComparison.OrdinalIgnoreCase) ||
           c.AnnoScolastico.Contains(testoRicerca, StringComparison.OrdinalIgnoreCase)
           select c).ToList();

        /// <summary>
        /// Fornisce l'html per il corpo della tabella.
        /// In generale però è necessario stare attenti ad evitare il rischio che
        /// la stringa restituita non sia sicura e non contenga vulnerabilità
        /// XSS (Cross-Site Scripting).
        /// Utile per il caso 1 e non utilizzato nel caso 2.
        /// </summary>
        /// <returns>Html da mostrare.</returns>
        /// 

        // Classe interna alla classe Classi.
        public class Classe
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string AnnoScolastico { get; set; }
        }
    }
}