// using Promore.Core.Handlers;
// using Promore.Core.Models;
// using Promore.Core.Responses.Lots;
//
// namespace Promore.Tests.Mock;
//
// public class LotHandlerMock(MockContext context) : ILotHandler
// {
//     public Task<List<GetStatusByRegionResponse>> GetStatusByRegion(int regionId)
//     {
//          var lots = context.Lots
//              .Where(x => x.RegionId == regionId)
//              .Select(lot => new GetStatusByRegionResponse
//              (
//                  lot.Id,
//                  lot.Status,
//                  lot.UserId
//              ))
//              .ToList();
//         
//          return Task.FromResult(lots);
//     }
//     
//     public Task<Lot> GetLotById(int id)
//     {
//         var lot = context.Lots
//             .FirstOrDefault(x => x.Id == id);
//         
//         return Task.FromResult(lot);
//     }
//     
//     public Task<GetLotByIdResponse> GetByIdAsync(int id)
//     {
//         var clients = context.Clients
//             .Where(x => x.LotId == id)
//             .Select(client => client.Id)
//             .ToList();
//         
//         var lot = context.Lots
//             .Select(lot => new GetLotByIdResponse
//             (
//                 lot.Id,
//                 lot.Block,
//                 lot.Number,
//                 lot.SurveyDate,
//                 lot.LastModifiedDate,
//                 lot.Status,
//                 lot.Comments,
//                 lot.UserId,
//                 lot.RegionId,
//                 clients
//             ))
//             .FirstOrDefault(x => x.Id == id);
//         
//         return Task.FromResult(lot);
//     }
//
//     public Task<int> InsertAsync(Lot lot)
//     {
//         context.Lots.Add(lot);
//         return Task.FromResult(lot.Id);
//     }
//
//     public Task<int> UpdateAsync(Lot lot)
//     {
//         var lotSaved = context.Lots.FirstOrDefault(x => x.Id == lot.Id);
//         context.Lots.Remove(lotSaved);
//         context.Lots.Add(lot);
//         return Task.FromResult(1);
//     }
//
//     public Task<int> DeleteAsync(int id)
//     {
//         var lot = context.Lots.FirstOrDefault(x => x.Id == id);
//         if (lot is null)
//             return Task.FromResult(0);
//
//         context.Lots.Remove(lot);
//         return Task.FromResult(1);
//     }
// }