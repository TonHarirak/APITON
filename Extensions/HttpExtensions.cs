using System;
using System.Text.Json;
using APITON.Helpers;

namespace APITON.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
    {
        var jsonOpts = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        response.Headers.Add("Pagination", JsonSerializer.Serialize(header, jsonOpts));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}
