using System;
using System.Collections.Generic;
using ClubeDaLeitura.ConsoleApp.Modulos.Revistas;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Caixas
{
    public class Caixa
    {
        public string Etiqueta { get; set; }
        public string Cor { get; set; }
        public int DiasEmprestimo { get; set; }
        
        public Caixa(string etiqueta, string cor, int diasEmprestimo)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DiasEmprestimo = diasEmprestimo;
        }
        
        public bool Validar()
        {
            bool etiquetaValida = !string.IsNullOrEmpty(Etiqueta) && Etiqueta.Length <= 50;
            bool corValida = !string.IsNullOrEmpty(Cor);
            bool diasEmprestimoValidos = DiasEmprestimo > 0 && DiasEmprestimo <= 30; // Limite arbitrário de 30 dias
            
            return etiquetaValida && corValida && diasEmprestimoValidos;
        }
        
        public void AdicionarRevista(Revista revista)
        {
            revista.Caixa = this;
        }
        
        public void RemoverRevista(Revista revista)
        {
            if (revista.Caixa == this)
            {
                revista.Caixa = null;
            }
        }
    }
} 