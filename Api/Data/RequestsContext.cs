using Microsoft.EntityFrameworkCore;
using RequestService.Common.Models;
using Route = RequestService.Common.Models.Route;

namespace RequestService.Api.Data;

public sealed class RequestsContext : DbContext
{
    public DbSet<Request>? Requests { get; set; }
    public DbSet<Route>? Routes { get; set; }

    public RequestsContext(DbContextOptions<RequestsContext> options): base(options)
    {
    }
}