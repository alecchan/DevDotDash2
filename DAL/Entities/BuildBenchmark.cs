using System;

public class BuildBenchmark {
    public object Id { get; set; }

    public string Host { get; set; }
    public string Solution { get; set; }

    public Guid SolutionGuid { get; set; }
    public string Tags { get; set; }
    public DateTime CreatedDate { get; set; }
    public int ProjectCount { get; set; }
    public int FileCount { get; set; }
    public bool SolutionBuild { get; set; }
    public bool Successful { get; set; }
    public bool UpToDate { get; set; }
    public int BuildDuration { get; internal set; }
}
