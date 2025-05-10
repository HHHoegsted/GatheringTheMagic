using System.Text.Json;

namespace GatheringTheMagic.Client.Utility
{
    public class JsonCleaner
    {
        public static string CleanJson(string jsonString)
        {
            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                var cleanedObject = CleanJsonElement(doc.RootElement);
                return JsonSerializer.Serialize(cleanedObject);
            }
        }
        private static object CleanJsonElement(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                var cleanedDict = new Dictionary<string, object>();

                foreach (var property in element.EnumerateObject())
                {                   
                    if (property.Name != "$id" && property.Name != "$ref")
                    {
                        cleanedDict[property.Name] = CleanJsonElement(property.Value);
                    }
                }

                return cleanedDict;
            }
            else if (element.ValueKind == JsonValueKind.Array)
            {
                var cleanedList = new List<object>();

                foreach (var item in element.EnumerateArray())
                {
                    cleanedList.Add(CleanJsonElement(item));
                }

                return cleanedList;
            }
            else
            {
                return element.GetRawText();
            }
        }
    }
}
