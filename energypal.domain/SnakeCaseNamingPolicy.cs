using System.Text;
using System.Text.Json;

namespace energypal.domain;

public class SnakeCaseNamingPolicy : JsonNamingPolicy {
  /// <inheritdoc />
  public override string ConvertName(string name) => ToSnakeCase(name);

  internal enum SnakeCaseState {
    Start,
    Lower,
    Upper,
    NewWord,
  }

  // borrowed from Newtonsoft.Json since Microsoft's Text.Json doesn't support snake_case naming for json fields.
  static string ToSnakeCase(string s) {
    if (string.IsNullOrEmpty(s))
      return s;
    StringBuilder stringBuilder = new StringBuilder();
    SnakeCaseState snakeCaseState = SnakeCaseState.Start;

    for (int index = 0; index < s.Length; ++index) {
      if (s[index] == ' ') {
        if (snakeCaseState != SnakeCaseState.Start)
          snakeCaseState = SnakeCaseState.NewWord;
      }
      else if (char.IsUpper(s[index])) {
        switch (snakeCaseState) {
          case SnakeCaseState.Lower:
          case SnakeCaseState.NewWord:
            stringBuilder.Append('_');

            break;
          case SnakeCaseState.Upper:
            bool flag = index + 1 < s.Length;

            if ((index > 0) & flag) {
              char c = s[index + 1];

              if (!char.IsUpper(c) && c != '_') {
                stringBuilder.Append('_');

                break;
              }

              break;
            }

            break;
        }
        char lowerInvariant = char.ToLowerInvariant(s[index]);
        stringBuilder.Append(lowerInvariant);
        snakeCaseState = SnakeCaseState.Upper;
      }
      else if (s[index] == '_') {
        stringBuilder.Append('_');
        snakeCaseState = SnakeCaseState.Start;
      }
      else {
        if (snakeCaseState == SnakeCaseState.NewWord)
          stringBuilder.Append('_');
        stringBuilder.Append(s[index]);
        snakeCaseState = SnakeCaseState.Lower;
      }
    }

    return stringBuilder.ToString();
  }
}
