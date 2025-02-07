namespace Ambev.DeveloperEvaluation.Common.Helpers;

public static class OrderParser
{
    public static List<(string PropertyName, bool Ascendent)> ParseSortingFields(this string? input)
    {
        var sortingList = new List<(string PropertyName, bool Ascendent)>();

        if (string.IsNullOrWhiteSpace(input))
            return sortingList;

        var elements = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var ascendent = true;

        foreach (var element in elements)
        {
            var parts = element.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                continue;

            var field = parts[0].Trim();

            if (parts.Length > 1)
                ascendent = parts[1].Trim().Equals("asc", StringComparison.OrdinalIgnoreCase);

            sortingList.Add((field, ascendent));
        }

        return sortingList;
    }
}

