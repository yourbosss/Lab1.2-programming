using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;


//В зоопарке содеражтся животные, со следующими данными:
//название, вес, вес еды, тип пищи.
//вывести данные о животных поедающее мясо и животного, поедающее максимальное количество мяса за свой вес.

namespace Lab1_2
{
    [Serializable]    /// сохранение в документы
    class Animal
    {
        public string Name { get; set; } //создаем табличку
        public double Weight { get; set; }
        public double FoodWeight { get; set; }
        public string FoodType { get; set; }

        public Animal(string name, double weight, double foodWeight, string foodType)
        {
            Name = name;
            Weight = weight;
            FoodWeight = foodWeight;
            FoodType = foodType;
        }

        public override string ToString()
        {
            return $"{Name} ({Weight} кг, {FoodWeight} кг/{Weight} кг, {FoodType})";
        }
    }

    class Program
    {
        static void Main()
        {
            string filePath = "D:\\visualstudio\\programming\\Lab1.2\\animals.xml";
            XDocument myDoc = new XDocument();


            try
            {
                myDoc = XDocument.Load(filePath);
            }
            catch (Exception)
            {
                Console.WriteLine("Документ не найден или содержит некорректные данные");                                //вывод данных о животных поедающие мясо.
                return;
            }

            List<Animal> animals = myDoc.Descendants("animal").Select(animal => new Animal(
                (string)animal.Element("name"),
                (double)animal.Element("weight"),
                (double)animal.Element("foodWeight"),
                (string)animal.Element("foodType")
            )).ToList();

            Console.WriteLine("Животные, поедающие мясо:");                                //вывод данных о животных поедающие мясо.
            foreach (Animal animal in animals.Where(a => a.FoodType == "мясо"))
            {
                Console.WriteLine(animal);
            }

            // нахождение животного, поедающего максимальное количество пищи на 1 кг собственного веса.
            Animal maxFoodPerWeight = animals.OrderByDescending(a => a.FoodWeight / a.Weight).First();
            Console.WriteLine($"Животное, поедающее максимальное количество пищи на 1 кг собственного веса: {maxFoodPerWeight}");
        }
    }
}

           