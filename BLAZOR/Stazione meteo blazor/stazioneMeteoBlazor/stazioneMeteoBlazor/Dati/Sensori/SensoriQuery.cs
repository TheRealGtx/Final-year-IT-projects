using stazioneMeteoBlazor.Dati.GrandezzaFisica;
using System.Data.SqlClient;

namespace stazioneMeteoBlazor.Dati.Sensori
{
    public class SensoriQuery
    {
        private readonly string _conn;

        public SensoriQuery(string conn)
        {
            _conn = conn;
        }

        public Sensori ottieneSensoriById(string id)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Sensori WHERE idCodiceSensore = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Sensori
                    {
                        Id = (int)reader["idCodiceSensore"],
                        Nome = (string)reader["Nome"],
                        Tipo = (string)reader["Tipo"],
                        grandezzaFisica = OttieniGrandezzeFisicheByIdSensore(id),
                        camera = (byte)reader["Camera"],
                        caratteristiche = (string)reader["Caratteristiche"],
                        note = (string)reader["Note"]
                    };
                }
                return null;
            }
        }

        public List<Sensori> OttieneSensori()
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Sensori", conn);
                var reader = cmd.ExecuteReader();
                var sensori = new List<Sensori>();
                while (reader.Read())
                {
                    Sensori sens = new Sensori();
                    sens.Id = (int)reader["idCodiceSensore"];
                    sens.Nome = (string)reader["Nome"];
                    sens.Tipo = (string)reader["Tipo"];
                    sens.grandezzaFisica = OttieniGrandezzeFisicheByIdSensore(sens.Id.ToString());
                    sens.camera = (byte)reader["Camera"];
                    sens.caratteristiche = (string)reader["Caratteristiche"];
                    sens.note = (string)reader["Note"];

                    sensori.Add(sens);
                }
                return sensori;
            }
        }

        public void ModificaSensori(Sensori sensore)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand(
                "UPDATE Sensori SET " +
                "idGrandezzaFisica = @grandezzaFisica, Camera = @camera, Nome = @nome, Tipo = @tipo, Caratteristiche = @caratteristiche, note = @note" +
                " WHERE idCodiceSensore = @id", conn);
                cmd.Parameters.AddWithValue("@id", sensore.Id);
                cmd.Parameters.AddWithValue("@grandezzaFisica",OttieneIDGrandezzaFisicaByNomeGrandezza(sensore.grandezzaFisica));
                cmd.Parameters.AddWithValue("@camera", sensore.camera);
                cmd.Parameters.AddWithValue("@nome", sensore.Nome);
                cmd.Parameters.AddWithValue("@tipo", sensore.Tipo);
                cmd.Parameters.AddWithValue("@caratteristiche", sensore.caratteristiche);
                cmd.Parameters.AddWithValue("@note", sensore.note);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminaSensore(string id)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM SensoriInstallati WHERE idCodiceSensore = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DELETE FROM Sensori WHERE idCodiceSensore = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void CreaSensore(Sensori sensore)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Sensori (idGrandezzaFisica, Camera, Nome, Tipo, Caratteristiche, note) " +
                                         "VALUES (@idGrandezzaFisica, @Camera, @Nome, @Tipo, @Caratteristiche, @note)", conn);
                cmd.Parameters.AddWithValue("@idGrandezzaFisica", OttieneIDGrandezzaFisicaByNomeGrandezza(sensore.grandezzaFisica));
                cmd.Parameters.AddWithValue("@Camera", sensore.camera);
                cmd.Parameters.AddWithValue("@Nome", sensore.Nome);
                cmd.Parameters.AddWithValue("@Tipo", sensore.Tipo);
                cmd.Parameters.AddWithValue("@Caratteristiche", sensore.caratteristiche);
                cmd.Parameters.AddWithValue("@note", sensore.note);
                cmd.ExecuteNonQuery();
            }
        }

        public string OttieniGrandezzeFisicheByIdSensore(string id)
        {
            string grandezza = null;
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT GrandezzaFisica FROM GrandezzaFisica where idGrandezzaFisica = (SELECT idGrandezzaFisica FROM Sensori WHERE idCodiceSensore = @id)", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    grandezza = (string)reader["GrandezzaFisica"];
                }
                return grandezza;
            }
        }

        public List<string> OttieniGrandezzeFisiche()
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT DISTINCT GrandezzaFisica FROM GrandezzaFisica", conn);
                var reader = cmd.ExecuteReader();
                var grandezze = new List<string>();
                while (reader.Read())
                {
                    grandezze.Add((string)reader["GrandezzaFisica"]);
                }
                return grandezze;
            }
        }

        public int OttieneIDGrandezzaFisicaByNomeGrandezza(string nome)
        {
            int id = 0;
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT idGrandezzaFisica FROM GrandezzaFisica WHERE GrandezzaFisica = @nome", conn);
                cmd.Parameters.AddWithValue("@nome", nome);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    id = (int)reader["idGrandezzaFisica"];
                }
                return id;
            }
        }

        public List<string> OttieneTipoSensore()
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT DISTINCT Tipo FROM Sensori", conn);
                var reader = cmd.ExecuteReader();
                var sensori = new List<string>();
                while (reader.Read())
                {
                    sensori.Add((string)reader["Tipo"]);
                }
                return sensori;
            }
        }
    }
}