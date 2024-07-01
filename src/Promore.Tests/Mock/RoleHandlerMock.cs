// using Promore.Core.Handlers;
// using Promore.Core.Models;
//
// namespace Promore.Tests.Mock;
//
// public class RoleHandlerMock(MockContext context) : IRoleHandler
// {
//     public Task<List<Role>> GetRolesByIdListAsync(List<int> rolesId)
//     {
//         var roles = context.Roles
//             .Where(role => rolesId.Contains(role.Id))
//             .ToList();
//         
//         return Task.FromResult(roles);
//     }
// }