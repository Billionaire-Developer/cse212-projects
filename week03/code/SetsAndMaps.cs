using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    // Problem 1: Find symmetric pairs
    public static string[] FindPairs(string[] words)
    {
        var wordSet = new HashSet<string>(words);
        var result = new HashSet<string>();

        foreach (var word in words)
        {
            if (word[0] == word[1]) continue;

            var reversed = new string(new[] { word[1], word[0] });
            if (wordSet.Contains(reversed))
            {
                var pair = string.Compare(word, reversed) < 0 ? $"{word} & {reversed}" : $"{reversed} & {word}";
                result.Add(pair);
            }
        }

        return result.ToArray();
    }

    // Problem 2: Summarize degrees from CSV
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length < 4) continue;

            string degree = fields[3].Trim();
            if (string.IsNullOrEmpty(degree)) continue;

            if (!degrees.ContainsKey(degree))
                degrees[degree] = 0;

            degrees[degree]++;
        }

        return degrees;
    }

    // Problem 3: Check if two words are anagrams
    public static bool IsAnagram(string word1, string word2)
    {
        string Normalize(string s) => new string(s.ToLower().Where(char.IsLetter).ToArray());

        var dict = new Dictionary<char, int>();
        foreach (char c in Normalize(word1))
        {
            if (!dict.ContainsKey(c)) dict[c] = 0;
            dict[c]++;
        }

        foreach (char c in Normalize(word2))
        {
            if (!dict.ContainsKey(c)) return false;
            dict[c]--;
            if (dict[c] < 0) return false;
        }

        return dict.Values.All(v => v == 0);
    }

    // Problem 5: Fetch and summarize earthquake data from JSON
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);
        var results = new List<string>();

        foreach (var feature in featureCollection.Features)
        {
            if (feature?.Properties?.Place != null && feature.Properties.Mag.HasValue)
            {
                results.Add($"{feature.Properties.Place} - Mag {feature.Properties.Mag.Value}");
            }
        }

        return results.ToArray();
    }
}

// JSON Deserialization Classes for Earthquake Data
public class FeatureCollection
{
    public List<Feature> Features { get; set; }
}

public class Feature
{
    public Properties Properties { get; set; }
}

public class Properties
{
    public string Place { get; set; }
    public double? Mag { get; set; }
}
