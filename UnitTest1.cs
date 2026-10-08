using NUnit.Framework;
using tradingcompany.dal;

namespace tradingcompany.tests;

public class ProductDalTests
{
    private string _connectionString = "Server=localhost;Database=trading_company;Integrated Security=true;TrustServerCertificate=true;";
    private IProductDal _dal = null!;

    [SetUp]
    public void Setup()
    {
        _dal = new ProductDal(_connectionString);
    }

    [Test]
    public void GetAll_ShouldReturnItems()
    {
        var result = _dal.GetAll();
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.GreaterThan(0));
    }

    [Test]
    public void CRUD_Operations_ShouldWork()
    {
        var newItem = new Product { Name = "Тест для юніт-тесту", Price = 100m };
        int id = _dal.Create(newItem);
        Assert.That(id, Is.GreaterThan(0));

        var fetched = _dal.GetById(id);
        Assert.That(fetched, Is.Not.Null);
        Assert.That(fetched!.Name, Is.EqualTo("Тест для юніт-тесту"));

        fetched.Name = "Оновлено в тесті";
        bool updated = _dal.Update(fetched);
        Assert.That(updated, Is.True);

        bool deleted = _dal.Delete(id);
        Assert.That(deleted, Is.True);
    }
}