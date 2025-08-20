public class MenuPrincipal
{
    public static void MostrarMenu()
    {
        int opcao;

        do
        {
            Console.WriteLine("=== Simulador de Bolsa de Valores ===");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Cadastrar");
            Console.WriteLine("3. Sair");
            Console.Write("Escolha uma opção: ");
            int.TryParse(Console.ReadLine(), out opcao);

            switch (opcao)
            {
                case 1:
                    Console.Write("Login: ");
                    Console.ReadLine();
                    Console.Write("Senha: ");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.Write("Nome: ");
                    Console.ReadLine();
                    Console.Write("Login: ");
                    Console.ReadLine();
                    Console.Write("Senha: ");
                    Console.ReadLine();
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        } while (opcao != 3);
    }
}