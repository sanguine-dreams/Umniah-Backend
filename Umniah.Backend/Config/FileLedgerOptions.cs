namespace Umniah.Backend.Config;

public class FileLedgerOptions
{
    public const string SectionName = "FileLedger";

    public bool Enabled { get; set; } = true;
    public int IntervalMinutes { get; set; } = 10080;
    public int PendingGraceHours { get; set; } = 24;
    public int BatchSize { get; set; } = 200;
    public int MaxAttempts { get; set; } = 5;
    public int KeepPerTag { get; set; } = 1000;
}
