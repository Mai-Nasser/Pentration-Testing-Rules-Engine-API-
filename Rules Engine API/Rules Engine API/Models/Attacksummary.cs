 
namespace Rules_Engine_API.Models;
 
public class AttackSummary
{
    public string AttackName { get; set; }

     public int EvaluatedCount { get; set; }

     public double DetectionErrorRate { get; set; }

     public double ClassificationErrorRate { get; set; }

     public List<FailedPayloadInfo> FailedPayloads { get; set; } = new();
}

 
public class FailedPayloadInfo
{
     public int SessionId { get; set; }

    public string Payload { get; set; }
    public string Pattern { get; set; }
    public bool DetectionCorrect { get; set; }
    public bool ClassificationCorrect { get; set; }
    public string? AssignedTo { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
