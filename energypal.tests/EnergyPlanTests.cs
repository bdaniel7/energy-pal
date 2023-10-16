using energypal.domain;

namespace energypal.tests;

public class EnergyPlanTests {

  [Test]
  public void GetPlansFromFile() {
    string fileName = "plans.json";
    var energyPlanCalculator = new EnergyPlanCalculator();
    energyPlanCalculator.GetPlansFromFile(fileName);
    Assert.That(energyPlanCalculator.EnergyPlans.Count, Is.EqualTo(4));
    var energyPlan3 = energyPlanCalculator.EnergyPlans[2];
    Assert.That(energyPlan3.PlanName, Is.EqualTo("planThree"));
    Assert.That(energyPlan3.Prices[0].Rate, Is.EqualTo(14.5));
    Assert.That(energyPlan3.Prices[1].Rate, Is.EqualTo(10.1));
    Assert.That(energyPlan3.Prices[2].Rate, Is.EqualTo(9));
  }

  [TestCase(1000, 108.68)]
  [TestCase(2000, 213.68)]
  public void CalculatePlanOne(int annualkWh, double annualCostValue) {
    var energyPlan = new EnergyPlan() {
                                          PlanName = "planOne",
                                          SupplierName = "energyOne",
                                          Prices = new[] {
                                                             new PlanPrice {
                                                                               Rate = 13.5,
                                                                               Threshold = 100
                                                                           },
                                                             new PlanPrice() {
                                                                                 Rate = 10
                                                                             }
                                                         }
                                      };

    var annualCost = new AnnualCost(energyPlan);
    annualCost.CalculateFor(annualkWh);

    Assert.That(annualCost.TotalCost, Is.EqualTo(annualCostValue));

    Assert.That(annualCost.ToString(), Is.EqualTo($"{annualCost.SupplierName}, {annualCost.PlanName}, \u00a3 {annualCost.TotalCost}"));
  }

  [TestCase(1000, 111.25)]
  [TestCase(2000, 205.75)]
  public void CalculatePlanThree(int annualkWh, double annualCostValue) {
    var energyPlan = new EnergyPlan() {
                                          PlanName = "planThree",
                                          SupplierName = "energyThree",
                                          Prices = new[] {
                                                             new PlanPrice {
                                                                               Rate = 14.5,
                                                                               Threshold = 250
                                                                           },
                                                             new PlanPrice() {
                                                                                 Rate = 10.1,
                                                                                 Threshold = 200
                                                                             },
                                                             new PlanPrice() {
                                                                                 Rate = 9.0
                                                                             }
                                                         }
                                      };

    var annualCost = new AnnualCost(energyPlan);
    annualCost.CalculateFor(annualkWh);

    Assert.That(annualCost.TotalCost, Is.EqualTo(annualCostValue));
  }

  [TestCase(1000, 121.33)]
  [TestCase(2000, 215.83)]
  public void CalculatePlanFour(int annualkWh, double annualCostValue) {
    var energyPlan = new EnergyPlan() {
                                          PlanName = "planFour",
                                          SupplierName = "energyFour",
                                          Prices = new[] {
                                                             new PlanPrice() {
                                                                                 Rate = 9.0
                                                                             }
                                                         },
                                          StandingCharge = 7
                                      };

    var annualCost = new AnnualCost(energyPlan);
    annualCost.CalculateFor(annualkWh);

    Assert.That(annualCost.TotalCost, Is.EqualTo(annualCostValue));
  }
}