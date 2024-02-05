using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.RestServer.Helpers;

public class KeysetPagination<T>(IQueryable<T> query, Expression<Func<T, long>> pkExpression, int pageSize)
    where T : class, new()
{
    private readonly Func<T, long> _pkFunc = pkExpression.Compile();

    public async Task<IEnumerable<T>> GetPaginatedData(long id, bool forward = true)
    {
        var predicate = CreatePredicateExpression(id, forward);
        // when querying descending reverse for proper ordering
        var data = forward
            ? await query.Where(predicate).OrderBy(pkExpression).Take(pageSize).ToListAsync()
            : await query.Where(predicate).OrderByDescending(pkExpression).Take(pageSize).Reverse().ToListAsync();

        if (data.Count == 0 || forward || await HasNextPage(data))
        {
            return data;
        }

        // take only remaining items from last page
        var remainder = (int)(await TotalCount() % pageSize);

        return remainder == 0 ? data : data.TakeLast(remainder).ToList();
    }

    public async Task<long> TotalCount() => await query.CountAsync();

    public async Task<long> LastRecordIdValue() =>
        await query.OrderByDescending(pkExpression).Select(pkExpression).FirstOrDefaultAsync();

    public Task<bool> HasNextPage(IReadOnlyList<T> data)
    {
        if (!data.Any())
        {
            return Task.FromResult(false);
        }

        var idValue = _pkFunc(data[^1]);
        var predicate = CreatePredicateExpression(idValue);
        return HasPage(predicate);
    }

    public Task<bool> HasPreviousPage(IReadOnlyList<T> data)
    {
        if (!data.Any())
        {
            return Task.FromResult(false);
        }

        var idValue = _pkFunc(data[0]);
        var predicate = CreatePredicateExpression(idValue, false);
        return HasPage(predicate);
    }

    private Task<bool> HasPage(Expression<Func<T, bool>> predicate) => query.AnyAsync(predicate);

    private Expression<Func<T, bool>> CreatePredicateExpression(long id, bool forward = true)
    {
        var parameter = pkExpression.Parameters[0];
        return Expression.Lambda<Func<T, bool>>(
            forward
                ? Expression.GreaterThan(pkExpression.Body, Expression.Constant(id))
                : Expression.LessThan(pkExpression.Body, Expression.Constant(id)), parameter);
    }
}