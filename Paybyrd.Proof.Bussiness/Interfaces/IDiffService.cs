using Paybyrd.Proof.Bussiness.Models;

namespace Paybyrd.Proof.Bussiness.Interfaces;

public interface IDiffService
{
    void SaveDataObject(Guid id, string side, string data);
    ResponseDiffDataModel? ProcessResult(Guid id);
}