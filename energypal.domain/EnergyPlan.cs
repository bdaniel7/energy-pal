namespace energypal.domain;

public class EnergyPlan {
  public string SupplierName { get; set; }

  public string PlanName { get; set; }

  public PlanPrice[] Prices { get; set; }

  public int StandingCharge { get; set; }
}

public class PlanPrice {
  public double Rate { get; set; }

  public int Threshold { get; set; } = 0;
}