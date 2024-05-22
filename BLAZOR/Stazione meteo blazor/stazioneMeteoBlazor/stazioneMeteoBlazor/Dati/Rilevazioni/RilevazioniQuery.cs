using System.Data.SqlClient;

namespace stazioneMeteoBlazor.Dati.Rilevazioni
{
    public class RilevazioniQuery
    {
        private readonly string _conn;

        public RilevazioniQuery(string conn)
        {
            _conn = conn;
        }

        public Rilevazione ottieneRilevazioniById(string id)
        {
            using (var conn = new SqlConnection(_conn))
            {
                Rilevazione riv = new Rilevazione();

                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Rilevamenti WHERE idRilevamenti = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    riv.Id = (int)reader["idRilevamenti"];
                    riv.Data = (DateTime)reader["DataOra"];
                    riv.Rilevamento = (string)reader["Dato"];
                    riv.Sensore = OttieneNomeSensoriByIDRievazione(riv.Id);
                }
                return riv;
            }
        }

        public List<Rilevazione> OttieneRilevazioni()
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Rilevamenti", conn);
                var reader = cmd.ExecuteReader();
                var rilevazioni = new List<Rilevazione>();
                while (reader.Read())
                {
                    Rilevazione riv = new Rilevazione();
                    riv.Id = (int)reader["idRilevamenti"];
                    riv.Data = (DateTime)reader["DataOra"];
                    riv.Rilevamento = (string)reader["Dato"];
                    riv.Sensore = OttieneNomeSensoriByIDRievazione(riv.Id);

                    rilevazioni.Add(riv);
                }
                return rilevazioni;
            }
        }

        public void ModificaRilevazioni(Rilevazione dato)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand(
                "UPDATE Rilevamenti SET " +
                "idSensoriInstallati = @sensore, DataOra = @data, Dato = @dato" +
                " WHERE idRilevamenti = @id", conn);
                cmd.Parameters.AddWithValue("@id", dato.Id);
                cmd.Parameters.AddWithValue("@sensore", OttieneSensoriID(dato.Sensore));
                cmd.Parameters.AddWithValue("@data", dato.Data);
                cmd.Parameters.AddWithValue("@Dato", dato.Rilevamento);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminaRilevazione(string id)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Rilevamenti WHERE idRilevamenti = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void CreaRilevazione(Rilevazione rilevazione)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Rilevamenti (idSensoriInstallati, DataOra, Dato) " +
                                         "VALUES (@sensore, @data, @rilevamento)", conn);
                cmd.Parameters.AddWithValue("@sensore", OttieneSensoriID(rilevazione.Sensore));
                cmd.Parameters.AddWithValue("@data", rilevazione.Data);
                cmd.Parameters.AddWithValue("@rilevamento", rilevazione.Rilevamento);
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> OttieneSensori()
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT DISTINCT Nome FROM Sensori", conn);
                var reader = cmd.ExecuteReader();
                var sensori = new List<string>();
                while (reader.Read())
                {
                    sensori.Add((string)reader["Nome"]);
                }
                return sensori;

            }
        }

        public int OttieneSensoriID(string sensore)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT idCodiceSensore FROM Sensori where Nome = @sensore", conn);
                cmd.Parameters.AddWithValue("@sensore", sensore);
                var reader = cmd.ExecuteReader();
                int sensoriId = 0;
                while (reader.Read())
                {
                    sensoriId = (int)reader["idCodiceSensore"];
                }
                return sensoriId;
            }
        }

        public string OttieneNomeSensoriByIDRievazione(int id)
        {
            string sensore;
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Nome FROM Sensori WHERE idCodiceSensore = (SELECT idSensoriInstallati FROM Rilevamenti WHERE idRilevamenti = @id)", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    sensore = (string)reader["Nome"];
                }
                else
                {
                    sensore = null;
                }
                return sensore;
            }
        }
    }
}