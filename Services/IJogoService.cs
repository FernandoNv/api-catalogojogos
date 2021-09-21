using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;

namespace ApiCatalogoJogos.Services
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        Task<JogoViewModel> Obter(Guid idJogo);
        Task<JogoViewModel> Inserir(JogoInputModel jogo);
        Task Atualizar(Guid idJogo, JogoInputModel jogo);
        Task Atualizar(Guid idJogo, double preco);
        Task Remover(Guid idJogo);

    }
}