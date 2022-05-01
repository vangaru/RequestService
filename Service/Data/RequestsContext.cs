﻿using Microsoft.EntityFrameworkCore;
using RequestService.Common;
using RequestService.Common.Models;

namespace RequestService.Data;

public sealed class RequestsContext : DbContext
{
    public DbSet<Request>? Requests { get; set; }
    public DbSet<Route>? Routes { get; set; }

    public RequestsContext(DbContextOptions<RequestsContext> options): base(options)
    {
        Database.EnsureCreated();
    }
}