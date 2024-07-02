using Microsoft.EntityFrameworkCore;
using Promore.Api.Data;
using Promore.Api.Data.Contexts;
using Promore.Core.Handlers;
using Promore.Core.Models;
using Promore.Core.Requests.Lots;
using Promore.Core.Responses;
using Promore.Core.Responses.Lots;

namespace Promore.Api.Handlers;

public class LotHandler(PromoreDataContext context) : ILotHandler
{
    public async Task<Response<Lot?>> CreateAsync(CreateLotRequest request)
    {
        try
        {
            var user = await context
                .Users
                .Include(roles => roles.Roles)
                .Include(regions => regions.Regions)
                .Include(lots => lots.Lots)
                .FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user is null)
                return new Response<Lot?>(null, 404, "Usuário não encontrado!");
        
            var region = await context
                .Regions
                .FirstOrDefaultAsync(x => x.Id == request.RegionId);
            if (region is null)
                return new Response<Lot?>(null, 404, "Região não encontrada!");
        
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
        
            await context.Lots.AddAsync(lot);
            await context.SaveChangesAsync();
            
            return new Response<Lot?>(lot, 201, "Lote criado com sucesso!");
        }
        catch
        {
            return new Response<Lot?>(null, 500, "[AHLCR12] Não foi possível criar o lote!");
        }
        
    }

    public async Task<Response<Lot?>> UpdateAsync(UpdateLotRequest request)
    {
        try
        {
            var lot = await context
                .Lots
                .Include(x => x.User)
                .Include(x => x.Region)
                .Include(x => x.Clients)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (lot is null)
                return new Response<Lot?>(null, 404, $"Lote '{request.Id}' não encontrado!");
        
            var user = await context
                .Users
                .Include(roles => roles.Roles)
                .Include(regions => regions.Regions)
                .Include(lots => lots.Lots)
                .FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user is null)
                return new Response<Lot?>(null, 404, $"Usuário '{request.UserId}' não encontrado!");
        
            var region = await context
                .Regions
                .FirstOrDefaultAsync(x => x.Id == request.RegionId);
            if (region is null)
                return new Response<Lot?>(null, 404, $"Região '{request.RegionId}' não encontrada!");
        
            var clients = await context
                .Clients
                .Where(client => request.Clients.Contains(client.Id))
                .ToListAsync();
            if (clients.Count == 0)
                return new Response<Lot?>(null, 404, $"Clientes não encontrados!");
        
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
            await context.SaveChangesAsync();

            return new Response<Lot?>(lot);
        }
        catch
        {
            return new Response<Lot?>(null, 500, "[AHLUP12] Não foi possível alterar o lote");
        }
        
    }
    
    public async Task<Response<Lot?>> DeleteAsync(DeleteLotRequest request)
    {
        try
        {
            var lot = await context.Lots.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (lot is null)
                return new Response<Lot?>(null, 404, $"Lote '{request.Id}' não encontrado!");

            context.Lots.Remove(lot);
            await context.SaveChangesAsync();
            
            return new Response<Lot?>(null, 200, $"Lote {request.Id} removido com sucesso!");
        }
        catch
        {
            return new Response<Lot?>(null, 500, "[AHLDE12] Não foi possível excluir o lote!");
        }
        
    }
    
    public async Task<Response<Lot?>> GetByIdAsync(GetLotByIdRequest request)
    {
        try
        {
            var lot = await context
                .Lots
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Include(clients => clients.Clients)
                .FirstOrDefaultAsync();
            
            return lot is null 
                ? new Response<Lot?>(null, 404, $"Lote '{request.Id}' não encontrado!") 
                : new Response<Lot?>(lot);
        }
        catch
        {
            return new Response<Lot?>(null, 500, "[AHLGT12] Não foi possível encontrar o lote!");
        }
    }
    
    public async Task<Response<List<LotsStatusResponse>?>> GetAllStatusByRegionIdAsync(GetLotsStatusByRegionIdRequest request)
    {
        try
        {
            var lots = await context
                .Lots
                .Include(x => x.Region)
                .Where(x => x.Region.Id == request.RegionId)
                .AsNoTracking()
                .Select(lot => new LotsStatusResponse
                (
                    lot.Id,
                    lot.Status,
                    lot.UserId
                ))
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            return new Response<List<LotsStatusResponse>?>(lots);
        }
        catch
        {
            return new Response<List<LotsStatusResponse>?>(null, 500, "[AHLGA12] Não foi possível encontrar o lote!");
        }
    }
}