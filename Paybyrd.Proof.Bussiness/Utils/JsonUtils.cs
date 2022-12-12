using Newtonsoft.Json.Linq;
using Paybyrd.Proof.Bussiness.Models;

namespace Paybyrd.Proof.Bussiness.Utils;

/// <summary>
/// Utilitary class to provide required methods to compare and list differences from a Json Data Structure.
/// </summary>
public static class JsonUtils
{
    public static bool Equals(JObject left, JObject right)
    {
        return JToken.DeepEquals(left, right);
    }
    public static bool SizeEquals(JObject left, JObject right)
    {
        return int.Equals(left.Count, right.Count);
    }

    public static ListDiffsModel GetFieldListDiffs(JObject left, JObject right)
    {
        var leftJProperties = left.Properties().ToList();
        var rightJProperties = right.Properties().ToList();

        List<string> listPropertiesDiff = new List<string>();
        List<string> listValuesDiff = new List<string>();

        foreach (JProperty leftProperty in leftJProperties)
        {
            var getRightProperty = rightJProperties.FirstOrDefault(rp => rp.Name == leftProperty.Name);
            if (getRightProperty == null)
            {
                listPropertiesDiff.Add(leftProperty.Name);
            }
            else if (getRightProperty.Value.ToString() != leftProperty.Value.ToString())
            {
                listValuesDiff.Add(leftProperty.Name);
            }
        }

        foreach (JProperty rightProperty in rightJProperties)
        {
            var getLeftProperty = leftJProperties.FirstOrDefault(rp => rp.Name == rightProperty.Name);
            if (getLeftProperty == null)
            {
                listPropertiesDiff.Add(rightProperty.Name);
            }
            else if (getLeftProperty.Value.ToString() != rightProperty.Value.ToString())
            {
                if (!listValuesDiff.Any(dvalue => dvalue == rightProperty.Name))
                    listValuesDiff.Add(rightProperty.Name);
            }
        }

        return new ListDiffsModel(listPropertiesDiff, listValuesDiff);

    }

}
