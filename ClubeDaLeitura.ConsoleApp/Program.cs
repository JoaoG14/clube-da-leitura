using System;

namespace ClubeDaLeitura.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Clube da Leitura - Sistema de Gerenciamento");
            Console.WriteLine("Desenvolvido por: Seu Nome");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            
            TelaPrincipal telaPrincipal = new TelaPrincipal();
            telaPrincipal.Menu();
        }
    }
}
