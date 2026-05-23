using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Hello!");
            if (!TryReadInt("Nhap so thu nhat: ", out int number1)) return;
            if (!TryReadInt("Nhap so thu hai: ", out int number2)) return;

            Console.WriteLine("Ban muon thuc hien phep tinh nao?");
            Console.WriteLine("[A]dd numbers");
            Console.WriteLine("[S]ubtract numbers");
            Console.WriteLine("[M]ultiply numbers");
            Console.WriteLine("[D]ivide numbers");
            Console.Write("> ");
            
            string choice = Console.ReadLine() ?? "";

            if (EqualsCaseInsensitive(choice, "A"))
            {
                int result = number1 + number2;
                PrintFinalEquation(number1, number2, result, "+");
            }
            else if (EqualsCaseInsensitive(choice, "S"))
            {
                int result = number1 - number2;
                PrintFinalEquation(number1, number2, result, "-");
            }
            else if (EqualsCaseInsensitive(choice, "M"))
            {
                int result = number1 * number2;
                PrintFinalEquation(number1, number2, result, "*");
            }
            else if (EqualsCaseInsensitive(choice, "D"))
            {
                if (number2 == 0)
                {
                    Console.WriteLine("Lỗi: Không thể thực hiện phép chia cho 0!");
                }
                else
                {
                    int result = number1 / number2;
                    PrintFinalEquation(number1, number2, result, "/");
                }
            }
            else
            {
                Console.WriteLine("Lua chon khong hop le!");
            }

            Console.WriteLine("Nhan phim bat ky de dong...");
            Console.ReadKey();
        }

        static bool TryReadInt(string prompt, out int result)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out result))
            {
                return true;
            }
            else
            {
                Console.WriteLine($"'{input}' is not a valid number!");
                return false;
            }
        }
        static void PrintFinalEquation(int n1, int n2, int result, string @operator)
        {
            // Sử dụng String Interpolation ($"...") đúng yêu cầu đề bài
            Console.WriteLine($"{n1} {@operator} {n2} = {result}");
        }

        static bool EqualsCaseInsensitive(string left, string right)
        {
            return left.ToUpper() == right.ToUpper();
        }
    }
}