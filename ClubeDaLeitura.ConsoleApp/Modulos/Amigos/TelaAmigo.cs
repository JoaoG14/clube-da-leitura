using System;
using System.Collections.Generic;
using ClubeDaLeitura.ConsoleApp.Modulos.Emprestimos;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Amigos
{
    public class TelaAmigo
    {
        private RepositorioAmigo repositorioAmigo;
        private List<Emprestimo> emprestimos;
        
        public TelaAmigo(RepositorioAmigo repositorio, List<Emprestimo> emprestimos)
        {
            repositorioAmigo = repositorio;
            this.emprestimos = emprestimos;
        }
        
        public void Inserir()
        {
            Console.Clear();
            Console.WriteLine("=== Inserir Novo Amigo ===");
            
            Amigo novoAmigo = ObterAmigo();
            
            if (novoAmigo != null)
            {
                if (repositorioAmigo.AmigoJaExiste(novoAmigo))
                {
                    Console.WriteLine("\nUm amigo com este nome e telefone já existe!");
                    Console.ReadKey();
                    return;
                }
                
                if (novoAmigo.Validar())
                {
                    repositorioAmigo.Inserir(novoAmigo);
                    Console.WriteLine("\nAmigo inserido com sucesso!");
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
            Console.WriteLine("=== Editar Amigo ===");
            
            VisualizarTodos();
            
            Console.Write("\nDigite o ID do amigo que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());
            
            Amigo amigoAtualizado = ObterAmigo();
            
            if (amigoAtualizado != null)
            {
                if (amigoAtualizado.Validar())
                {
                    repositorioAmigo.Editar(id, amigoAtualizado);
                    Console.WriteLine("\nAmigo atualizado com sucesso!");
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
            Console.WriteLine("=== Excluir Amigo ===");
            
            VisualizarTodos();
            
            Console.Write("\nDigite o ID do amigo que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());
            
            Amigo amigo = repositorioAmigo.SelecionarPorId(id);
            
            if (!repositorioAmigo.PodeExcluir(amigo, emprestimos))
            {
                Console.WriteLine("\nNão é possível excluir este amigo pois ele possui empréstimos vinculados.");
                Console.ReadKey();
                return;
            }
            
            repositorioAmigo.Excluir(id);
            Console.WriteLine("\nAmigo excluído com sucesso!");
            Console.ReadKey();
        }
        
        public void VisualizarTodos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Amigos ===");
            
            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();
            
            if (amigos.Count == 0)
            {
                Console.WriteLine("\nNenhum amigo cadastrado.");
                return;
            }
            
            Console.WriteLine("ID | Nome | Responsável | Telefone");
            Console.WriteLine("----------------------------");
            
            for (int i = 0; i < amigos.Count; i++)
            {
                Console.WriteLine($"{i} | {amigos[i].Nome} | {amigos[i].NomeResponsavel} | {amigos[i].Telefone}");
            }
        }
        
        public void VisualizarEmprestimos()
        {
            Console.Clear();
            Console.WriteLine("=== Empréstimos por Amigo ===");
            
            VisualizarTodos();
            
            Console.Write("\nDigite o ID do amigo para visualizar seus empréstimos: ");
            int id = Convert.ToInt32(Console.ReadLine());
            
            Amigo amigo = repositorioAmigo.SelecionarPorId(id);
            
            List<Emprestimo> emprestimosDoAmigo = amigo.ObterEmprestimos(emprestimos);
            
            if (emprestimosDoAmigo.Count == 0)
            {
                Console.WriteLine("\nEste amigo não possui empréstimos.");
            }
            else
            {
                Console.WriteLine($"\nEmpréstimos de {amigo.Nome}:");
                Console.WriteLine("ID | Revista | Data Empréstimo | Data Devolução | Situação");
                Console.WriteLine("---------------------------------------------------");
                
                for (int i = 0; i < emprestimosDoAmigo.Count; i++)
                {
                    Emprestimo emp = emprestimosDoAmigo[i];
                    Console.WriteLine($"{i} | {emp.Revista.Titulo} | {emp.DataEmprestimo.ToShortDateString()} | {emp.DataDevolucao.ToShortDateString()} | {emp.Situacao}");
                }
            }
            
            Console.ReadKey();
        }
        
        private Amigo ObterAmigo()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            
            Console.Write("Nome do Responsável: ");
            string nomeResponsavel = Console.ReadLine();
            
            Console.Write("Telefone (formato: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX): ");
            string telefone = Console.ReadLine();
            
            return new Amigo(nome, nomeResponsavel, telefone);
        }
    }
} 