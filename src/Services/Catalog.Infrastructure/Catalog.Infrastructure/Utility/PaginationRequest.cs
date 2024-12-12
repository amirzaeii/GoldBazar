namespace Catalog.Infrastructure;
public record PaginationRequest(int PageSize = 10, int PageIndex = 0);
