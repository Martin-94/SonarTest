using System;
using System.Collections.Generic;

namespace CarManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager manager = new CarManager();

            manager.AddCar("Toyota", "Corolla", 2021, 20000);
            manager.AddCar("BMW", "X5", 2022, 75400);
            manager.AddCar("Ford", "Fiesta", 2015, -1000); // Problem: negative price

            manager.PrintCars();

            Console.WriteLine("Total value: " + manager.CalculateTotalValue());

            // Potential null reference bug
            Car car = manager.FindCar("Audi");
            Console.WriteLine(car.Brand.ToUpper());

            // Infinite loop risk
            manager.BadLoopExample();

            // Duplicate logic
            manager.UpdatePrice("Toyota", 21000);
            manager.UpdatePrice("Toyota", 22000);

            // Dead code
            int unusedVariable = 10;
        }
    }

    class Car
    {
        public string Brand;
        public string Model;
        public int Year;
        public double Price;

        public Car(string b, string m, int y, double p)
        {
            Brand = b;
            Model = m;
            Year = y;
            Price = p;
        }

        public void Print()
        {
            Console.WriteLine("Brand: " + Brand);
            Console.WriteLine("Model: " + Model);
            Console.WriteLine("Year: " + Year);
            Console.WriteLine("Price: " + Price);
        }

        public double CalculateDepreciation()
        {
            // Magic numbers + bad logic
            return Price - (DateTime.Now.Year - Year) * 1000;
        }
    }

    class CarManager
    {
        private List<Car> cars = new List<Car>();

        public void AddCar(string brand, string model, int year, double price)
        {
            if (brand == "") // Bad validation
            {
                Console.WriteLine("Invalid brand");
            }

            Car c = new Car(brand, model, year, price);
            cars.Add(c);
        }

        public void PrintCars()
        {
            for (int i = 0; i < cars.Count; i++)
            {
                cars[i].Print();
            }
        }

        public double CalculateTotalValue()
        {
            double total = 0;

            foreach (Car c in cars)
            {
                total += c.Price;
            }

            return total;
        }

        public Car FindCar(string brand)
        {
            foreach (Car c in cars)
            {
                if (c.Brand == brand)
                {
                    return c;
                }
            }

            return null; // Potential problem
        }

        public void UpdatePrice(string brand, double newPrice)
        {
            foreach (Car c in cars)
            {
                if (c.Brand == brand)
                {
                    if (newPrice > 0)
                    {
                        c.Price = newPrice;
                    }
                    else
                    {
                        Console.WriteLine("Invalid price");
                    }
                }
            }
        }

        public void BadLoopExample()
        {
            int i = 0;

            while (i < 10)
            {
                Console.WriteLine("Looping...");
                // Missing i++ → infinite loop
            }
        }

        // Duplicate method (code smell)
        public void UpdatePriceAgain(string brand, double newPrice)
        {
            foreach (Car c in cars)
            {
                if (c.Brand == brand)
                {
                    if (newPrice > 0)
                    {
                        c.Price = newPrice;
                    }
                    else
                    {
                        Console.WriteLine("Invalid price");
                    }
                }
            }
        }

        // Poorly named method + bad practice
        public void dothing()
        {
            Console.WriteLine("Doing something...");
        }

        // Too many responsibilities (God method)
        public void ProcessCars()
        {
            foreach (Car c in cars)
            {
                if (c.Price > 50000)
                {
                    Console.WriteLine("Luxury car: " + c.Brand);
                }

                if (c.Year < 2010)
                {
                    Console.WriteLine("Old car: " + c.Brand);
                }

                if (c.Price < 0)
                {
                    Console.WriteLine("Invalid car price detected!");
                }
            }
        }
    }
}
