using System;
using System.Collections.Generic;
using ClubeDaLeitura.ConsoleApp.Modulos.Emprestimos;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Amigos
{
    public class RepositorioAmigo
    {
        private List<Amigo> amigos;
        
        public RepositorioAmigo()
        {
            amigos = new List<Amigo>();
        }
        
        public void Inserir(Amigo amigo)
        {
            amigos.Add(amigo);
        }
        
        public void Editar(int indice, Amigo amigo)
        {
            amigos[indice] = amigo;
        }
        
        public void Excluir(int indice)
        {
            amigos.RemoveAt(indice);
        }
        
        public Amigo SelecionarPorId(int indice)
        {
            return amigos[indice];
        }
        
        public List<Amigo> SelecionarTodos()
        {
            return amigos;
        }
        
        public int ObterQuantidadeAmigos()
        {
            return amigos.Count;
        }
        
        public bool AmigoJaExiste(Amigo amigo)
        {
            foreach (Amigo a in amigos)
            {
                if (a.Nome == amigo.Nome && a.Telefone == amigo.Telefone)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public bool PodeExcluir(Amigo amigo, List<Emprestimo> emprestimos)
        {
            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (emprestimo.Amigo == amigo)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
} 