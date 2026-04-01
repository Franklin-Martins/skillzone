using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Utils;

public static class SlugHelper
{
    public static string Slugify(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // 1. Normaliza para decompor acentos
        var normalized = input.Normalize(NormalizationForm.FormD);

        var sb = new StringBuilder();

        foreach (var c in normalized)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

            // Remove acentos
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        // 2. Converte para minúsculo
        var cleaned = sb.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();

        // 3. Remove caracteres inválidos
        cleaned = Regex.Replace(cleaned, @"[^a-z0-9\s-]", "");

        // 4. Substitui espaços por hífen
        cleaned = Regex.Replace(cleaned, @"\s+", "-").Trim('-');

        // 5. Remove hífens duplicados
        cleaned = Regex.Replace(cleaned, @"-+", "-");

        return cleaned;
    }
}