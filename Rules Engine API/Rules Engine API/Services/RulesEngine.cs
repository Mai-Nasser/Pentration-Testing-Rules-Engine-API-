 

using System.Text.Json;
using System.Text.RegularExpressions;
using Rules_Engine_API.Models;

namespace Rules_Engine_API.Services
{
    public class RulesEngine : IRulesEngine
    {
        private readonly List<AttackRule> _rules = new();

        public RulesEngine()
        {
            var rulesPath = Path.Combine(AppContext.BaseDirectory, "Rules", "conf-6.json");

            if (!File.Exists(rulesPath))
                throw new FileNotFoundException("Rules file not found", rulesPath);

            var json = File.ReadAllText(rulesPath);

            LoadSafeRules(json);
        }

        
        private void LoadSafeRules(string json)
        {
            var rawRules = JsonSerializer.Deserialize<List<AttackRule>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (rawRules == null)
                return;

            foreach (var rule in rawRules)
            {
                if (rule.Patterns == null || rule.Patterns.Count == 0)
                    continue;

                var safePatterns = new List<AttackPattern>();

                foreach (var pattern in rule.Patterns)
                {
                    if (string.IsNullOrWhiteSpace(pattern.Regex))
                        continue;

                    try
                    {
                         _ = new Regex(pattern.Regex, RegexOptions.Compiled);
                        safePatterns.Add(pattern);
                    }
                    catch (RegexParseException)
                    {
                         continue;
                    }
                }

                if (safePatterns.Any())
                {
                    rule.Patterns = safePatterns;
                    _rules.Add(rule);
                }
            }
        }

        public List<DetectionResult> Analyze(string payload)
        {
            var results = new List<DetectionResult>();

            if (string.IsNullOrWhiteSpace(payload))
                return results;

            foreach (var rule in _rules)
            {
                foreach (var pattern in rule.Patterns)
                {
                    try
                    {
                        if (Regex.IsMatch(payload, pattern.Regex, RegexOptions.IgnoreCase))
                        {
                            results.Add(new DetectionResult
                            {
                                AttackId = rule.Id,
                                AttackName = rule.Name,
                                Pattern = pattern.Regex,
                                Score = pattern.Score
                            });
                        }
                    }
                    catch
                    {
                         continue;
                    }
                }
            }

            return results;
        }
    }
}
