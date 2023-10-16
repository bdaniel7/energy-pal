using System.Collections.ObjectModel;
using System.Text.Json;

namespace energypal.domain;

public class EnergyPlanCalculator {
  List<EnergyPlan> energyPlans = new();

  public ReadOnlyCollection<EnergyPlan> EnergyPlans => new(energyPlans);

  public void GetPlansFromFile(string fileName) {
    string json = File.ReadAllText(fileName);

    var serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = new SnakeCaseNamingPolicy() };

    energyPlans = JsonSerializer.Deserialize<List<EnergyPlan>>(json, serializerOptions)!;
  }

  public List<AnnualCost> CalculateAnnualCost(int annualConsumption) {
    var annualCosts = new List<AnnualCost>();
    foreach (var energyPlan in energyPlans) {
      AnnualCost cost = new AnnualCost(energyPlan);
      cost.CalculateFor(annualConsumption);
      annualCosts.Add(cost);
    }

    return annualCosts
          .OrderBy(ac => ac.TotalCost)
          .ToList();
  }
}
