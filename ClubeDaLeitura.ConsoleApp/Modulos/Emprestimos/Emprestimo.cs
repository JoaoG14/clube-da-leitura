using System;
using ClubeDaLeitura.ConsoleApp.Modulos.Amigos;
using ClubeDaLeitura.ConsoleApp.Modulos.Revistas;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Emprestimos
{
    public enum StatusEmprestimo
    {
        Aberto,
        Concluido,
        Atrasado
    }
    
    public class Emprestimo
    {
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataEmprestimo { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public StatusEmprestimo Situacao { get; private set; }
        
        public Emprestimo(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = DateTime.Now;
            DataDevolucao = DataEmprestimo.AddDays(revista.Caixa.DiasEmprestimo);
            Situacao = StatusEmprestimo.Aberto;
            
            revista.Emprestar();
        }
        
        public bool Validar()
        {
            bool amigoValido = Amigo != null;
            bool revistaValida = Revista != null && Revista.Status == StatusRevista.Emprestada;
            
            return amigoValido && revistaValida;
        }
        
        public DateTime ObterDataDevolucao()
        {
            return DataDevolucao;
        }
        
        public void RegistrarDevolucao()
        {
            if (Situacao != StatusEmprestimo.Concluido)
            {
                Situacao = StatusEmprestimo.Concluido;
                Revista.Devolver();
            }
            else
            {
                throw new InvalidOperationException("Este empréstimo já foi concluído.");
            }
        }
        
        public void AtualizarStatus()
        {
            if (Situacao == StatusEmprestimo.Aberto && DateTime.Now > DataDevolucao)
            {
                Situacao = StatusEmprestimo.Atrasado;
            }
        }
    }
} 