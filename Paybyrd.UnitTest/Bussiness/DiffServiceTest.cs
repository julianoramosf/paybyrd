using System;
using System.Text;
using Paybyrd.Proof.Bussiness.Interfaces;
using Paybyrd.Proof.Bussiness.Service;
using Paybyrd.Proof.Bussiness.Constants;

namespace Paybyrd.UnitTest;

[TestClass]
public class DiffServiceTest
{
    private static string Base64Json_A { get; set; }
    private static string Base64Json_B { get; set; }
    private static string Base64Json_C { get; set; }
    private static string Base64Json_D { get; set; }
    private static string Base64Json_E { get; set; }
    private static IDiffService service;
    private static Guid Id {get;set;}

    [ClassInitialize()]
    public static void ClassInitialize(TestContext testContext)
    {
        service = new DiffService();
        Id = Guid.NewGuid();
        Base64Json_A = Convert.ToBase64String(Encoding.UTF8.GetBytes("{'Name': 'João','Age': 41,'State': 'Rio de Janeiro'}"));
        Base64Json_B = Convert.ToBase64String(Encoding.UTF8.GetBytes("{'Name': 'João','Age': 41,'State': 'Rio de Janeiro'}"));
        Base64Json_C = Convert.ToBase64String(Encoding.UTF8.GetBytes("{'Name': 'Eduardo','Age': 40,'City': 'Rio de Janeiro'}"));
        Base64Json_D = Convert.ToBase64String(Encoding.UTF8.GetBytes("{'Name': 'Amanda','Age': 40,'City': 'Rio de Janeiro'}"));
        Base64Json_E = Convert.ToBase64String(Encoding.UTF8.GetBytes("{'Name': 'João','Age': 41,'City': 'Rio de Janeiro','State': 'RJ'}"));
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void Should_Return_Exception_For_Invalid_JsonBase64()
    {
        service.SaveDataObject(Id, Global.LEFTDIFF, "NoJsonBase64Data");
    }

    [TestMethod]
    public void Should_Return_Success_Valid_JsonBase64()
    {
        try
        {
            service.SaveDataObject(Id, Global.LEFTDIFF, Base64Json_A);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void Should_Return_Exception_NoSidesOnMemory()
    {
        service.ProcessResult(Guid.NewGuid());
    }

    [TestMethod]
    public void Should_Return_Success_JsonEquals()
    {

        service.SaveDataObject(Id, Global.LEFTDIFF, Base64Json_A);
        service.SaveDataObject(Id, Global.RIGHTDIFF, Base64Json_A);

        var result = service.ProcessResult(Id);

        Assert.AreEqual(result.Message, Messages.JSON_EQUALS);

    }

    [TestMethod]
    public void Should_Return_Success_JsonSizeNotEquals()
    {

        service.SaveDataObject(Id, Global.LEFTDIFF, Base64Json_A);
        service.SaveDataObject(Id, Global.RIGHTDIFF, Base64Json_E);

        var result = service.ProcessResult(Id);

        Assert.AreEqual(result.Message, Messages.JSON_SIZE_NOT_EQUALS);
    }

  [TestMethod]
    public void Should_Return_Success_HasListValuesDiff()
    {
        service.SaveDataObject(Id, Global.LEFTDIFF, Base64Json_C);
        service.SaveDataObject(Id, Global.RIGHTDIFF, Base64Json_D);

        var result = service.ProcessResult(Id);

        var hasValueListDiff = result.ListDiffs != null && result.ListDiffs.Values.Count > 0;

        Assert.IsTrue(hasValueListDiff);
    }
    

     [TestMethod]
    public void Should_Return_Success_HasListPropertiesDiff()
    {
        service.SaveDataObject(Id, Global.LEFTDIFF, Base64Json_C);
        service.SaveDataObject(Id, Global.RIGHTDIFF, Base64Json_B);

        var result = service.ProcessResult(Id);

        var hasPropertiesListDiff = result.ListDiffs != null && result.ListDiffs.Properties.Count > 0;

        Assert.IsTrue(hasPropertiesListDiff);
    }

}