using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace OOP_Curs_Kanevskii_23VP2
{
    /// <summary>
    /// Класс для работы с базой данных SQLite, реализующий интерфейс IDisposable
    /// </summary>
    public class DatabaseContext : IDisposable
    {
        // Строка подключения к базе данных
        private readonly string _connectionString;

        // Кэшированный список продуктов для уменьшения обращений к БД
        public List<Product> CachedProducts { get; private set; } = new List<Product>();

        // Свойство для доступа к строке подключения (только для чтения)
        public string ConnectionString => _connectionString;

        /// <summary>
        /// Конструктор класса DatabaseContext
        /// </summary>
        /// <param name="connectionString">Строка подключения к SQLite базе данных</param>
        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
            InitializeDatabase(); // Инициализация структуры БД
            LoadProductsToCache(); // Загрузка данных в кэш
        }

        /// <summary>
        /// Инициализация структуры базы данных (создание таблицы, если не существует)
        /// </summary>
        public void InitializeDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    // SQL-запрос для создания таблицы Product с необходимыми полями
                    var createTableCommand = new SQLiteCommand(@"
                        CREATE TABLE IF NOT EXISTS Product (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Mass REAL NOT NULL,
                            Price REAL NOT NULL,
                            Amount INTEGER NOT NULL
                        )", connection);
                    createTableCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при инициализации базы данных.", ex);
            }
        }

        /// <summary>
        /// Загрузка всех продуктов из базы данных в кэш (CachedProducts)
        /// </summary>
        public void LoadProductsToCache()
        {
            CachedProducts.Clear(); // Очистка текущего кэша
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("SELECT * FROM Product", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Заполнение кэша данными из БД
                            CachedProducts.Add(new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Mass = reader.GetDouble(2),
                                Price = reader.GetDouble(3),
                                Amount = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при загрузке товаров в кэш.", ex);
            }
        }

        /// <summary>
        /// Добавление нового продукта в базу данных
        /// </summary>
        /// <param name="product">Объект продукта для добавления</param>
        public void AddProduct(Product product)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    // SQL-запрос для вставки нового продукта
                    var command = new SQLiteCommand(@"
                        INSERT INTO Product (Name, Mass, Price, Amount)
                        VALUES (@Name, @Mass, @Price, @Amount)", connection);
                    // Добавление параметров с защитой от SQL-инъекций
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Mass", product.Mass);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Amount", product.Amount);
                    command.ExecuteNonQuery();
                }
                LoadProductsToCache(); // Обновление кэша после изменения
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при добавлении товара в базу данных.", ex);
            }
        }

        /// <summary>
        /// Обновление существующего продукта в базе данных
        /// </summary>
        /// <param name="product">Объект продукта с обновленными данными</param>
        public void UpdateProduct(Product product)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    // SQL-запрос для обновления продукта по ID
                    var command = new SQLiteCommand(@"
                        UPDATE Product
                        SET Name = @Name, Mass = @Mass, Price = @Price, Amount = @Amount
                        WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Mass", product.Mass);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Amount", product.Amount);
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.ExecuteNonQuery();
                }
                LoadProductsToCache(); // Обновление кэша после изменения
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при обновлении товара в базе данных.", ex);
            }
        }

        /// <summary>
        /// Удаление продукта из базы данных по ID
        /// </summary>
        /// <param name="id">Идентификатор продукта для удаления</param>
        public void DeleteProduct(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    // SQL-запрос для удаления продукта по ID
                    var command = new SQLiteCommand("DELETE FROM Product WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
                LoadProductsToCache(); // Обновление кэша после изменения
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при удалении товара из базы данных.", ex);
            }
        }

        /// <summary>
        /// Реализация интерфейса IDisposable для освобождения ресурсов
        /// </summary>
        public void Dispose()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close(); // Закрытие соединения, если оно открыто
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при освобождении ресурсов базы данных.", ex);
            }
        }
    }
}