using System;
using System.Collections.Generic;
using ClubeDaLeitura.ConsoleApp.Modulos.Revistas;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Caixas
{
    public class RepositorioCaixa
    {
        private List<Caixa> caixas;
        
        public RepositorioCaixa()
        {
            caixas = new List<Caixa>();
        }
        
        public void Inserir(Caixa caixa)
        {
            caixas.Add(caixa);
        }
        
        public void Editar(int indice, Caixa caixa)
        {
            caixas[indice] = caixa;
        }
        
        public void Excluir(int indice)
        {
            caixas.RemoveAt(indice);
        }
        
        public Caixa SelecionarPorId(int indice)
        {
            return caixas[indice];
        }
        
        public List<Caixa> SelecionarTodos()
        {
            return caixas;
        }
        
        public int ObterQuantidadeCaixas()
        {
            return caixas.Count;
        }
        
        public bool EtiquetaJaExiste(string etiqueta)
        {
            foreach (Caixa c in caixas)
            {
                if (c.Etiqueta == etiqueta)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public bool PodeExcluir(Caixa caixa, List<Revista> revistas)
        {
            foreach (Revista revista in revistas)
            {
                if (revista.Caixa == caixa)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
} 