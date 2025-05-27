using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Curs_Kanevskii_23VP2
{
    /// <summary>
    /// Класс "Товар" - базовую сущность товара в системе.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Масса товара (в кг)
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// Цена товара за единицу (в руб)
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Количество единиц товара в наличии (в штуках)
        /// </summary>
        public int Amount { get; set; }
    }
}
