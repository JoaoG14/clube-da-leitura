using System;
using System.Collections.Generic;
using ClubeDaLeitura.ConsoleApp.Modulos.Caixas;

namespace ClubeDaLeitura.ConsoleApp.Modulos.Revistas
{
    public class TelaRevista
    {
        private RepositorioRevista repositorioRevista;
        private RepositorioCaixa repositorioCaixa;
        
        public TelaRevista(RepositorioRevista repositorio, RepositorioCaixa repositorioCaixa)
        {
            repositorioRevista = repositorio;
            this.repositorioCaixa = repositorioCaixa;
        }
        
        public void Inserir()
        {
            Console.Clear();
            Console.WriteLine("=== Inserir Nova Revista ===");
            
            if (repositorioCaixa.ObterQuantidadeCaixas() == 0)
            {
                Console.WriteLine("\nNão há caixas cadastradas. Cadastre uma caixa primeiro.");
                Console.ReadKey();
                return;
            }
            
            Revista novaRevista = ObterRevista();
            
            if (novaRevista != null)
            {
                if (repositorioRevista.RevistaJaExiste(novaRevista))
                {
                    Console.WriteLine("\nUma revista com este título e edição já existe!");
                    Console.ReadKey();
                    return;
                }
                
                if (novaRevista.Validar())
                {
                    repositorioRevista.Inserir(novaRevista);
                    Console.WriteLine("\nRevista inserida com sucesso!");
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
            Console.WriteLine("=== Editar Revista ===");
            
            VisualizarTodos();
            
            Console.Write("\nDigite o ID da revista que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());
            
            Revista revistaExistente = repositorioRevista.SelecionarPorId(id);
            
            if (revistaExistente.Status == StatusRevista.Emprestada)
            {
                Console.WriteLine("\nNão é possível editar uma revista que está emprestada.");
                Console.ReadKey();
                return;
            }
            
            Revista revistaAtualizada = ObterRevista();
            
            if (revistaAtualizada != null)
            {
                if (repositorioRevista.RevistaJaExiste(revistaAtualizada) && 
                    (revistaExistente.Titulo != revistaAtualizada.Titulo || 
                     revistaExistente.NumeroEdicao != revistaAtualizada.NumeroEdicao))
                {
                    Console.WriteLine("\nUma revista com este título e edição já existe!");
                    Console.ReadKey();
                    return;
                }
                
                if (revistaAtualizada.Validar())
                {
                    revistaAtualizada.Status = revistaExistente.Status;
                    repositorioRevista.Editar(id, revistaAtualizada);
                    Console.WriteLine("\nRevista atualizada com sucesso!");
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
            Console.WriteLine("=== Excluir Revista ===");
            
            VisualizarTodos();
            
            Console.Write("\nDigite o ID da revista que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());
            
            Revista revista = repositorioRevista.SelecionarPorId(id);
            
            if (!repositorioRevista.PodeExcluir(revista))
            {
                Console.WriteLine("\nNão é possível excluir esta revista pois ela está emprestada.");
                Console.ReadKey();
                return;
            }
            
            repositorioRevista.Excluir(id);
            Console.WriteLine("\nRevista excluída com sucesso!");
            Console.ReadKey();
        }
        
        public void VisualizarTodos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Revistas ===");
            
            List<Revista> revistas = repositorioRevista.SelecionarTodos();
            
            if (revistas.Count == 0)
            {
                Console.WriteLine("\nNenhuma revista cadastrada.");
                return;
            }
            
            Console.WriteLine("ID | Título | Edição | Ano | Caixa | Status");
            Console.WriteLine("-----------------------------------------");
            
            for (int i = 0; i < revistas.Count; i++)
            {
                Console.WriteLine($"{i} | {revistas[i].Titulo} | {revistas[i].NumeroEdicao} | " +
                                 $"{revistas[i].AnoPublicacao.Year} | {revistas[i].Caixa.Etiqueta} | {revistas[i].Status}");
            }
        }
        
        public void VisualizarCaixas()
        {
            Console.Clear();
            Console.WriteLine("=== Visualizar Revistas por Caixa ===");
            
            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();
            
            if (caixas.Count == 0)
            {
                Console.WriteLine("\nNenhuma caixa cadastrada.");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("ID | Etiqueta | Cor");
            Console.WriteLine("------------------");
            
            for (int i = 0; i < caixas.Count; i++)
            {
                Console.WriteLine($"{i} | {caixas[i].Etiqueta} | {caixas[i].Cor}");
            }
            
            Console.Write("\nDigite o ID da caixa para visualizar suas revistas: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());
            
            Caixa caixaSelecionada = repositorioCaixa.SelecionarPorId(idCaixa);
            
            Console.WriteLine($"\nRevistas na caixa {caixaSelecionada.Etiqueta}:");
            
            List<Revista> revistas = repositorioRevista.SelecionarTodos();
            bool encontrouRevistas = false;
            
            for (int i = 0; i < revistas.Count; i++)
            {
                if (revistas[i].Caixa == caixaSelecionada)
                {
                    if (!encontrouRevistas)
                    {
                        Console.WriteLine("ID | Título | Edição | Ano | Status");
                        Console.WriteLine("-----------------------------");
                        encontrouRevistas = true;
                    }
                    
                    Console.WriteLine($"{i} | {revistas[i].Titulo} | {revistas[i].NumeroEdicao} | " +
                                     $"{revistas[i].AnoPublicacao.Year} | {revistas[i].Status}");
                }
            }
            
            if (!encontrouRevistas)
            {
                Console.WriteLine("Nenhuma revista encontrada nesta caixa.");
            }
            
            Console.ReadKey();
        }
        
        private Revista ObterRevista()
        {
            Console.Write("Título (2-100 caracteres): ");
            string titulo = Console.ReadLine();
            
            Console.Write("Número da Edição: ");
            int numeroEdicao;
            if (!int.TryParse(Console.ReadLine(), out numeroEdicao))
            {
                Console.WriteLine("Número de edição inválido.");
                return null;
            }
            
            Console.Write("Ano de Publicação: ");
            DateTime anoPublicacao;
            try
            {
                int ano = Convert.ToInt32(Console.ReadLine());
                anoPublicacao = new DateTime(ano, 1, 1);
            }
            catch
            {
                Console.WriteLine("Ano de publicação inválido.");
                return null;
            }
            
            // Listar caixas disponíveis
            Console.WriteLine("\nCaixas disponíveis:");
            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();
            
            for (int i = 0; i < caixas.Count; i++)
            {
                Console.WriteLine($"{i} - {caixas[i].Etiqueta}");
            }
            
            Console.Write("\nSelecione a caixa (ID): ");
            int idCaixa;
            if (!int.TryParse(Console.ReadLine(), out idCaixa) || idCaixa < 0 || idCaixa >= caixas.Count)
            {
                Console.WriteLine("Caixa inválida.");
                return null;
            }
            
            Caixa caixaSelecionada = repositorioCaixa.SelecionarPorId(idCaixa);
            
            return new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);
        }
    }
} 