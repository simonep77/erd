using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using ERD.Scheduler;


AppContextERD.WriteLog("INFO", "ERD Scheduler in avvio...");

AppContextERD.Conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("NET_ENVIRONMENT") ?? "Development"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();


AppContextERD.WriteLog("INFO", "Letta configurazione");

//Gestione arresto globale
AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
{
    AppContextERD.WriteLog("INFO", "ERD Scheduler in arresto...");
    AppContextERD.Scheduler?.Stop();
    AppContextERD.WriteLog("INFO", "Arresto completato.");
};

//Su cancellazione, sblocca chiusura
Console.CancelKeyPress += (sender, e) =>
{
    Environment.Exit(0);
};

//Avvia scheduler
AppContextERD.Scheduler = new IntSvcScheduler();
AppContextERD.Scheduler.Start();

//Attende chiusura
Thread.Sleep(Timeout.Infinite);

