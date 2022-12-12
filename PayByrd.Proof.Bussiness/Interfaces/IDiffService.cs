using PayByrd.Proof.Bussiness.Models;

namespace PayByrd.Proof.Bussiness.Interfaces;

public interface IDiffService
{
    void SaveDataObject(Guid id, string side, string data);
    ResponseDiffDataModel? ProcessResult(Guid id);
}