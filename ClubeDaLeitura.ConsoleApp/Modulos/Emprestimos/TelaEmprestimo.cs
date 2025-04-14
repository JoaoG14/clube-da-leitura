using System;
using System.Collections.Generic;
using ClubeDaLeitura.ConsoleApp.Modulos.Amigos;
using ClubeDaLeitura.ConsoleApp.Modulos.Revistas;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Emprestimos
{
    public class TelaEmprestimo
    {
        private RepositorioEmprestimo repositorioEmprestimo;
        private RepositorioAmigo repositorioAmigo;
        private RepositorioRevista repositorioRevista;
        
        public TelaEmprestimo(RepositorioEmprestimo repositorio, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista)
        {
            repositorioEmprestimo = repositorio;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
        }
        
        public void RegistrarEmprestimo()
        {
            Console.Clear();
            Console.WriteLine("=== Registrar Novo Empréstimo ===");
            
            if (repositorioAmigo.ObterQuantidadeAmigos() == 0)
            {
                Console.WriteLine("\nNão há amigos cadastrados. Cadastre um amigo primeiro.");
                Console.ReadKey();
                return;
            }
            
            if (repositorioRevista.ObterQuantidadeRevistas() == 0)
            {
                Console.WriteLine("\nNão há revistas cadastradas. Cadastre uma revista primeiro.");
                Console.ReadKey();
                return;
            }
            
            // Selecionar amigo
            Console.WriteLine("\nSelecione um amigo:");
            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();
            
            for (int i = 0; i < amigos.Count; i++)
            {
                Console.WriteLine($"{i} - {amigos[i].Nome} | {amigos[i].Telefone}");
            }
            
            Console.Write("\nDigite o ID do amigo: ");
            int idAmigo;
            if (!int.TryParse(Console.ReadLine(), out idAmigo) || idAmigo < 0 || idAmigo >= amigos.Count)
            {
                Console.WriteLine("Amigo inválido.");
                Console.ReadKey();
                return;
            }
            
            Amigo amigoSelecionado = repositorioAmigo.SelecionarPorId(idAmigo);
            
            // Verificar se o amigo já possui um empréstimo ativo
            if (repositorioEmprestimo.VerificarAmigoComEmprestimoAtivo(amigoSelecionado))
            {
                Console.WriteLine("\nEste amigo já possui um empréstimo ativo. Cada amigo só pode ter um empréstimo por vez.");
                Console.ReadKey();
                return;
            }
            
            // Selecionar revista
            Console.WriteLine("\nSelecione uma revista disponível:");
            List<Revista> revistasDisponiveis = repositorioRevista.SelecionarPorStatus(StatusRevista.Disponivel);
            
            if (revistasDisponiveis.Count == 0)
            {
                Console.WriteLine("\nNão há revistas disponíveis para empréstimo no momento.");
                Console.ReadKey();
                return;
            }
            
            for (int i = 0; i < revistasDisponiveis.Count; i++)
            {
                Revista revista = revistasDisponiveis[i];
                Console.WriteLine($"{i} - {revista.Titulo} | Edição: {revista.NumeroEdicao} | Caixa: {revista.Caixa.Etiqueta}");
            }
            
            Console.Write("\nDigite o ID da revista: ");
            int idRevista;
            if (!int.TryParse(Console.ReadLine(), out idRevista) || idRevista < 0 || idRevista >= revistasDisponiveis.Count)
            {
                Console.WriteLine("Revista inválida.");
                Console.ReadKey();
                return;
            }
            
            Revista revistaSelecionada = revistasDisponiveis[idRevista];
            
            try
            {
                Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada);
                
                if (novoEmprestimo.Validar())
                {
                    repositorioEmprestimo.Inserir(novoEmprestimo);
                    
                    Console.WriteLine("\nEmpréstimo registrado com sucesso!");
                    Console.WriteLine($"Data de devolução: {novoEmprestimo.ObterDataDevolucao().ToShortDateString()}");
                }
                else
                {
                    Console.WriteLine("\nNão foi possível registrar o empréstimo. Verifique os dados fornecidos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao registrar empréstimo: {ex.Message}");
            }
            
            Console.ReadKey();
        }
        
        public void RegistrarDevolucao()
        {
            Console.Clear();
            Console.WriteLine("=== Registrar Devolução ===");
            
            List<Emprestimo> emprestimosAbertos = repositorioEmprestimo.SelecionarEmprestimosAbertos();
            
            if (emprestimosAbertos.Count == 0)
            {
                Console.WriteLine("\nNão há empréstimos em aberto para devolução.");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("\nEmpréstimos em aberto:");
            MostrarListaEmprestimos(emprestimosAbertos);
            
            Console.Write("\nDigite o ID do empréstimo a ser devolvido: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id) || id < 0 || id >= emprestimosAbertos.Count)
            {
                Console.WriteLine("ID de empréstimo inválido.");
                Console.ReadKey();
                return;
            }
            
            Emprestimo emprestimo = emprestimosAbertos[id];
            
            try
            {
                emprestimo.RegistrarDevolucao();
                Console.WriteLine("\nDevolução registrada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao registrar devolução: {ex.Message}");
            }
            
            Console.ReadKey();
        }
        
        public void VisualizarTodos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Todos os Empréstimos ===");
            
            List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarTodos();
            
            if (emprestimos.Count == 0)
            {
                Console.WriteLine("\nNenhum empréstimo registrado.");
                Console.ReadKey();
                return;
            }
            
            MostrarListaEmprestimos(emprestimos);
            Console.ReadKey();
        }
        
        public void VisualizarAbertos()
        {
            Console.Clear();
            Console.WriteLine("=== Empréstimos em Aberto ===");
            
            List<Emprestimo> emprestimosAbertos = repositorioEmprestimo.SelecionarEmprestimosAbertos();
            
            if (emprestimosAbertos.Count == 0)
            {
                Console.WriteLine("\nNão há empréstimos em aberto.");
                Console.ReadKey();
                return;
            }
            
            MostrarListaEmprestimos(emprestimosAbertos);
            Console.ReadKey();
        }
        
        public void VisualizarAtrasados()
        {
            Console.Clear();
            Console.WriteLine("=== Empréstimos Atrasados ===");
            
            List<Emprestimo> emprestimosAtrasados = repositorioEmprestimo.SelecionarEmprestimosAtrasados();
            
            if (emprestimosAtrasados.Count == 0)
            {
                Console.WriteLine("\nNão há empréstimos atrasados.");
                Console.ReadKey();
                return;
            }
            
            MostrarListaEmprestimos(emprestimosAtrasados);
            Console.ReadKey();
        }
        
        private void MostrarListaEmprestimos(List<Emprestimo> emprestimos)
        {
            Console.WriteLine("ID | Amigo | Revista | Data Empréstimo | Data Devolução | Situação");
            Console.WriteLine("-----------------------------------------------------------");
            
            for (int i = 0; i < emprestimos.Count; i++)
            {
                Emprestimo emp = emprestimos[i];
                Console.WriteLine($"{i} | {emp.Amigo.Nome} | {emp.Revista.Titulo} | " +
                                 $"{emp.DataEmprestimo.ToShortDateString()} | {emp.ObterDataDevolucao().ToShortDateString()} | {emp.Situacao}");
            }
        }
    }
} 