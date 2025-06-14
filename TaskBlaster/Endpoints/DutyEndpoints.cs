using TaskBlaster.Interfaces;
using TaskBlaster.Models;

namespace TaskBlaster.Endpoints
{
    public static class DutyEndpoints
    {
        public static void MapDutyEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/duties")
                .WithTags(nameof(Duty));

            group.MapGet("/", async (HttpContext context, IDutyService dutyService) =>
            {
                var uid = GetUid(context);
                var duties = await dutyService.GetAllDutiesAsync(uid);
                return Results.Ok(duties);
            });

            group.MapGet("/{id}", async (HttpContext context, IDutyService dutyService, int id) =>
            {
                var uid = GetUid(context);
                var duty = await dutyService.GetDutyByIdAsync(id, uid);
                return duty is not null ? Results.Ok(duty) : Results.NotFound();
            });

            group.MapPost("/", async (HttpContext context, IDutyService dutyService, Duty duty) =>
            {
                var uid = GetUid(context);
                var created = await dutyService.CreateDutyAsync(duty, uid);
                return Results.Created($"/api/duties/{created.Id}", created);
            });

            group.MapPut("/{id}", async (HttpContext context, IDutyService dutyService, int id, Duty duty) =>
            {
                var uid = GetUid(context);
                var updated = await dutyService.UpdateDutyAsync(id, duty, uid);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            group.MapDelete("/{id}", async (HttpContext context, IDutyService dutyService, int id) =>
            {
                var uid = GetUid(context);
                var success = await dutyService.DeleteDutyAsync(id, uid);
                return success ? Results.NoContent() : Results.NotFound();
            });

            group.MapGet("/category/{categoryId}", async (HttpContext context, IDutyService dutyService, int categoryId) =>
            {
                var uid = GetUid(context);
                var duties = await dutyService.GetDutiesByCategoryIdAsync(categoryId, uid);
                return Results.Ok(duties);
            });

            group.MapPut("/toggle-complete/{id}", async (HttpContext context, IDutyService dutyService, int id) =>
            {
                var uid = GetUid(context);
                var success = await dutyService.ToggleDutyCompletionAsync(id, uid);
                return success ? Results.Ok() : Results.NotFound();
            });
        }

        private static string GetUid(HttpContext context)
        {
            // Use header if available, otherwise fallback to test UID
            return context.Request.Headers["uid"].ToString() ?? "a";
        }
    }
}
