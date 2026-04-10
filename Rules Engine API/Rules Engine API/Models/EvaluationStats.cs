 
namespace Rules_Engine_API.Models;

public class EvaluationStats
{
     
    public int TotalPayloadsTested { get; set; }

     public int DetectionCorrectCount { get; set; }
    public int DetectionIncorrectCount { get; set; }
    public double DetectionAccuracyPercent { get; set; }

     public int ClassificationCorrectCount { get; set; }
    public int ClassificationIncorrectCount { get; set; }
    public double ClassificationAccuracyPercent { get; set; }
}
 
public class FailedPayloadEntry
{
    public int SessionId { get; set; }
    public string Payload { get; set; }
    public string AttackName { get; set; }
    public string Pattern { get; set; }
    public string FailureType { get; set; }   
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
