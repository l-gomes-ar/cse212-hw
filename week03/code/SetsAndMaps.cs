using System.Diagnostics;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Create a set from the array of words, and a dynamic array for the pairs
        HashSet<string> wordsHashSet = new HashSet<string>(words);
        List<string> pairs = new List<string>();


        // Iterate through each elem of the array
        for (int i = 0; i < words.Length; i++) {
            // Look up for a symmetric pair in the set, as long as the word is not a repeated char
            var symmetricWord = $"{words[i][1]}{words[i][0]}";
            if (wordsHashSet.Contains(symmetricWord) && words[i][0] != words[i][1]) {
                // Add the pair to the list, and remove the pair from the set, to avoid duplicates
                pairs.Add($"{words[i]} & {symmetricWord}");
                wordsHashSet.Remove(words[i]);
                wordsHashSet.Remove(symmetricWord);
            }
        }

        // Convert the list to array
        return pairs.ToArray();

    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // Add degreeName as a key in the dictionary
            // if already exists increase the number of people that earned that degree
            var degreeName = fields[3];
            if (degrees.ContainsKey(degreeName))
                degrees[degreeName]++;
            else
                degrees[degreeName] = 1;
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Ensure case insensitivity, all lower case 
        word1 = word1.ToLower();
        word2 = word2.ToLower();

        // Create a dictionary summarising how many times a letter appears in a word
        var w1Dict = summarizeAndCreateDictionary(word1);
        var w2Dict = summarizeAndCreateDictionary(word2);

        // If the dictionaries are the same size
        // Order the dictionaries by key and stringify them for comparison
        // otherwise, return false
        if (w1Dict.Count == w2Dict.Count) {
            var w1DictAsString = string.Join(", ", w1Dict.OrderBy(c => c.Key));
            var w2DictAsString = string.Join(", ", w2Dict.OrderBy(c => c.Key));

            return  w1DictAsString == w2DictAsString;
        }

        return false;

        Dictionary<char, int> summarizeAndCreateDictionary(string word)
        {
            var wordDict = new Dictionary<char, int>();

            // Loop through each char in string, avoiding spaces
            // Keep track of the number of repetitions for each letter  
            for (int i = 0; i < word.Length; i++) {
                if (word[i] != ' ') {
                    if (wordDict.ContainsKey(word[i]))
                        wordDict[word[i]] += 1;
                    else
                        wordDict[word[i]] = 1;
                }
            }

            return wordDict;
        }
    }


    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
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

        string[] descriptions = new string[featureCollection.Features.Length];

        for (int i = 0; i < featureCollection.Features.Length; i++) {
            var properties = featureCollection.Features[i].Properties;

            descriptions[i] = $"{properties.Place} - Mag {properties.Mag}";
        }

        return [];
    }
}