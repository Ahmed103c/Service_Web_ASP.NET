
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class OmdbSearchResponse
{
    [JsonPropertyName("Search")]
    public List<OmdbFilm>? Search { get; set; }

    [JsonPropertyName("totalResults")]
    public string? TotalResults { get; set; }

    [JsonPropertyName("Response")]
    public string? Response { get; set; }
}
