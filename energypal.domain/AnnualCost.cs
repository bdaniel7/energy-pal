namespace energypal.domain;

public class AnnualCost {

  double VAT = 1.05;
  int daysInYear = 365;

  public AnnualCost(EnergyPlan energyPlan) {
    SupplierName = energyPlan.SupplierName;
    PlanName = energyPlan.PlanName;
    Prices = energyPlan.Prices;
    StandingCharge = energyPlan.StandingCharge;
  }

  internal PlanPrice[] Prices { get; set; }

  internal int StandingCharge { get; set; }

  public double TotalCost { get; private set; }

  public string SupplierName { get; init; }

  public string PlanName { get; init; }

  /// <inheritdoc />
  public override string ToString() =>
      $"{SupplierName}, {PlanName}, \u00a3 {TotalCost}";

  public void CalculateFor(int annualConsumption) {
    double energyCost = 0;
    double sumForThreshold = 0;
    foreach (var price in Prices) {
      sumForThreshold += price.Threshold;

      if (price.Threshold == 0) {
        energyCost += (annualConsumption - sumForThreshold) * price.Rate;
      }
      else {
        energyCost += price.Rate * price.Threshold;
      }
    }

    if (StandingCharge > 0.0) {
      TotalCost = Math.Round((energyCost + StandingCharge * daysInYear) * VAT / 100,
                             2);
    }
    else {
      TotalCost = Math.Round(energyCost * VAT / 100,
                             2);
    }
  }
}
