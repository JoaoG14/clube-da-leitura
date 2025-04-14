using System;
using System.Collections.Generic;
using ClubeDaLeitura.ConsoleApp.Modulos.Emprestimos;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Amigos
{
    public class Amigo
    {
        public string Nome { get; set; }
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }
        
        public Amigo(string nome, string nomeResponsavel, string telefone)
        {
            Nome = nome;
            NomeResponsavel = nomeResponsavel;
            Telefone = telefone;
        }
        
        public bool Validar()
        {
            bool nomeValido = Nome != null && Nome.Length >= 3 && Nome.Length <= 100;
            bool responsavelValido = NomeResponsavel != null && NomeResponsavel.Length >= 3 && NomeResponsavel.Length <= 100;
            
            // Validação de telefone com regex simples para os formatos (XX) XXXX-XXXX ou (XX) XXXXX-XXXX
            bool telefoneValido = System.Text.RegularExpressions.Regex.IsMatch(
                Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$");
            
            return nomeValido && responsavelValido && telefoneValido;
        }
        
        public List<Emprestimo> ObterEmprestimos(List<Emprestimo> todosEmprestimos)
        {
            List<Emprestimo> emprestimosDoAmigo = new List<Emprestimo>();
            
            foreach (Emprestimo emprestimo in todosEmprestimos)
            {
                if (emprestimo.Amigo == this)
                {
                    emprestimosDoAmigo.Add(emprestimo);
                }
            }
            
            return emprestimosDoAmigo;
        }
    }
} 