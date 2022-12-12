using Paybyrd.Proof.Bussiness.Infrastructure.Cache;

namespace Paybyrd.UnitTest;

[TestClass]
public class MemCacheTest
{

    [TestMethod]
    public void Get_ShouldReturnSucessWithInvalidKey()
    {
        object? checkGetCache = MemCache.Get<string>("KeyNotExist");
        Assert.IsNull(checkGetCache);
    }

    [TestMethod]
    public void Get_ShouldReturnSucessWithValidKey()
    {
        MemCache.UpdateOrAdd<string>("TestKey", "ValidKey");
        string getCache = MemCache.Get<string>("TestKey");
        Assert.AreEqual("ValidKey", getCache);
    }

}