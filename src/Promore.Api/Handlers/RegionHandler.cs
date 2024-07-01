using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Regions;
using Promore.Core.Responses.Regions;

namespace Promore.Api.Handlers;

public class RegionHandler(PromoreDataContext context) : IRegionHandler
{
    public async Task<OperationResult<List<GetRegionsResponse>>> GetAllAsync(GetAllRegionsRequest request)
    {
        var regions = await context
            .Regions
            .AsNoTracking()
            .Select(region => new GetRegionsResponse
            (
                region.Id,
                region.Name,
                region.EstablishedDate,
                region.StartDate,
                region.EndDate
            ))
            .ToListAsync();
        
        if (regions.Count == 0)
            return OperationResult<List<GetRegionsResponse>>.FailureResult("Nenhuma região cadastrada!");
        
        return OperationResult<List<GetRegionsResponse>>.SuccessResult(regions);
    }

    public async Task<OperationResult<GetRegionsByIdResponse>> GetByIdAsync(GetRegionByIdRequest request)
    {
        var region = await context
            .Regions
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(region => new GetRegionsByIdResponse
            (
                region.Id,
                region.Name,
                region.EstablishedDate,
                region.StartDate,
                region.EndDate
            ))
            .FirstOrDefaultAsync();
        
        if (region is null)
            return OperationResult<GetRegionsByIdResponse>.FailureResult($"Região '{request.Id}' não encontrada!");
        
        return OperationResult<GetRegionsByIdResponse>.SuccessResult(region);
    }

    public async Task<OperationResult<long>> CreateAsync(CreateRegionRequest request)
    {
        var users = await context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .Include(x => x.Regions)
            .Include(x => x.Lots)
            .Where(user => request.Users.Contains(user.Id))
            .ToListAsync();
        
        var region = new Region
        {
            Name = request.Name,
            EstablishedDate = request.EstablishedDate,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Users = users
        };
        
        context.Regions.Add(region);
        var rowsAffected = await context.SaveChangesAsync();
        var id = rowsAffected > 0 ? region.Id : 0;
        
        return id > 0 ? OperationResult<long>.SuccessResult(id) : OperationResult<long>.FailureResult("Não foi possível inserir a região!");
    }

    public async Task<OperationResult> UpdateAsync(UpdateRegionRequest request)
    {
        var region = await context
            .Regions
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        
        region.Name = request.Name;
        region.EstablishedDate = request.EstablishedDate;
        region.StartDate = request.StartDate;
        region.EndDate = request.EndDate;
        
        context.Update(region);
        var rowsAffected = await context.SaveChangesAsync();

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar a região!");
    }

    public async Task<OperationResult> DeleteAsync(DeleteRegionRequest request)
    {
        var region = await context.Regions.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (region is null)
            return OperationResult.FailureResult("Não foi possível apagar a região!");

        context.Remove(region);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult("Região removida!") : OperationResult.FailureResult("Não foi possível apagar a região!");
    }
}