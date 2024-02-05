using BookManagement.RestServer.Models;
using BookManagement.Shared.Dto;

namespace BookManagement.RestServer.Helpers;

public static class PaginationHelper
{
    public static PagedResponseDto<T> CreatePagedResponse<T>(IEnumerable<T> data,
        PaginationProperties paginationProperties, PaginationFilter filter, string baseRoute,
        IQueryCollection queryParams)
    {
        var totalPages = (int)Math.Ceiling((double)paginationProperties.TotalCount / filter.PageSize);
        var routeWithQuery = baseRoute + CreateQueryString(filter, queryParams);

        var beforeIdParam = $"{nameof(filter.BeforeId).FirstCharToLower()}";
        var afterIdParam = $"{nameof(filter.AfterId).FirstCharToLower()}";

        return new PagedResponseDto<T>
        {
            PageSize = filter.PageSize,
            TotalPages = totalPages,
            TotalRecords = paginationProperties.TotalCount,
            FirstPageUrl = routeWithQuery,
            LastPageUrl = totalPages == 1
                ? routeWithQuery
                : $"{routeWithQuery}&{beforeIdParam}={paginationProperties.LastRecordId + 1}",
            NextPageUrl = paginationProperties.HasNextPage
                ? $"{routeWithQuery}&{afterIdParam}={paginationProperties.LastElementId}"
                : null,
            PreviousPageUrl = paginationProperties.HasPreviousPage
                ? $"{routeWithQuery}&{beforeIdParam}={paginationProperties.FirstElementId}"
                : null,
            Data = data
        };
    }

    public static PagedResponseDto<T> CreateEmptyPagedResponse<T>(PaginationFilter filter, string baseRoute,
        IQueryCollection queryParams)
    {
        var routeWithQuery = baseRoute + CreateQueryString(filter, queryParams);
        return new PagedResponseDto<T>
        {
            PageSize = filter.PageSize,
            TotalPages = 0,
            TotalRecords = 0,
            FirstPageUrl = routeWithQuery,
            LastPageUrl = routeWithQuery,
            Data = Enumerable.Empty<T>()
        };
    }

    private static string CreateQueryString(PaginationFilter filter, IQueryCollection queryParams)
    {
        var queryString = $"?{nameof(filter.PageSize).FirstCharToLower()}={filter.PageSize}";
        if (queryParams.Count == 0)
        {
            return queryString;
        }

        var queryKeysToRemove = new HashSet<string>
            { nameof(filter.AfterId).ToLower(), nameof(filter.BeforeId).ToLower(), nameof(filter.PageSize).ToLower() };
        var additionalQueryParams = queryParams.Where(x => !queryKeysToRemove.Contains(x.Key.ToLower())).ToList();

        if (additionalQueryParams.Count == 0)
        {
            return queryString;
        }

        var additionalParamsString = "&" + string.Join("&", additionalQueryParams.Select(x => $"{x.Key}={x.Value}"));
        return queryString + additionalParamsString;
    }
}