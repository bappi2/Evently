using Evently.Modules.Events.Api.Database;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace Evently.Modules.Events.Api.Events;

public static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/events", async (EventRequest request, EventsDbContext dbContext) =>
        {
            var newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                StartsAtUtc = request.StartsAtUtc,
                EndsAtUtc = request.EndsAtUtc,
                Status = EventStatus.Draft
            };

            dbContext.Events.Add(newEvent);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/events/{newEvent.Id}", newEvent);
        })
        .WithTags(Tags.Events);
    }
}


