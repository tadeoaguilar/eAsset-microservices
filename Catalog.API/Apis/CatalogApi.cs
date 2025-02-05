using System.ComponentModel;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Apis
{
    public static class CatalogApi
    {
        public static IEndpointRouteBuilder MapCatalogApi(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/catalog/assets", GetAllAssets)
            .WithName("GeAlltAsset")
            .WithSummary("Get all Assets")
            .WithDescription("Get all assets from the catalog")
            .WithTags("All assets");

            app.MapGet("/api/catalog/assets/{cd}", GetAssetByCD)
            .WithName("GetAssetbyCD")
            .WithSummary("Get Asset by CD")
            .WithDescription("Get an asset from the catalog")
            .WithTags("Asset by CD");

            app.MapPost("/api/catalog/assets", CreateAsset);
            return app;
        }
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    public static async Task<Ok<PaginatedItems<Asset>>> GetAllAssets(
        [AsParameters] PaginationRequest paginationRequest,
        [AsParameters] CatalogServices services,
        [Description("The name of the assets to return")] string? assetCD,
        [Description("The subNumber of assets to return")] int? subNumber,
        [Description("The description of assets to return")] string? description)
    {
        var pageSize = paginationRequest.PageSize;
        var pageIndex = paginationRequest.PageIndex;

        var root = (IQueryable<Asset>)services.Context.Asset;

       if (assetCD is not null)
        {
            root = root.Where(c => c.AssetCD.StartsWith(assetCD));
        }
        if (subNumber is not null)
        {
            root = root.Where(c => c.SubNumber == subNumber);
        }
        if (description is not null)
        {
            root = root.Where(c => c.Description != null && c.Description.StartsWith(description));
        }

        var totalAssets = await root
            .LongCountAsync();

        var assetsOnPage = await root         
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return TypedResults.Ok(new PaginatedItems<Asset>(pageIndex, pageSize, totalAssets, assetsOnPage));
    }        

      [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    public static async Task<Results<Ok<Asset>, NotFound, BadRequest<ProblemDetails>>> GetAssetByCD(
        HttpContext httpContext,
        [AsParameters] CatalogServices services,
        [Description("The asset cd")] string cd)
    {
        if (cd.Length <= 0)
        {
            return TypedResults.BadRequest<ProblemDetails>(new (){
                Detail = "Cd is not valid"
            });
        }

        var item = await services.Context.Asset.SingleOrDefaultAsync(a => a.AssetCD == cd);

        if (item == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(item);
    }

        public static async Task<Created> CreateAsset(
        [AsParameters] CatalogServices services,
        Asset asset)
    {
        var item = new Asset
        {
            Id = new Guid(),
            AssetCD = asset.AssetCD,
            SubNumber = asset.SubNumber,
            LocationID =asset.LocationID,
            CostCenterID= asset.CostCenterID,
            Description = asset.Description,
            ManufacturerID = asset.ManufacturerID,
            Active = asset.Active,
            LastModified = DateTime.Now.ToUniversalTime(),
            CreatedAt = DateTime.Now.ToUniversalTime()

            
        };
        

        services.Context.Asset.Add(item);
        await services.Context.SaveChangesAsync();

        return TypedResults.Created($"/api/catalog/assets/{item.Id}");
    }
    
    }

    
}
