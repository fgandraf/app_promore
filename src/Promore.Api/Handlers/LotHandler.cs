using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Core;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Lots;
using Promore.Core.Responses.Lots;

namespace Promore.Api.Handlers;

public class LotHandler(PromoreDataContext context) : ILotHandler
{
    public async Task<OperationResult<GetLotByIdResponse>> GetByIdAsync(GetLotByIdRequest request)
    {
        var lot = await context
            .Lots
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Include(clients => clients.Clients)
            .Select(lot => new GetLotByIdResponse
            (
                lot.Id,
                lot.Block,
                lot.Number,
                lot.SurveyDate,
                lot.LastModifiedDate,
                lot.Status,
                lot.Comments,
                lot.UserId,
                lot.RegionId,
                lot.Clients.Select(x=> x.Id).ToList()
            ))
            .FirstOrDefaultAsync();
        
        return OperationResult<GetLotByIdResponse>.SuccessResult(lot);
    }
    
    public async Task<OperationResult<List<GetStatusByRegionResponse>>> GetStatusByRegionAsync(GetLotsStatusByRegionIdRequest request)
    {
        var regions = await context
            .Lots
            .Include(x => x.Region)
            .Where(x => x.Region.Id == request.RegionId)
            .AsNoTracking()
            .Select(lot => new GetStatusByRegionResponse
            (
                lot.Id,
                lot.Status,
                lot.UserId
            ))
            .ToListAsync();
        
        return OperationResult<List<GetStatusByRegionResponse>>.SuccessResult(regions);
    }

    public async Task<OperationResult<int>> CreateAsync(CreateLotRequest request)
    {
        var user = await context
            .Users
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .FirstOrDefaultAsync(x => x.Id == request.UserId);
        
        var region = await context
            .Regions
            .FirstOrDefaultAsync(x => x.Id == request.RegionId);
        
        var lot = new Lot
        {
            Id = request.Id,
            Block = request.Block,
            Number = request.Number,
            SurveyDate = request.SurveyDate,
            LastModifiedDate = request.LastModifiedDate,
            Status = request.Status,
            Comments = request.Comments,
            User = user,
            Region = region
        };
        
        context.Lots.Add(lot);
        var rowsAffected = await context.SaveChangesAsync();
        var id = rowsAffected > 0 ? lot.Id : 0;

        return id != 0 ? OperationResult<int>.SuccessResult(id) : OperationResult<int>.FailureResult("Não foi possível inserir o lote!");
    }

    public async Task<OperationResult> UpdateAsync(UpdateLotRequest request)
    {
        var lot = await context
            .Lots
            .Include(x => x.User)
            .Include(x => x.Region)
            .Include(x => x.Clients)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        if (lot is null)
            return OperationResult.FailureResult($"Lote '{request.Id}' não encontrado!");
        
        var user = await context
            .Users
            .Include(roles => roles.Roles)
            .Include(regions => regions.Regions)
            .Include(lots => lots.Lots)
            .FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (user is null)
            return OperationResult.FailureResult($"Usuário '{request.UserId}' não encontrado!");
        
        var region = await context
            .Regions
            .FirstOrDefaultAsync(x => x.Id == request.RegionId);
        if (region is null)
            return OperationResult.FailureResult($"Região '{request.RegionId}' não encontrada!");
        
        var clients = await context
             .Clients
             .Where(client => request.Clients.Contains(client.Id))
             .ToListAsync();
        if (clients.Count == 0)
            return OperationResult.FailureResult($"Clientes não encontrados!");
        
        lot.Block = request.Block;
        lot.Number = request.Number;
        lot.SurveyDate = request.SurveyDate;
        lot.LastModifiedDate = request.LastModifiedDate;
        lot.Status = request.Status;
        lot.Comments = request.Comments;
        lot.User = user;
        lot.Region = region;
        lot.Clients = clients;
        
        context.Update(lot);
        var rowsAffected = await context.SaveChangesAsync();

        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível alterar o lote!");
    }

    public async Task<OperationResult> DeleteAsync(DeleteLotRequest request)
    {
        var lot = await context.Lots.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (lot is null)
            return OperationResult.FailureResult("Não foi possível apagar o lote!");

        context.Lots.Remove(lot);
        var rowsAffected = await context.SaveChangesAsync();
        
        return rowsAffected > 0 ? OperationResult.SuccessResult("Lote removido!") : OperationResult.FailureResult("Não foi possível apagar o lote!");
    }
}