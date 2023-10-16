using energypal.domain;

namespace energypal;

public class Program {
  public static void Main(string[] args) {
    string? readLine;

    var energyPlanCalculator = new EnergyPlanCalculator();

    showHelp();

    while ((readLine = Console.ReadLine()) != "exit") {

      Console.WriteLine();

      if (readLine is { Length: 0 }) {
        showHelp();
      }
      else {
        var arguments = readLine.Split(" ");
        var command = arguments[0];
        var param = arguments[1];

        if (arguments.Length == 2 && command == "input" && param.EndsWith(".json")) {
          Console.WriteLine($"Command is: {command} {param}.");

          if (!File.Exists(param)) {
            Console.WriteLine($"File {param} does not exist.");
          }
          else {
            energyPlanCalculator.GetPlansFromFile(param);
            Console.WriteLine($"{energyPlanCalculator.EnergyPlans.Count} energy plans were loaded");
            Console.WriteLine();
          }
        }

        if (arguments.Length == 2 && command == "annual_cost" && int.TryParse(param, out int annualConsumption)) {
          var annualCosts = energyPlanCalculator.CalculateAnnualCost(annualConsumption);
          printAnnualCosts(annualCosts);
          Console.WriteLine();
        }
      }
    }
  }
  static void printAnnualCosts(List<AnnualCost> annualCosts) {
    foreach (var annualCost in annualCosts) {
      Console.WriteLine(annualCost.ToString());
    }
  }

  static void showHelp() {
    Console.WriteLine("Usage:");
    Console.WriteLine("input <filename>.json - load the plans for further usage");
    Console.WriteLine("annual_cost <annual_consumption> -  calculate ‘Annual Cost’");
    Console.WriteLine("exit - terminate the program");
    Console.WriteLine();
  }
}
