namespace tradingcompany.dal;

public interface IProductDal
{
    List<Product> GetAll(); //чит всіх записів
    Product? GetById(int id);//чит одного запису за айді 
    int Create(Product item);
    bool Update(Product item);
    bool Delete(int id);
}