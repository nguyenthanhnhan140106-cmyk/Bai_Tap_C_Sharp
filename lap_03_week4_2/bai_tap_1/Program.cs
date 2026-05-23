using System;

namespace bai_tap1
{
    // Thêm class bọc bên ngoài phương thức static
    public class Program
    {
        public static void HandleMultipleExceptions(string input, int index)
        {
            int[] numbers = { 1, 2, 3 };

            try
            {
                int parsedValue = int.Parse(input);
                Console.WriteLine($"Gia tri tai index {index} la: {numbers[index]}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid format.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Index out of range.");
            }
        }
        static void Main(string[] args)
        {
            // Ví dụ test thử hàm:
            HandleMultipleExceptions("123", 1);
            HandleMultipleExceptions("abc", 0);
            HandleMultipleExceptions("123", 5);
        }
    }
}