using stazioneMeteoBlazor.Dati.Rilevazioni;
using stazioneMeteoBlazor.Dati.Sensori;
using stazioneMeteoBlazor.Dati.Stazioni;
using System.Data.SqlClient;

namespace stazioneMeteoBlazor.Dati.SensoriInstallati
{
    public class SensoriInstallatiQuery
    {
        private readonly string _conn;

        public SensoriInstallatiQuery(string conn)
        {
            _conn = conn;
        }

        public SensoriInstallati ottieneInstallazioniById(string id)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT idSensoriInstallati, idNomeStazione, coalesce(Note, '') as Note FROM SensoriInstallati WHERE idSensoriInstallati = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    SensoriInstallati sens = new SensoriInstallati();
                    sens.Id = (int)reader["idSensoriInstallati"];
                    sens.Sensore = OttieneNomeSensoreByIdSensoreInstallato(sens.Id.ToString());
                    sens.Stazione = (string)reader["idNomeStazione"];
                    sens.Note = (string)reader["Note"];

                    return sens;
                }
                return null;
            }
        }

        public List<SensoriInstallati> OttieneInstallazioni()
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT idSensoriInstallati, idCodiceSensore, idNomeStazione, coalesce(Note, '') as Note FROM SensoriInstallati", conn);
                var reader = cmd.ExecuteReader();
                var installazioni = new List<SensoriInstallati>();
                while (reader.Read())
                {
                    SensoriInstallati sens = new SensoriInstallati();

                    sens.Id = (int)reader["idSensoriInstallati"];
                    sens.Sensore = OttieneNomeSensoreByIdSensoreInstallato(sens.Id.ToString());
                    sens.Stazione = (string)reader["idNomeStazione"];
                    sens.Note = (string)reader["Note"];
                    installazioni.Add(sens);
                }
                return installazioni;
            }
        }

        public void ModificaInstallazioni(SensoriInstallati installazione)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand(
                "UPDATE SensoriInstallati SET " +
                "idCodiceSensore = @sensore, idNomeStazione = @stazione, Note = @note" +
                " WHERE idSensoriInstallati = @id", conn);
                cmd.Parameters.AddWithValue("@stazione", installazione.Stazione);
                cmd.Parameters.AddWithValue("@sensore", OttieneCodiceSensoreByNome(installazione.Sensore));
                cmd.Parameters.AddWithValue("@note", installazione.Note);
                cmd.Parameters.AddWithValue("@id", installazione.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminaInstallazione(string id)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM SensoriInstallati WHERE idSensoriInstallati = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void CreaInstallazione(SensoriInstallati installazione)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO SensoriInstallati (idCodiceSensore, idNomeStazione, Note) " +
                                         "VALUES (@sensore, @stazione, @note)", conn);
                cmd.Parameters.AddWithValue("@sensore", OttieneCodiceSensoreByNome(installazione.Sensore));
                cmd.Parameters.AddWithValue("@stazione", installazione.Stazione);
                cmd.Parameters.AddWithValue("@note", installazione.Note);
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> OttieneSensori()
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Distinct Nome FROM Sensori", conn);
                var reader = cmd.ExecuteReader();
                var sensori = new List<string>();
                while (reader.Read())
                {
                    sensori.Add((string)reader["Nome"]);
                }
                return sensori;
            }
        }

        public List<string> ottieneStazioni()
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Distinct idNomeStazione FROM Stazioni", conn);
                var reader = cmd.ExecuteReader();
                var stazioni = new List<string>();
                while (reader.Read())
                {
                    stazioni.Add((string)reader["idNomeStazione"]);
                }
                return stazioni;
            }
        }

        public string OttieneNomeSensoreByIdSensoreInstallato(string id)
        {
            string nomeSensore = null;

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Nome FROM Sensori WHERE idCodiceSensore = (SELECT idCodiceSensore FROM SensoriInstallati WHERE idSensoriInstallati = @id)", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    nomeSensore = (string)reader["Nome"];
                }
                else
                {
                    nomeSensore = null;
                }
                return nomeSensore;
            }
        }

        public int OttieneCodiceSensoreByNome(string nome)
        {
            int codice = 0;

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT idCodiceSensore FROM Sensori s WHERE Nome = @nome", conn);
                cmd.Parameters.AddWithValue("@nome", nome);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    codice = (int)reader["idCodiceSensore"];
                }
                return codice;
            }
        }
    }
}
