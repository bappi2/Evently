using Evently.Modules.Events.Api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Api.Events;

public static class GetEvent
{
    public static void MapEndpoint (IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/events/{id}", async (Guid Id, EventsDbContext context) =>
        {
           EventResponse eventResponse = await context.Events
                .Where(e => e.Id == Id)
                .Select(e => new EventResponse(e.Id, e.Title, e.Description, e.Location, e.StartsAtUtc, e.EndsAtUtc, e.Status))
                .SingleOrDefaultAsync();

            return eventResponse is not null
                ? Results.Ok(eventResponse)
                : Results.NotFound();
        })
        .WithTags(Tags.Events);
    }
}


