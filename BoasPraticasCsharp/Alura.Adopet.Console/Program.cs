using Alura.Adopet.Console;
using Alura.Adopet.Console.Comandos;

System.Console.ForegroundColor = System.ConsoleColor.Green;

ComandosDoSistema comandos = new ComandosDoSistema();
try
{
    string comandoExecutar = args[0].Trim();    
    IComando? comando = comandos[comandoExecutar];
    if(comando is not null ) await comando.ExecutarAsync(args);
    else System.Console.WriteLine("Comando inválido!");
}
catch (Exception ex)
{
    // mostra a exceção em vermelho
    System.Console.ForegroundColor = System.ConsoleColor.Red;
    System.Console.WriteLine($"Aconteceu um exceção: {ex.Message}");
}
finally
{
    System.Console.ForegroundColor = System.ConsoleColor.White;
}

