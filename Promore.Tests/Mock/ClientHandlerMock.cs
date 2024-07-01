// using Promore.Core;
// using Promore.Core.Handlers;
// using Promore.Core.Models;
// using Promore.Core.Requests.Clients;
// using Promore.Core.Responses.Clients;
//
// namespace Promore.Tests.Mock;
//
// public class ClientHandlerMock(MockContext context) : IClientHandler
// {
//     public Task<OperationResult<List<GetClientsResponse>>> GetAllAsync(GetAllClientsRequest request)
//     {
//         var clients = context.Clients
//             .Select(client => new GetClientsResponse
//             (
//                 client.Id,
//                 client.Name,
//                 client.Cpf,
//                 client.Phone,
//                 client.MothersName,
//                 client.BirthdayDate,
//                 client.LotId
//             ))
//             .ToList();
//         
//         return Task.FromResult(OperationResult<List<GetClientsResponse>>.SuccessResult(clients));
//     }
//
//     public Task<OperationResult<List<GetClientsByLotIdResponse>>> GetAllByLotIdAsync(GetAllClientsByLotIdRequest request)
//     {
//         var client = context.Clients
//             .Where(x => x.LotId == request.LotId)
//             .Select(client => new GetClientsByLotIdResponse
//             (
//                 client.Id,
//                 client.Name,
//                 client.Cpf,
//                 client.Phone,
//                 client.MothersName,
//                 client.BirthdayDate,
//                 client.LotId
//             ))
//             .ToList();
//         
//         return Task.FromResult(OperationResult<List<GetClientsByLotIdResponse>>.SuccessResult(client));
//     }
//     
//     
//     public Task<OperationResult<GetClientByIdResponse>> GetClientByIdAsync(GetClientByIdRequest request)
//     {
//         var client = context
//             .Clients
//             .Where(x => x.Id == request.Id)
//             .Select(client => new GetClientByIdResponse
//             (
//                 client.Id,
//                 client.Name,
//                 client.Cpf,
//                 client.Phone,
//                 client.MothersName,
//                 client.BirthdayDate,
//                 client.LotId
//             ))
//             .FirstOrDefault()!;
//         
//         return Task.FromResult(OperationResult<GetClientByIdResponse>.SuccessResult(client));
//     }
//
//     public Task<OperationResult<long>> CreateAsync(CreateClientRequest request)
//     {
//         client.Id = context.Clients.Max(x => x.Id) + 1;
//         context.Clients.Add(client);
//         return Task.FromResult(Convert.ToInt64(client.Id));
//     }
//
//     public Task<OperationResult> UpdateAsync(UpdateClientRequest request)
//     {
//         var clientSaved = context.Clients.FirstOrDefault(x => x.Id == client.Id);
//         context.Clients.Remove(clientSaved);
//         context.Clients.Add(client);
//         return Task.FromResult(1);
//     }
//
//     public Task<OperationResult> DeleteAsync(DeleteClientRequest request)
//     {
//         var client = context.Clients.FirstOrDefault(x => x.Id == id);
//         if (client is null)
//             return Task.FromResult(0);
//
//         context.Clients.Remove(client);
//         return Task.FromResult(1);
//     }
// }