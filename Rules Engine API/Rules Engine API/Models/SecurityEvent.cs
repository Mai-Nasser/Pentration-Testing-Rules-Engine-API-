namespace Rules_Engine_API.Models;

public class SecurityEvent
{
     public string EventType { get; set; } = "web_attack";
    public DateTime Timestamp { get; set; }

     public string AttackType { get; set; }
    public List<string> MatchedAttacks { get; set; }

     public string SourceIp { get; set; }
    public string Payload { get; set; }

     public string Decoy { get; set; }
}
