using tradingcompany.dal;

string connectionString = "Server=localhost;Database=trading_company;Integrated Security=true;TrustServerCertificate=true;";
IProductDal dal = new ProductDal(connectionString);

Console.WriteLine("=== Перевірка CRUD для продуктів ===");

var products = dal.GetAll();
Console.WriteLine($"Всього товарів у базі: {products.Count}");

var newProduct = new Product { Name = "Новий тестовий товар", Price = 999.99m };
int createdId = dal.Create(newProduct);
Console.WriteLine($"Створено товар з ID: {createdId}");

var item = dal.GetById(createdId);
if (item != null)
{
    Console.WriteLine($"Знайдено товар: {item.Name}, ціна: {item.Price}");
    item.Name = "Оновлений товар";
    item.Price = 1200.00m;
    dal.Update(item);
    Console.WriteLine("Товар успішно оновлено.");
}

dal.Delete(createdId);
Console.WriteLine($"Товар з ID {createdId} успішно видалено.");