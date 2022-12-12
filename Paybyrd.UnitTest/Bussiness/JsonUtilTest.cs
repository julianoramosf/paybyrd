using Newtonsoft.Json.Linq;
using Paybyrd.Proof.Bussiness.Infrastructure.Cache;
using Paybyrd.Proof.Bussiness.Models;
using Paybyrd.Proof.Bussiness.Utils;

namespace Paybyrd.UnitTest;

[TestClass]
public class JsonUtilTest
{
    private static JObject? ObjJson_A { get; set; }
    private static JObject? ObjJson_B { get; set; }
    private static JObject? ObjJson_C { get; set; }
    private static JObject? ObjJson_D { get; set; }
    private static JObject? ObjJson_E { get; set; }

    [ClassInitialize()]
    public static void ClassInitialize(TestContext testContext)
    {
        ObjJson_A = JObject.Parse(@"{'Name': 'João','Age': 41,'State': 'Rio de Janeiro'}");
        ObjJson_B = JObject.Parse(@"{'Name': 'João','Age': 41,'State': 'Rio de Janeiro'}");
        ObjJson_C = JObject.Parse(@"{'Name': 'Eduardo','Age': 40,'City': 'Rio de Janeiro'}");
        ObjJson_D = JObject.Parse(@"{'Name': 'Amanda','Age': 40,'City': 'Rio de Janeiro'}");
        ObjJson_E = JObject.Parse(@"{'Name': 'João','Age': 41,'City': 'Rio de Janeiro','State': 'RJ'}");
    }

    [TestMethod]
    public void Should_Return_True_With_Equals_Objects()
    {
        var validate = JsonUtils.Equals(ObjJson_A, ObjJson_B);
        Assert.IsTrue(validate);
    }

    [TestMethod]
    public void Should_Return_False_With_Equals_Objects()
    {
        Assert.IsFalse(JsonUtils.Equals(ObjJson_A, ObjJson_C));
    }

    [TestMethod]
    public void Should_Return_True_With_Equals_Size_Objects()
    {
        Assert.IsTrue(JsonUtils.SizeEquals(ObjJson_B, ObjJson_C));
    }

    [TestMethod]
    public void Should_Return_False_With_Not_Equal_Size_Objects()
    {
        Assert.IsFalse(JsonUtils.SizeEquals(ObjJson_C, ObjJson_E));
    }

    [TestMethod]
    public void Should_Return_Exists_ListDiffs_Value()
    {
        ListDiffsModel listDiffs = JsonUtils.GetFieldListDiffs(ObjJson_C, ObjJson_D);
        var existDiffValues = listDiffs.Values != null && listDiffs.Values.Count > 0;
        Assert.IsTrue(existDiffValues);
    }

    [TestMethod]
    public void Should_Return_Exists_ListDiffs_Properties()
    {
        ListDiffsModel listDiffs = JsonUtils.GetFieldListDiffs(ObjJson_B, ObjJson_C);
        var existDiffProperties = listDiffs.Properties != null && listDiffs.Properties.Count > 0;
        Assert.IsTrue(existDiffProperties);
    }

}