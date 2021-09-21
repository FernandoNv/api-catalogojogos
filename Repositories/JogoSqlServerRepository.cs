
using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExemploApiCatalogoJogos.Repositories
{
    public class JogoSqlServerRepository : IJogoRepository
    {
        private readonly NpgsqlConnection npgsqlConnection;

        public JogoSqlServerRepository(IConfiguration configuration)
        {
            npgsqlConnection = new NpgsqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            var jogos = new List<Jogo>();

            // var comando = $"select * from Jogo order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";
            var comando = $"SELECT * FROM jogo ORDER BY id LIMIT {quantidade} OFFSET {((pagina - 1) * quantidade)}";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand sqlCommand = new NpgsqlCommand(comando, npgsqlConnection);
            NpgsqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await npgsqlConnection.CloseAsync();

            return jogos;
        }

        public async Task<Jogo> Obter(Guid id)
        {
            Jogo jogo = null;
            var comando = $"SELECT * FROM jogo WHERE id = '{id}'";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand sqlCommand = new NpgsqlCommand(comando, npgsqlConnection);
            NpgsqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogo = new Jogo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = (double)sqlDataReader["Preco"]
                };
            }

            await npgsqlConnection.CloseAsync();

            return jogo;
        }

        public async Task<List<Jogo>> Obter(string nome, string produtora)
        {
            var jogos = new List<Jogo>();

            var comando = $"SELECT * FROM jogo WHERE nome = '{nome}' and produtora = '{produtora}'";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand sqlCommand = new NpgsqlCommand(comando, npgsqlConnection);
            NpgsqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await npgsqlConnection.CloseAsync();

            return jogos;
        }

        public async Task Inserir(Jogo jogo)
        {
            var comando = $"INSERT INTO jogo(id, nome, produtora, preco) VALUES('{jogo.Id}', '{jogo.Nome}', '{jogo.Produtora}', {jogo.Preco.ToString().Replace(",", ".")})";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(comando, npgsqlConnection);
            npgsqlCommand.ExecuteNonQuery();
            await npgsqlConnection.CloseAsync();
        }

        public async Task Atualizar(Jogo jogo)
        {
            var comando = $"update Jogo set Nome = '{jogo.Nome}', Produtora = '{jogo.Produtora}', Preco = {jogo.Preco.ToString().Replace(",", ".")} where Id = '{jogo.Id}'";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(comando, npgsqlConnection);
            npgsqlCommand.ExecuteNonQuery();
            await npgsqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Jogo where Id = '{id}'";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(comando, npgsqlConnection);
            npgsqlCommand.ExecuteNonQuery();
            await npgsqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            npgsqlConnection?.Close();
            npgsqlConnection?.Dispose();
        }
    }
}
