// using Promore.Core.Handlers;
// using Promore.Core.Models;
// using Promore.Core.Responses.Regions;
//
// namespace Promore.Tests.Mock;
//
// public class RegionHandlerMock(MockContext context) : IRegionHandler
// {
//     public Task<List<GetRegionsResponse>> GetAll()
//     {
//         var regions = context.Regions
//             .Select(region => new GetRegionsResponse
//             (
//                 region.Id,
//                 region.Name,
//                 region.EstablishedDate,
//                 region.StartDate,
//                 region.EndDate
//             ))
//             .ToList();
//
//         return Task.FromResult(regions);
//     }
//
//     public Task<List<Region>> GetRegionsByIdListAsync(List<int> regionsId)
//     {
//         var regions = context.Regions
//             .Where(region => regionsId.Contains(region.Id))
//             .ToList();
//
//         return Task.FromResult(regions);
//     }
//     
//     public Task<Region> GetRegionByIdAsync(int id)
//     {
//         var region = context.Regions
//             .FirstOrDefault(x => x.Id == id);
//
//         if (region is null)
//             return null;
//         
//         var users = context.Users
//             .Where(x => x.Regions.Any(r => r.Id == id))
//             .ToList();
//         
//         var lots = context.Lots
//             .Where(x => x.RegionId == id)
//             .ToList();
//         
//         region.Users = users;
//         region.Lots = lots;
//         
//         return Task.FromResult(region);
//     }
//
//     public Task<GetRegionsByIdResponse> GetByIdAsync(int id)
//     {
//         var region = context.Regions
//             .Select(region => new GetRegionsByIdResponse
//             (
//                 region.Id,
//                 region.Name,
//                 region.EstablishedDate,
//                 region.StartDate,
//                 region.EndDate
//             ))
//             .FirstOrDefault(x => x.Id == id);
//
//         return Task.FromResult(region);
//     }
//
//     public Task<long> InsertAsync(Region region)
//     {
//         region.Id = context.Regions.Max(x => x.Id) + 1;
//         context.Regions.Add(region);
//         return Task.FromResult(Convert.ToInt64(region.Id));
//     }
//     
//     public Task<int> UpdateAsync(Region region)
//     {
//         var regionSaved = context.Regions.FirstOrDefault(x => x.Id == region.Id);
//         context.Regions.Remove(regionSaved);
//         context.Regions.Add(region);
//         return Task.FromResult(1);
//     }
//
//     public Task<int> DeleteAsync(int id)
//     {
//         var region = context.Regions.FirstOrDefault(x => x.Id == id);
//         if (region is null)
//             return Task.FromResult(0);
//
//         var result = context.Regions.Remove(region);
//         return result ? Task.FromResult(1) : Task.FromResult(0);
//     }
// }