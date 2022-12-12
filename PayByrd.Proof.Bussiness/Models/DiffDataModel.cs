using Newtonsoft.Json.Linq;
using PayByrd.Proof.Bussiness.Constants;

namespace PayByrd.Proof.Bussiness.Models;

public class DiffDataModel
{
    public string Id { get; set; }
    public JObject Left { get; set; }
    public JObject Right { get; set; }

    public DiffDataModel(string id)
    {
        Id = id;
        Left = new JObject();
        Right = new JObject();
    }

    public void WriteJObject(string side, JObject dataObject)
    {
        try
        {
            if (side.Equals(Global.LEFTDIFF))
                Left = dataObject;
            else
                Right = dataObject;
        }
        catch (System.Exception)
        {
            throw new ApplicationException(Messages.WRITE_JOBJECT_ERROR);
        }
    }


}