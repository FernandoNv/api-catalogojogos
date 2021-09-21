using System;

namespace ApiCatalogoJogos.Exceptions
{
    public class JogoCadastradoException : Exception
    {
        public JogoCadastradoException() : base("Este jogo esta cadastrado") { }
    }
}