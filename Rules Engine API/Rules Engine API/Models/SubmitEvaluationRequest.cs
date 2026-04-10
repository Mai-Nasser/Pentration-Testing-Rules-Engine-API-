 
namespace Rules_Engine_API.Models;
 
public class SubmitEvaluationRequest
{
    public int SessionId { get; set; }
    public List<AttackEvaluation> AttackEvaluations { get; set; } = new();
}

public class AttackEvaluation
{
    public string AttackName { get; set; }

     public string Pattern { get; set; }
    public bool DetectionCorrect { get; set; }
    public bool ClassificationCorrect { get; set; }
    public string? Notes { get; set; }
}
