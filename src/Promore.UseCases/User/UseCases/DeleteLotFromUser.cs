using Promore.Core;
using Promore.Core.Contracts;

namespace Promore.UseCases.User.UseCases;

public class DeleteLotFromUser(IUserRepository userRepository)
{
    public async Task<OperationResult> Handle(int userId, string lotId)
    {
        var user = await userRepository.GetEntityByIdAsync(userId);
        if (user.Equals(new Core.Entities.User()))
            return OperationResult.FailureResult($"Usuário não encontrado ou não está ativo!");

        var lot = user.Lots.FirstOrDefault(x => x.Id == lotId);
        if (lot is null)
            return OperationResult.FailureResult($"Lote não vinculado ao usuário!");

        user.Lots.Remove(lot);

        var rowsAffected = await userRepository.UpdateAsync(user);
        return rowsAffected > 0 ? OperationResult.SuccessResult() : OperationResult.FailureResult("Não foi possível remover o lote do usuário!");
    }
}