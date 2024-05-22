using System.Data.SqlClient;

namespace stazioneMeteoBlazor.Dati.GrandezzaFisica
{
    public class GrandezzaFisicaQuery
    {
        private readonly string _conn;

        public GrandezzaFisicaQuery(string conn)
        {
            _conn = conn;
        }

        public GrandezzaFisica ottieneGrandezzaById(string id)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM GrandezzaFisica WHERE idGrandezzaFisica = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new GrandezzaFisica
                    {
                        Id = (int)reader["idGrandezzaFisica"],
                        grandezzaFisica = (string)reader["GrandezzaFisica"],
                        simbolo = (string)reader["Simbolo"],
                        simboloAdottato = (string)reader["SimboloUnitaDiMisuraAdottato"],
                        note = (string)reader["Note"]
                    };
                }
                return null;
            }
        }

        public List<GrandezzaFisica> OttieneGrandezzaFisica()
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM GrandezzaFisica", conn);
                var reader = cmd.ExecuteReader();
                var grandezza = new List<GrandezzaFisica>();
                while (reader.Read())
                {
                    grandezza.Add(new GrandezzaFisica
                    {
                        Id = (int)reader["idGrandezzaFisica"],
                        grandezzaFisica = (string)reader["GrandezzaFisica"],
                        simbolo = (string)reader["Simbolo"],
                        simboloAdottato = (string)reader["SimboloUnitaDiMisuraAdottato"],
                        note = (string)reader["Note"]
                    });
                }
                return grandezza;
            }
        }

        public void ModificaGrandezzaFisica(GrandezzaFisica grandezza)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand(
                "UPDATE GrandezzaFisica SET " +
                "GrandezzaFisica = @grandezzaFisica, Simbolo = @simbolo, SimboloUnitaDiMisuraAdottato = @simboloAdottato, note = @note" +
                " WHERE idGrandezzaFisica = @id", conn);
                cmd.Parameters.AddWithValue("@id", grandezza.Id);
                cmd.Parameters.AddWithValue("@grandezzaFisica", grandezza.grandezzaFisica);
                cmd.Parameters.AddWithValue("@simbolo", grandezza.simbolo);
                cmd.Parameters.AddWithValue("@simboloAdottato", grandezza.simboloAdottato);
                cmd.Parameters.AddWithValue("@note", grandezza.note);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminaGrandezzaFisica(string id)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Sensori WHERE idGrandezzaFisica = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DELETE FROM GrandezzaFisica WHERE idGrandezzaFisica = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void CreaGrandezzaFisica(GrandezzaFisica grandezza)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO GrandezzaFisica (GrandezzaFisica, Simbolo, SimboloUnitaDiMisuraAdottato, Note) " +
                                         "VALUES (@GrandezzaFisica, @Simbolo, @SimboloAdottato, @note)", conn);
                cmd.Parameters.AddWithValue("@GrandezzaFisica", grandezza.grandezzaFisica);
                cmd.Parameters.AddWithValue("@Simbolo", grandezza.simbolo);
                cmd.Parameters.AddWithValue("@SimboloAdottato", grandezza.simboloAdottato);
                cmd.Parameters.AddWithValue("@note", grandezza.note);
                cmd.ExecuteNonQuery();
            }
        }
    }
}