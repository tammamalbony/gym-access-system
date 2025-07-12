// =============================
// File: Services/BackupService.cs
// =============================
using System.Diagnostics;
using Gym.Api.Data;
using Microsoft.Extensions.Hosting;

namespace Gym.Api.Services;

public class BackupService(ILogger<BackupService> log, IConfiguration cfg) : BackgroundService
{
    private readonly ILogger<BackupService> _log = log;
    private readonly IConfiguration _cfg = cfg;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int intervalHours = _cfg.GetValue<int>("Backup:IntervalHours", 12);
        int startHour = _cfg.GetValue<int>("Backup:StartHour", 2);
        string dir = _cfg.GetValue<string>("Backup:Dir", "backups");
        Directory.CreateDirectory(dir);

        DateTime next = NextRun(DateTime.Now, startHour);
        while (!stoppingToken.IsCancellationRequested)
        {
            var delay = next - DateTime.Now;
            if (delay > TimeSpan.Zero)
                await Task.Delay(delay, stoppingToken);

            await RunBackup(dir, stoppingToken);
            next = next.AddHours(intervalHours);
        }
    }

    private DateTime NextRun(DateTime now, int hour)
    {
        var t = now.Date.AddHours(hour);
        if (t <= now) t = t.AddDays(1);
        return t;
    }

    private async Task RunBackup(string dir, CancellationToken token)
    {
        try
        {
            string cs = _cfg.GetConnectionString("Default")!;
            var builder = new MySqlConnector.MySqlConnectionStringBuilder(cs);
            string path = Path.Combine(dir, $"gym_{DateTime.Now:yyyyMMdd_HHmmss}.sql");

            var args = $"--host={builder.Server} --user={builder.UserID} --password={builder.Password} --routines --events {builder.Database}";
            var psi = new ProcessStartInfo("mysqldump", args)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            using var proc = Process.Start(psi);
            if (proc is null)
            {
                _log.LogError("mysqldump not found");
                return;
            }
            await using var fs = File.Create(path);
            await proc.StandardOutput.BaseStream.CopyToAsync(fs, token);
            await proc.WaitForExitAsync(token);
            CleanupOld(dir);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Backup failed");
        }
    }

    private void CleanupOld(string dir)
    {
        var files = new DirectoryInfo(dir).GetFiles("*.sql").OrderByDescending(f => f.CreationTimeUtc).ToList();
        foreach (var f in files.Skip(50))
        {
            try { f.Delete(); } catch { /* ignore */ }
        }
    }
}
