using System;
using System.Collections.Generic;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Revistas
{
    public class RepositorioRevista
    {
        private List<Revista> revistas;
        
        public RepositorioRevista()
        {
            revistas = new List<Revista>();
        }
        
        public void Inserir(Revista revista)
        {
            revistas.Add(revista);
        }
        
        public void Editar(int indice, Revista revista)
        {
            revistas[indice] = revista;
        }
        
        public void Excluir(int indice)
        {
            revistas.RemoveAt(indice);
        }
        
        public Revista SelecionarPorId(int indice)
        {
            return revistas[indice];
        }
        
        public List<Revista> SelecionarTodos()
        {
            return revistas;
        }
        
        public int ObterQuantidadeRevistas()
        {
            return revistas.Count;
        }
        
        public bool RevistaJaExiste(Revista revista)
        {
            foreach (Revista r in revistas)
            {
                if (r.Titulo == revista.Titulo && r.NumeroEdicao == revista.NumeroEdicao)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public bool PodeExcluir(Revista revista)
        {
            return revista.Status != StatusRevista.Emprestada;
        }
        
        public List<Revista> SelecionarPorStatus(StatusRevista status)
        {
            List<Revista> revistasFiltradas = new List<Revista>();
            
            foreach (Revista revista in revistas)
            {
                if (revista.Status == status)
                {
                    revistasFiltradas.Add(revista);
                }
            }
            
            return revistasFiltradas;
        }
    }
} 