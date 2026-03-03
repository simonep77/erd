using ERD.Scheduler;
using ERD.Service.BIZ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using MoreLinq;
using System.Runtime.InteropServices;


AppContextERD.WriteLog("INFO", "ERD Scheduler in avvio...");

AppContextERD.Conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("NET_ENVIRONMENT") ?? "Development"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();


AppContextERD.WriteLog("INFO", "Letta configurazione");

//Registra diversi segnali per la chiusura
var segnali = new PosixSignal[] { PosixSignal.SIGTERM, PosixSignal.SIGHUP, PosixSignal.SIGINT };
var registrazioni = new List<PosixSignalRegistration>();
segnali.ForEach(s =>
{
    registrazioni.Add(PosixSignalRegistration.Create(s, (h) =>
    {
        AppContextERD.WriteLog("INFO", $"{h.Signal} ricevuto");
        endProcess();
    }));
});


//Avvia scheduler
AppContextERD.Scheduler = new IntSvcScheduler();
AppContextERD.Scheduler.Start();

//Test
//var s = AppContextERD.CreateSlot();
//var est = s.BizNewWithLoadByPK<ReportEstrazioneBIZ>(244);

//est.Run(false, false, false);
//est.SendEmail(false);

//Attende chiusura
Thread.Sleep(Timeout.Infinite);

void endProcess()
{
    AppContextERD.WriteLog("INFO", "ERD Scheduler in arresto...");
    AppContextERD.Scheduler?.Stop();
    AppContextERD.WriteLog("INFO", "Arresto completato.");
    Environment.Exit(0);
}