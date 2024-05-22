using System.Data.SqlClient;

namespace stazioneMeteoBlazor.Dati.Stazioni
{
    public class StazioniQuery
    {
        private readonly string _conn;

        public StazioniQuery(string conn)
        {
            _conn = conn;
        }

        public Stazioni ottieneStazioniById(string id)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT idNomeStazione, coalesce(IP_Statico, '') as IpStatico," +
                    "coalesce(Localita_Indirizzo, '') as Indirizzo, coalesce(Latitudine, 0) as Latitudine," +
                    "coalesce(Longitudine, 0) as Longitudine, coalesce(Altitudine, 0) as Altitudine, " +
                    "coalesce(Descrizione, '') as Descrizione, coalesce(Note, '') as Note" +
                    " FROM Stazioni WHERE idNomeStazione = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Stazioni
                    {
                        Id = (string)reader["idNomeStazione"],
                        Ip = (string)reader["IpStatico"],
                        Indirizzo = (string)reader["Indirizzo"],
                        Latitudine = (decimal)reader["Latitudine"],
                        Longitudine = (decimal)reader["Longitudine"],
                        Altitudine = (int)reader["Altitudine"],
                        Descrizione = (string)reader["Descrizione"],
                        Note = (string)reader["Note"]
                    };
                }
                return null;
            }
        }

        public List<Stazioni> OttieneStazioni()
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT idNomeStazione, coalesce(IP_Statico, '') as IpStatico," +
                    "coalesce(Localita_Indirizzo, '') as Indirizzo, coalesce(Latitudine, 0) as Latitudine," +
                    "coalesce(Longitudine, 0) as Longitudine, coalesce(Altitudine, 0) as Altitudine," +
                    "coalesce(Descrizione, '') as Descrizione, coalesce(Note, '') as Note " +
                    "FROM Stazioni", conn);
                var reader = cmd.ExecuteReader();
                var stazioni = new List<Stazioni>();
                while (reader.Read())
                {
                    stazioni.Add(new Stazioni
                    {
                        Id = (string)reader["idNomeStazione"],
                        Ip = (string)reader["IpStatico"],
                        Indirizzo = (string)reader["Indirizzo"],
                        Latitudine = (decimal)reader["Latitudine"],
                        Longitudine = (decimal)reader["Longitudine"],
                        Altitudine = (int)reader["Altitudine"],
                        Descrizione = (string)reader["Descrizione"],
                        Note = (string)reader["Note"]
                    });
                }
                return stazioni;
            }
        }

        public void ModificaStazioni(Stazioni stazione)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand(
                "UPDATE Stazioni SET " +
                "IP_Statico = @Ip, Localita_Indirizzo = @Indirizzo, Latitudine = @Latitudine, Longitudine = @Longitudine," +
                "Altitudine = @Altitudine, Descrizione = @Descrizione, Note = @Note " +
                "WHERE idNomeStazione = @id", conn);
                cmd.Parameters.AddWithValue("@Ip", stazione.Ip);
                cmd.Parameters.AddWithValue("@Id", stazione.Id);
                cmd.Parameters.AddWithValue("@Indirizzo", stazione.Indirizzo);
                cmd.Parameters.AddWithValue("@Latitudine", stazione.Latitudine);
                cmd.Parameters.AddWithValue("@Longitudine", stazione.Longitudine);
                cmd.Parameters.AddWithValue("@Altitudine", stazione.Altitudine);
                cmd.Parameters.AddWithValue("@Descrizione", stazione.Descrizione);
                cmd.Parameters.AddWithValue("@Note", stazione.Note);
                cmd.ExecuteNonQuery();
            }
        }
        

        public void EliminaStazione(string id)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM SensoriInstallati WHERE idNomeStazione = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DELETE FROM Stazioni WHERE idNomeStazione = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void CreaStazione(Stazioni stazione)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Stazioni (idNomeStazione, IP_Statico, Localita_Indirizzo, " +
                    "Latitudine, Longitudine, Altitudine, Descrizione, Note) " +
                    "VALUES (@Id, @Ip, @Indirizzo, @Latitudine, @Longitudine, @Altitudine, @Descrizione, @Note)", conn);
                cmd.Parameters.AddWithValue("@Ip", stazione.Ip);
                cmd.Parameters.AddWithValue("@Id", stazione.Id);
                cmd.Parameters.AddWithValue("@Indirizzo", stazione.Indirizzo);
                cmd.Parameters.AddWithValue("@Latitudine", stazione.Latitudine);
                cmd.Parameters.AddWithValue("@Longitudine", stazione.Longitudine);
                cmd.Parameters.AddWithValue("@Altitudine", stazione.Altitudine);
                cmd.Parameters.AddWithValue("@Descrizione", stazione.Descrizione);
                cmd.Parameters.AddWithValue("@Note", stazione.Note);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
