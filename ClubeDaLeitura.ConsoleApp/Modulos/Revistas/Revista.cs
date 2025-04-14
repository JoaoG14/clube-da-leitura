using System;
using ClubeDaLeitura.ConsoleApp.Modulos.Caixas;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Revistas
{
    public enum StatusRevista
    {
        Disponivel,
        Emprestada,
        Reservada
    }
    
    public class Revista
    {
        public string Titulo { get; set; }
        public int NumeroEdicao { get; set; }
        public DateTime AnoPublicacao { get; set; }
        public StatusRevista Status { get; set; }
        public Caixa Caixa { get; set; }
        
        public Revista(string titulo, int numeroEdicao, DateTime anoPublicacao, Caixa caixa)
        {
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            AnoPublicacao = anoPublicacao;
            Caixa = caixa;
            Status = StatusRevista.Disponivel; // Status padrão ao cadastrar
        }
        
        public bool Validar()
        {
            bool tituloValido = !string.IsNullOrEmpty(Titulo) && Titulo.Length >= 2 && Titulo.Length <= 100;
            bool edicaoValida = NumeroEdicao > 0;
            bool publicacaoValida = AnoPublicacao.Year > 1800 && AnoPublicacao.Year <= DateTime.Now.Year;
            bool caixaValida = Caixa != null;
            
            return tituloValido && edicaoValida && publicacaoValida && caixaValida;
        }
        
        public void Emprestar()
        {
            if (Status == StatusRevista.Disponivel)
            {
                Status = StatusRevista.Emprestada;
            }
            else
            {
                throw new InvalidOperationException("A revista não está disponível para empréstimo.");
            }
        }
        
        public void Devolver()
        {
            if (Status == StatusRevista.Emprestada)
            {
                Status = StatusRevista.Disponivel;
            }
            else
            {
                throw new InvalidOperationException("A revista não está emprestada.");
            }
        }
        
        public void Reservar()
        {
            if (Status == StatusRevista.Disponivel)
            {
                Status = StatusRevista.Reservada;
            }
            else
            {
                throw new InvalidOperationException("A revista não está disponível para reserva.");
            }
        }
    }
} 