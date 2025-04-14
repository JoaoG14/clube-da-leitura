using System;
using System.Collections.Generic;
using ClubeDaLeitura.ConsoleApp.Modulos.Amigos;
using ClubeDaLeitura.ConsoleApp.Modulos.Caixas;
using ClubeDaLeitura.ConsoleApp.Modulos.Revistas;
using ClubeDaLeitura.ConsoleApp.Modulos.Emprestimos;

namespace ClubeDaLeitura.ConsoleApp
{
    public class TelaPrincipal
    {
        private RepositorioAmigo repositorioAmigo;
        private RepositorioCaixa repositorioCaixa;
        private RepositorioRevista repositorioRevista;
        private RepositorioEmprestimo repositorioEmprestimo;
        
        private TelaAmigo telaAmigo;
        private TelaCaixa telaCaixa;
        private TelaRevista telaRevista;
        private TelaEmprestimo telaEmprestimo;
        
        public TelaPrincipal()
        {
            repositorioAmigo = new RepositorioAmigo();
            repositorioCaixa = new RepositorioCaixa();
            repositorioRevista = new RepositorioRevista();
            repositorioEmprestimo = new RepositorioEmprestimo();
            
            List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarTodos();
            List<Revista> revistas = repositorioRevista.SelecionarTodos();
            
            telaAmigo = new TelaAmigo(repositorioAmigo, emprestimos);
            telaCaixa = new TelaCaixa(repositorioCaixa, revistas);
            telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);
            telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista);
        }
        
        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Clube da Leitura =====");
                Console.WriteLine("1 - Módulo de Amigos");
                Console.WriteLine("2 - Módulo de Caixas");
                Console.WriteLine("3 - Módulo de Revistas");
                Console.WriteLine("4 - Módulo de Empréstimos");
                Console.WriteLine("0 - Sair");
                
                Console.Write("\nDigite a opção desejada: ");
                string opcao = Console.ReadLine();
                
                switch (opcao)
                {
                    case "1":
                        MenuAmigos();
                        break;
                    
                    case "2":
                        MenuCaixas();
                        break;
                    
                    case "3":
                        MenuRevistas();
                        break;
                    
                    case "4":
                        MenuEmprestimos();
                        break;
                    
                    case "0":
                        return;
                    
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        private void MenuAmigos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Módulo de Amigos =====");
                Console.WriteLine("1 - Inserir Amigo");
                Console.WriteLine("2 - Editar Amigo");
                Console.WriteLine("3 - Excluir Amigo");
                Console.WriteLine("4 - Visualizar Todos os Amigos");
                Console.WriteLine("5 - Visualizar Empréstimos de um Amigo");
                Console.WriteLine("0 - Voltar");
                
                Console.Write("\nDigite a opção desejada: ");
                string opcao = Console.ReadLine();
                
                switch (opcao)
                {
                    case "1":
                        telaAmigo.Inserir();
                        break;
                    
                    case "2":
                        telaAmigo.Editar();
                        break;
                    
                    case "3":
                        telaAmigo.Excluir();
                        break;
                    
                    case "4":
                        telaAmigo.VisualizarTodos();
                        Console.ReadKey();
                        break;
                    
                    case "5":
                        telaAmigo.VisualizarEmprestimos();
                        break;
                    
                    case "0":
                        return;
                    
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        private void MenuCaixas()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Módulo de Caixas =====");
                Console.WriteLine("1 - Inserir Caixa");
                Console.WriteLine("2 - Editar Caixa");
                Console.WriteLine("3 - Excluir Caixa");
                Console.WriteLine("4 - Visualizar Todas as Caixas");
                Console.WriteLine("0 - Voltar");
                
                Console.Write("\nDigite a opção desejada: ");
                string opcao = Console.ReadLine();
                
                switch (opcao)
                {
                    case "1":
                        telaCaixa.Inserir();
                        break;
                    
                    case "2":
                        telaCaixa.Editar();
                        break;
                    
                    case "3":
                        telaCaixa.Excluir();
                        break;
                    
                    case "4":
                        telaCaixa.VisualizarTodos();
                        Console.ReadKey();
                        break;
                    
                    case "0":
                        return;
                    
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        private void MenuRevistas()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Módulo de Revistas =====");
                Console.WriteLine("1 - Inserir Revista");
                Console.WriteLine("2 - Editar Revista");
                Console.WriteLine("3 - Excluir Revista");
                Console.WriteLine("4 - Visualizar Todas as Revistas");
                Console.WriteLine("5 - Visualizar Revistas por Caixa");
                Console.WriteLine("0 - Voltar");
                
                Console.Write("\nDigite a opção desejada: ");
                string opcao = Console.ReadLine();
                
                switch (opcao)
                {
                    case "1":
                        telaRevista.Inserir();
                        break;
                    
                    case "2":
                        telaRevista.Editar();
                        break;
                    
                    case "3":
                        telaRevista.Excluir();
                        break;
                    
                    case "4":
                        telaRevista.VisualizarTodos();
                        Console.ReadKey();
                        break;
                    
                    case "5":
                        telaRevista.VisualizarCaixas();
                        break;
                    
                    case "0":
                        return;
                    
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        private void MenuEmprestimos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Módulo de Empréstimos =====");
                Console.WriteLine("1 - Registrar Empréstimo");
                Console.WriteLine("2 - Registrar Devolução");
                Console.WriteLine("3 - Visualizar Todos os Empréstimos");
                Console.WriteLine("4 - Visualizar Empréstimos em Aberto");
                Console.WriteLine("5 - Visualizar Empréstimos Atrasados");
                Console.WriteLine("0 - Voltar");
                
                Console.Write("\nDigite a opção desejada: ");
                string opcao = Console.ReadLine();
                
                switch (opcao)
                {
                    case "1":
                        telaEmprestimo.RegistrarEmprestimo();
                        break;
                    
                    case "2":
                        telaEmprestimo.RegistrarDevolucao();
                        break;
                    
                    case "3":
                        telaEmprestimo.VisualizarTodos();
                        break;
                    
                    case "4":
                        telaEmprestimo.VisualizarAbertos();
                        break;
                    
                    case "5":
                        telaEmprestimo.VisualizarAtrasados();
                        break;
                    
                    case "0":
                        return;
                    
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
} 