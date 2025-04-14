using System;
using System.Collections.Generic;
using ClubeDaLeitura.ConsoleApp.Modulos.Amigos;
using ClubeDaLeitura.ConsoleApp.Modulos.Revistas;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Emprestimos
{
    public class RepositorioEmprestimo
    {
        private List<Emprestimo> emprestimos;
        
        public RepositorioEmprestimo()
        {
            emprestimos = new List<Emprestimo>();
        }
        
        public void Inserir(Emprestimo emprestimo)
        {
            emprestimos.Add(emprestimo);
        }
        
        public void Editar(int indice, Emprestimo emprestimo)
        {
            emprestimos[indice] = emprestimo;
        }
        
        public void Excluir(int indice)
        {
            emprestimos.RemoveAt(indice);
        }
        
        public Emprestimo SelecionarPorId(int indice)
        {
            return emprestimos[indice];
        }
        
        public List<Emprestimo> SelecionarTodos()
        {
            return emprestimos;
        }
        
        public int ObterQuantidadeEmprestimos()
        {
            return emprestimos.Count;
        }
        
        public bool VerificarAmigoComEmprestimoAtivo(Amigo amigo)
        {
            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (emprestimo.Amigo == amigo && emprestimo.Situacao == StatusEmprestimo.Aberto)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public List<Emprestimo> SelecionarEmprestimosAbertos()
        {
            List<Emprestimo> emprestimosAbertos = new List<Emprestimo>();
            
            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (emprestimo.Situacao == StatusEmprestimo.Aberto)
                {
                    emprestimosAbertos.Add(emprestimo);
                }
            }
            
            return emprestimosAbertos;
        }
        
        public List<Emprestimo> SelecionarEmprestimosConcluidos()
        {
            List<Emprestimo> emprestimosConcluidos = new List<Emprestimo>();
            
            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (emprestimo.Situacao == StatusEmprestimo.Concluido)
                {
                    emprestimosConcluidos.Add(emprestimo);
                }
            }
            
            return emprestimosConcluidos;
        }
        
        public List<Emprestimo> SelecionarEmprestimosAtrasados()
        {
            AtualizarStatusEmprestimos();
            
            List<Emprestimo> emprestimosAtrasados = new List<Emprestimo>();
            
            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (emprestimo.Situacao == StatusEmprestimo.Atrasado)
                {
                    emprestimosAtrasados.Add(emprestimo);
                }
            }
            
            return emprestimosAtrasados;
        }
        
        private void AtualizarStatusEmprestimos()
        {
            foreach (Emprestimo emprestimo in emprestimos)
            {
                emprestimo.AtualizarStatus();
            }
        }
    }
} 