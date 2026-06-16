using LBPUnion.ProjectLighthouse.Localization;

namespace LBPUnion.ProjectLighthouse.Servers.GameServer.Helpers;

public static class TranslationHelper
{
    // Map LBP locale codes to asp.net friendly codes, as they do not match casing and some are non-standard
    private static readonly Dictionary<string, string> LBPLocaleMap = new()
    {
        { "pt-br", "pt-BR" }, // (Brazilian Portuguese)
        { "pt", "pt-PT" }, // (Portuguese)
        { "zh-tw", "zh-TW" }, // (Traditional Chinese)
        { "zh-cn", "zh-CN" }, // (Simplified Chinese)
        { "da", "da-DK" }, // (Danish)
        { "nl", "nl-NL" }, // (Dutch)
        { "en-gb", "en-GB" }, // (GB English)
        { "en-us", "en" }, // (US English)
        { "fi", "fi-FI" }, // (Finnish)
        { "fr", "fr-FR" }, // (French)
        { "de", "de-DE" }, // (German)
        { "it", "it-IT" }, // (Italian)
        { "ja", "ja-JP" }, // (Japanese)
        { "ko", "ko-KR" }, // (Korean)
        { "no", "nb-NO" }, // (Norwegian) // no-NO locale code is legacy in dotnet
        { "pl", "pl-PL" }, // (Polish)
        { "ru", "ru-RU" }, // (Russian)
        { "es", "es-ES" }, // (Spanish)
        { "es-419", "es-MX" }, // (Mexican Spanish)
        { "sv", "sv-SE" }, // (Swedish)
        { "tr", "tr-TR" }, // (Turkish) 
        { "ar", "ar-SA" }, // (Arabic)
    };

    public static string MapLBPLocaleCode(string language)
    {
        foreach (KeyValuePair<string, string> kv in LBPLocaleMap)
        {
            if (kv.Key == language)
            {
                Console.WriteLine($@"Key: '{kv.Key}' Value: '{kv.Value}'");
                return kv.Value;
            }
        }

        Console.WriteLine($@"Default lang: '{LocalizationManager.DefaultLang}'");
        return LocalizationManager.DefaultLang;
    }
}