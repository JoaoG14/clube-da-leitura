using System;
using System.Collections.Generic;
using ClubeDaLeitura.ConsoleApp.Modulos.Revistas;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Caixas
{
    public class TelaCaixa
    {
        private RepositorioCaixa repositorioCaixa;
        private List<Revista> revistas;
        
        public TelaCaixa(RepositorioCaixa repositorio, List<Revista> revistas)
        {
            repositorioCaixa = repositorio;
            this.revistas = revistas;
        }
        
        public void Inserir()
        {
            Console.Clear();
            Console.WriteLine("=== Inserir Nova Caixa ===");
            
            Caixa novaCaixa = ObterCaixa();
            
            if (novaCaixa != null)
            {
                if (repositorioCaixa.EtiquetaJaExiste(novaCaixa.Etiqueta))
                {
                    Console.WriteLine("\nUma caixa com esta etiqueta já existe!");
                    Console.ReadKey();
                    return;
                }
                
                if (novaCaixa.Validar())
                {
                    repositorioCaixa.Inserir(novaCaixa);
                    Console.WriteLine("\nCaixa inserida com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nDados inválidos. Verifique se todos os campos estão preenchidos corretamente.");
                }
            }
            
            Console.ReadKey();
        }
        
        public void Editar()
        {
            Console.Clear();
            Console.WriteLine("=== Editar Caixa ===");
            
            VisualizarTodos();
            
            Console.Write("\nDigite o ID da caixa que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());
            
            Caixa caixaAtualizada = ObterCaixa();
            
            if (caixaAtualizada != null)
            {
                if (repositorioCaixa.EtiquetaJaExiste(caixaAtualizada.Etiqueta) && 
                    repositorioCaixa.SelecionarPorId(id).Etiqueta != caixaAtualizada.Etiqueta)
                {
                    Console.WriteLine("\nUma caixa com esta etiqueta já existe!");
                    Console.ReadKey();
                    return;
                }
                
                if (caixaAtualizada.Validar())
                {
                    repositorioCaixa.Editar(id, caixaAtualizada);
                    Console.WriteLine("\nCaixa atualizada com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nDados inválidos. Verifique se todos os campos estão preenchidos corretamente.");
                }
            }
            
            Console.ReadKey();
        }
        
        public void Excluir()
        {
            Console.Clear();
            Console.WriteLine("=== Excluir Caixa ===");
            
            VisualizarTodos();
            
            Console.Write("\nDigite o ID da caixa que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());
            
            Caixa caixa = repositorioCaixa.SelecionarPorId(id);
            
            if (!repositorioCaixa.PodeExcluir(caixa, revistas))
            {
                Console.WriteLine("\nNão é possível excluir esta caixa pois ela possui revistas vinculadas.");
                Console.ReadKey();
                return;
            }
            
            repositorioCaixa.Excluir(id);
            Console.WriteLine("\nCaixa excluída com sucesso!");
            Console.ReadKey();
        }
        
        public void VisualizarTodos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Caixas ===");
            
            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();
            
            if (caixas.Count == 0)
            {
                Console.WriteLine("\nNenhuma caixa cadastrada.");
                return;
            }
            
            Console.WriteLine("ID | Etiqueta | Cor | Dias de Empréstimo");
            Console.WriteLine("-----------------------------------");
            
            for (int i = 0; i < caixas.Count; i++)
            {
                Console.WriteLine($"{i} | {caixas[i].Etiqueta} | {caixas[i].Cor} | {caixas[i].DiasEmprestimo}");
            }
        }
        
        private Caixa ObterCaixa()
        {
            Console.Write("Etiqueta (máximo 50 caracteres): ");
            string etiqueta = Console.ReadLine();
            
            Console.Write("Cor: ");
            string cor = Console.ReadLine();
            
            Console.Write("Dias de empréstimo (padrão 7): ");
            string diasStr = Console.ReadLine();
            
            int dias = 7;
            if (!string.IsNullOrEmpty(diasStr))
            {
                try
                {
                    dias = Convert.ToInt32(diasStr);
                }
                catch
                {
                    Console.WriteLine("Valor inválido para dias. Usando o padrão (7).");
                    dias = 7;
                }
            }
            
            return new Caixa(etiqueta, cor, dias);
        }
    }
} 