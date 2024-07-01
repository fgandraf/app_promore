using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Regions;
using Promore.Core.Responses;
using Promore.Core.Responses.Regions;

namespace Promore.Api.Handlers;

public class RegionHandler(PromoreDataContext context) : IRegionHandler
{
    
    public async Task<Response<Region?>> CreateAsync(CreateRegionRequest request)
    {
        try
        {
            var users = await context
                .Users
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

            await context.Regions.AddAsync(region);
            await context.SaveChangesAsync();

            return new Response<Region?>(region, 201, "Região criada com sucesso!");
        }
        catch
        {
            return new Response<Region?>(null, 500, "[AHRCR12] Não foi possível criar a região!");
        }
    }

    public async Task<Response<Region?>> UpdateAsync(UpdateRegionRequest request)
    {
        try
        {
            var region = await context
                .Regions
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (region is null)
                return new Response<Region?>(null, 404, $"Lote '{request.Id}' não encontrado!");
        
            region.Name = request.Name;
            region.EstablishedDate = request.EstablishedDate;
            region.StartDate = request.StartDate;
            region.EndDate = request.EndDate;
        
            context.Update(region);
            await context.SaveChangesAsync();
            
            return new Response<Region?>(region);
        }
        catch
        {
            return new Response<Region?>(null, 500, "[AHRUP12] Não foi possível alterar a região!");
        }
        
    }
    
    public async Task<Response<Region?>> DeleteAsync(DeleteRegionRequest request)
    {
        try
        {
            var region = await context.Regions.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (region is null)
                return new Response<Region?>(null, 404, $"Região '{request.Id}' não encontrada!");

            context.Remove(region);
            await context.SaveChangesAsync();
        
            return new Response<Region?>(null, 200, $"Região {request.Id} removida com sucesso!");
        }
        catch
        {
            return new Response<Region?>(null, 500, "[AHRDE12] Não foi possível excluir a região!");
        }
        
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
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

    

    
}