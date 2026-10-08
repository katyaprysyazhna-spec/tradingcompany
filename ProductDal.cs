using Microsoft.Data.SqlClient;

namespace tradingcompany.dal;

public class ProductDal : IProductDal
{
    private readonly string _connectionString;

    public ProductDal(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Product> GetAll()
    {
        var list = new List<Product>();
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT product_id, name, price FROM products", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Product
            {
                ProductId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            });
        }
        return list;
    }

    public Product? GetById(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT product_id, name, price FROM products WHERE product_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Product
            {
                ProductId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            };
        }
        return null;
    }

    public int Create(Product item)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("INSERT INTO products (name, price) OUTPUT INSERTED.product_id VALUES (@name, @price)", conn);
        cmd.Parameters.AddWithValue("@name", item.Name);
        cmd.Parameters.AddWithValue("@price", item.Price);
        return (int)cmd.ExecuteScalar();
    }

    public bool Update(Product item)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("UPDATE products SET name = @name, price = @price WHERE product_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", item.ProductId);
        cmd.Parameters.AddWithValue("@name", item.Name);
        cmd.Parameters.AddWithValue("@price", item.Price);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Delete(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("DELETE FROM products WHERE product_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        return cmd.ExecuteNonQuery() > 0;
    }
}