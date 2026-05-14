using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;


namespace Pasticceria
{
    public partial class Form1 : Form
    {
        // Lista che contiene gli ingredienti attualmente presenti nella dispensa
        List<string> dispensa = new List<string>()
        {
            "Uova",
            "Farina",
            "Zucchero",
            "Burro"
        };

        // Lista che conterrà gli ingredienti da comprare dopo aver analizzato un ordine
        List<string> listaSpesa = new List<string>();

        Dictionary<string, List<string>> ingredientiDolci = new Dictionary<string, List<string>>()
        {
            { "Tiramisù", new List<string>() { "Mascarpone", "Uova", "Caffè", "Zucchero", "Cacao" } },
            { "Cannolo Siciliano", new List<string>() { "Ricotta", "Zucchero", "Cialda fritta", "Pistacchi", "Zucchero a velo" } },
            { "Panna Cotta", new List<string>() { "Panna", "Zucchero", "Gelatina", "Frutti di bosco" } },
            { "Brownies", new List<string>() { "Cioccolato", "Burro", "Zucchero", "Uova", "Farina", "Zucchero a velo" } },
            { "Cheesecake", new List<string>() { "Biscotti secchi", "Burro", "Formaggio spalmabile", "Zucchero", "Lamponi" } },
            { "Waffle", new List<string>() { "Farina", "Uova", "Latte", "Burro", "Zucchero", "Frutti di bosco" } },
            { "Gelato", new List<string>() { "Latte", "Panna", "Zucchero", "Cialda" } },
            { "Éclair", new List<string>() { "Acqua", "Burro", "Farina", "Uova", "Cioccolato" } },
            { "Torta di mele", new List<string>() { "Mele", "Farina", "Zucchero", "Burro", "Uova", "Lievito" } }
        };

        // Costruttore della classe Form1
        public Form1()
        {
            InitializeComponent(); // Inizializza i componenti grafici del form
            AggiornaDispensa(); // Mostra subito gli ingredienti iniziali nella ListBox
        }

        // Classe per mappare la struttura di un singolo prodotto dentro il file JSON
        public class Prodotto
        {
            public int id { get; set; }
            public string nome { get; set; }
            public int quantita { get; set; }
            public double prezzo { get; set; }
        }

        // Classe per mappare l'intero file JSON di un ordine
        public class Ordine
        {
            public string dataOrdine { get; set; }
            public string negozio { get; set; }
            public List<Prodotto> prodotti { get; set; }
            public string totale { get; set; }
        }

        // Metodo eseguito al click del tasto "Carica"
        private void buttonCarica_Click(object sender, EventArgs e)
        {
            // Ottiene il percorso della cartella "Download" dell'utente corrente
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            var directory = new DirectoryInfo(folderPath);

            // Cerca tutti i file .json e seleziona quello modificato più recentemente
            var filePiuRecente = directory.GetFiles("*.json")
                                          .OrderByDescending(f => f.LastWriteTime)
                                          .FirstOrDefault();

            // Se non trova file, avvisa l'utente e interrompe l'esecuzione
            if (filePiuRecente == null)
            {
                labelMessaggio.Text = "Nessun file JSON trovato nei Download!";
                return;
            }

            try
            {
                // Legge tutto il contenuto del file JSON come testo
                string json = File.ReadAllText(filePiuRecente.FullName);
                // Converte il testo JSON in un oggetto di classe "Ordine"
                Ordine ordine = JsonSerializer.Deserialize<Ordine>(json);

                // Pulisce le liste grafiche e la lista della spesa interna
                listBoxOrdini.Items.Clear();
                listBoxSpesa.Items.Clear();
                listaSpesa.Clear();

                // Crea una copia temporanea della dispensa per simulare il consumo senza intaccare quella reale subito
                List<string> dispensaTemporanea = new List<string>(dispensa);

                // Cicla ogni dolce presente nell'ordine appena caricato
                foreach (var prodotto in ordine.prodotti)
                {
                    // Aggiunge il nome, la quantità e la ricetta del dolce alla ListBox a video 
                    listBoxOrdini.Items.Add($"{prodotto.nome} x{prodotto.quantita}");

                    // Se esiste la ricetta, la mostro sotto
                    if (ingredientiDolci.ContainsKey(prodotto.nome))
                    {
                        listBoxOrdini.Items.Add("  Ricetta:");

                        foreach (var ingrediente in ingredientiDolci[prodotto.nome])
                        {
                            listBoxOrdini.Items.Add($"    - {ingrediente}");
                        }
                    }

                    // Se il dolce caricato esiste nel ricettario
                    if (ingredientiDolci.ContainsKey(prodotto.nome))
                    {
                        // Esegue il calcolo per ogni singola unità ordinata
                        for (int i = 0; i < prodotto.quantita; i++)
                        {
                            // Cicla ogni ingrediente necessario per quel dolce
                            foreach (var ingrediente in ingredientiDolci[prodotto.nome])
                            {
                                // Se l'ingrediente è in dispensa, lo rimuove dalla copia temporanea
                                if (dispensaTemporanea.Contains(ingrediente))
                                {
                                    dispensaTemporanea.Remove(ingrediente);
                                }
                                else
                                {
                                    // Altrimenti lo aggiunge alla lista della spesa
                                    listaSpesa.Add(ingrediente);
                                }
                            }
                        }
                    }
                }

                // Raggruppa gli ingredienti uguali nella lista della spesa
                var spesaRaggruppata = listaSpesa
                    .GroupBy(ing => ing)
                    .Select(gruppo => new { Nome = gruppo.Key, Conteggio = gruppo.Count() });

                // Cicla i risultati raggruppati per riempire la ListBox della spesa
                foreach (var voce in spesaRaggruppata)
                {
                    if (voce.Conteggio > 1)
                    {
                        listBoxSpesa.Items.Add($"{voce.Nome} x{voce.Conteggio}");
                    }
                    else
                    {
                        listBoxSpesa.Items.Add(voce.Nome);
                    }
                }

                labelMessaggio.Text = "Ordine caricato e spesa calcolata!";
            }
            catch (Exception ex)
            {
                // Gestisce eventuali errori
                MessageBox.Show("Errore nel caricamento del file: " + ex.Message);
            }
        }

        // Metodo per aggiornare visivamente la ListBox della dispensa
        private void AggiornaDispensa()
        {
            listBoxDispensa.Items.Clear();

            // Raggruppa gli ingredienti identici
            var dispensaRaggruppata = dispensa
                .GroupBy(i => i)
                .Select(g => new { Nome = g.Key, Quantita = g.Count() });

            foreach (var voce in dispensaRaggruppata)
            {
                listBoxDispensa.Items.Add($"{voce.Nome} x{voce.Quantita}");
            }
        }

        // Metodo eseguito al click del tasto "Compra"
        private void buttonCompra_Click(object sender, EventArgs e)
        {
            // Se non c'è nulla da comprare, avvisa ed esce
            if (listaSpesa.Count == 0)
            {
                labelMessaggio.Text = "La lista della spesa è vuota!";
                return;
            }

            // Aggiunge tutti gli ingredienti comprati alla dispensa effettiva
            foreach (var ingrediente in listaSpesa)
            {
                dispensa.Add(ingrediente);
            }

            // Svuota la lista della spesa e aggiorna la grafica
            listaSpesa.Clear();
            listBoxSpesa.Items.Clear();
            AggiornaDispensa();

            labelMessaggio.Text = "Spesa effettuata! Ingredienti pronti in dispensa.";
        }

        // Metodo eseguito al click del tasto "Cucina"
        private void buttonCucina_Click(object sender, EventArgs e)
        {
            // Impedisce di cucinare se mancano ingredienti
            if (listaSpesa.Count > 0)
            {
                labelMessaggio.Text = "Mancano ingredienti! Controlla la lista della spesa.";
                return;
            }

            // Impedisce di cucinare se non è stato caricato nessun ordine
            if (listBoxOrdini.Items.Count == 0)
            {
                labelMessaggio.Text = "Nessun ordine da cucinare!";
                return;
            }

            // Cicla ogni riga dell'ordine visualizzato nella ListBox
            foreach (string riga in listBoxOrdini.Items)
            {
                // Divide la stringa "NomeDolce xQuantità" per isolare nome e numero
                string[] parti = riga.Split(new string[] { " x" }, StringSplitOptions.None);

                if (parti.Length < 2) continue;

                string nomeDolce = parti[0];

                // Prova a convertire la parte dopo la 'x' in un numero intero
                if (int.TryParse(parti[1], out int quantita))
                {
                    // Se il dolce è nel ricettario
                    if (ingredientiDolci.ContainsKey(nomeDolce))
                    {
                        // Ripete per il numero di dolci ordinati
                        for (int i = 0; i < quantita; i++)
                        {
                            // Rimuove fisicamente gli ingredienti dalla dispensa reale
                            foreach (var ingrediente in ingredientiDolci[nomeDolce])
                            {
                                dispensa.Remove(ingrediente);
                            }
                        }
                    }
                }
            }

            // Svuota l'ordine completato e aggiorna la dispensa a video
            listBoxOrdini.Items.Clear();
            AggiornaDispensa();

            labelMessaggio.Text = "Dolci cucinati con successo!";
        }
        private void listBoxDispensa_SelectedIndexChanged(object sender, EventArgs e) { }
        private void labelMessaggio_Click(object sender, EventArgs e) { }

        private void listBoxOrdini_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}