namespace Paybyrd.Proof.Bussiness.Models;

public class ListDiffsModel
{
    public List<string>? Properties { get; set; }
    public List<string>? Values { get; set; }

    public ListDiffsModel(List<string> listPropertiesDiff, List<string> listValuesDiff)
    {
        Properties = listPropertiesDiff.Count > 0 ? listPropertiesDiff : null;
        Values = listValuesDiff.Count > 0 ? listValuesDiff : null;
    }

}