using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entidade;


namespace DataAccess
{
    public class DAArtista
    {
        private MySqlConnection _connection = null;



        public DAArtista()
        {
            _connection = Core.GetConnection();
        }

        public int Inserir(Artista artista)
        {
            MySqlCommand command = null;
            try
            {
                command = new MySqlCommand(Core.SCHEMA + ".INCLUIR_ARTISTA", _connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("P_ID_ARTISTA", MySqlDbType.Int32);
                command.Parameters["P_ID_ARTISTA"].Direction = System.Data.ParameterDirection.Output;

                command.Parameters.Add("P_OK", MySqlDbType.String);
                command.Parameters["P_OK"].Direction = System.Data.ParameterDirection.Output;

                command.Parameters.Add("P_RETORNO", MySqlDbType.String);
                command.Parameters["P_RETORNO"].Direction = System.Data.ParameterDirection.Output;

                command.Parameters.Add("P_COMMIT", MySqlDbType.String);
                command.Parameters["P_COMMIT"].Value = "S";

                command.Parameters.Add("P_ART_NOME", MySqlDbType.String);
                command.Parameters["P_ART_NOME"].Value = artista.Nome;

                command.Parameters.Add("P_ART_LINK_FOTO", MySqlDbType.String);
                command.Parameters["P_ART_LINK_FOTO"].Value = artista.LinkFoto;

                _connection.Open();

                command.Transaction = _connection.BeginTransaction();

                command.ExecuteNonQuery();

                command.Transaction.Commit();

                artista.Id = Convert.ToInt32(command.Parameters["P_ID_ARTISTA"].Value);

            }
            catch (Exception e)
            {
                command.Transaction.Rollback();
                throw e;
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }

            

            return artista.Id;
        }



        public void InserirArtistaArray(String artistas)
        {
            MySqlCommand command = null;
            try
            {
                command = new MySqlCommand(Core.SCHEMA + ".INCLUIR_ARTISTA_ARRAY", _connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                                
                command.Parameters.Add("P_OK", MySqlDbType.String);
                command.Parameters["P_OK"].Direction = System.Data.ParameterDirection.Output;

                command.Parameters.Add("P_RETORNO", MySqlDbType.String);
                command.Parameters["P_RETORNO"].Direction = System.Data.ParameterDirection.Output;

                command.Parameters.Add("P_COMMIT", MySqlDbType.String);
                command.Parameters["P_COMMIT"].Value = "S";

                command.Parameters.Add("P_ARTISTA", MySqlDbType.String);
                command.Parameters["P_ARTISTA"].Value = artistas;

                command.CommandTimeout = 0;

                _connection.Open();

                command.Transaction = _connection.BeginTransaction();

                command.ExecuteNonQuery();

                command.Transaction.Commit();

            }
            catch (Exception e)
            {
                command.Transaction.Rollback();
                throw e;
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
            
        }

    }
}
