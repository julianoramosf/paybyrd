using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Paybyrd.Proof.Bussiness.Interfaces;
using Paybyrd.Proof.Bussiness.Infrastructure.Cache;
using Paybyrd.Proof.Bussiness.Models;
using Paybyrd.Proof.Bussiness.Utils;
using Paybyrd.Proof.Bussiness.Constants;

namespace Paybyrd.Proof.Bussiness.Service;

public class DiffService : IDiffService
{
    public void SaveDataObject(Guid id, string side, string data)
    {
        try
        {
            var blob = Convert.FromBase64String(data);
            string json = Encoding.UTF8.GetString(blob);
            JObject dataObject = JsonConvert.DeserializeObject<JObject>(json) ?? new JObject();

            var model = MemCache.Exists(id.ToString()) ? MemCache.Get<DiffDataModel>(id.ToString()) : new DiffDataModel(id.ToString());
            model.WriteJObject(side, dataObject);
            MemCache.UpdateOrAdd<DiffDataModel>(id.ToString(), model);

        }
        catch
        {
            throw new ApplicationException(Messages.SAVE_DATA_ERROR);
        }
    }

    public ResponseDiffDataModel? ProcessResult(Guid id)
    {
        try
        {
            ResponseDiffDataModel result = new ResponseDiffDataModel();
            DiffDataModel model = MemCache.Get<DiffDataModel>(id.ToString());

            if (JsonUtils.Equals(model.Left, model.Right))
            {
                result.Message = Messages.JSON_EQUALS;
            }
            else if (!JsonUtils.SizeEquals(model.Left, model.Right))
            {
                result.Message = Messages.JSON_SIZE_NOT_EQUALS;
            }
            else
            {
                var listDiff = JsonUtils.GetFieldListDiffs(model.Left, model.Right);
                result.ListDiffs = listDiff;
            }

            return result;

        }
        catch(System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new ApplicationException(Messages.PROCESS_ERROR);
        }
    }

}