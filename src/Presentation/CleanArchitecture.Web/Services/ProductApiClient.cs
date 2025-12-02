using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace CleanArchitecture.Web.Services;

public class ProductApiClient
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public ProductApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<(IReadOnlyList<ProductDto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? search = null, CancellationToken ct = default)
    {
        try
        {
            var url = $"/api/products?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(search))
            {
                url += $"&search={Uri.EscapeDataString(search)}";
            }
            var response = await _http.GetAsync(url, ct);
            response.EnsureSuccessStatusCode();
            var payload = await response.Content.ReadFromJsonAsync<PagedResponse<ProductDto>>(JsonOptions, ct);
            return (payload?.Items ?? Array.Empty<ProductDto>(), payload?.TotalCount ?? 0);
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Failed to fetch products: {ex.Message}", ex);
        }
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            return await _http.GetFromJsonAsync<ProductDto>($"/api/products/{id}", JsonOptions, ct);
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Failed to fetch product: {ex.Message}", ex);
        }
    }

    public async Task<Guid> CreateAsync(CreateProductRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("/api/products", request, JsonOptions, ct);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<ProductDto>(JsonOptions, ct);
            return created?.Id ?? Guid.Empty;
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Failed to create product: {ex.Message}", ex);
        }
    }

    public async Task UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"/api/products/{id}", request, JsonOptions, ct);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Failed to update product: {ex.Message}", ex);
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await _http.DeleteAsync($"/api/products/{id}", ct);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Failed to delete product: {ex.Message}", ex);
        }
    }
}

public record PagedResponse<T>(IReadOnlyList<T> Items, int TotalCount);
public record ProductDto(Guid Id, string Name, string Description, decimal Price, int StockQuantity, string Category, bool IsActive);
public record CreateProductRequest(string Name, string Description, decimal Price, int StockQuantity, string Category);
public record UpdateProductRequest(string Name, string Description, decimal Price, int StockQuantity, string Category, bool IsActive);
