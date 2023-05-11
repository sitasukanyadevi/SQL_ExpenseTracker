using System.Data.SqlClient;
namespace Sql_ExpenseTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server=US-8ZBJZH3; database=ExpenseTracker; Integrated Security=true");
            con.Open();
            string s = "";
            do
            {
                Console.WriteLine("Welcome to Expense Tracker App");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Expenses");
                Console.WriteLine("3. View Incomes");
                Console.WriteLine("4. Check Available Balance");
                Console.WriteLine("Enter ur Choice");
                int choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        { 
                            SqlCommand cmd = new SqlCommand($"insert into ETracker values(@title, @description, @amount, @date)", con);
                            Console.WriteLine("Enter Title: ");
                            string title = Console.ReadLine();
                            Console.WriteLine("Enter Description: ");
                            string description = Console.ReadLine();
                            Console.WriteLine("Enter Amount: ");
                            int amount = Convert.ToInt16(Console.ReadLine());
                            Console.WriteLine("Enter date ");
                            string date = Console.ReadLine();
                            cmd.Parameters.AddWithValue("@title", title);
                            cmd.Parameters.AddWithValue("@description", description);
                            cmd.Parameters.AddWithValue("@amount", amount);
                            cmd.Parameters.AddWithValue("@date", date);  
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Record saved successfully");
                            break;

                        }
                    case 2:
                        {
                            SqlCommand cmd = new SqlCommand($"select * from ETracker where Amount < 0", con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            Console.WriteLine("Expenses:");
                            while (dr.Read())
                            {
                                Console.WriteLine($"{dr[0]},{dr[1]}, {dr[2]}, {dr[3]},{dr[4]}");
                            }
                            dr.Close();
                            break;

                        }
                    case 3:
                        {
                            SqlCommand cmd = new SqlCommand($"select * from ETracker where Amount > 0", con);
                            SqlDataReader dr1 = cmd.ExecuteReader();
                            Console.WriteLine("Incomes:");
                            while (dr1.Read())
                            {
                                for(int i = 0; i < dr1.FieldCount; i++)
                                {
                                    Console.Write($"{dr1[i]} |");
                                }
                                
                            }
                            dr1.Close();
                            break;
                        }
                    case 4:
                        {
                            
                            SqlCommand cmd = new SqlCommand($"select sum(Amount) as AvailableBalance from ETracker", con);
                            var bal = cmd.ExecuteScalar();
                            Console.WriteLine($"Available Balance:{bal}");   
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong Choice Entered");
                            break;
                        }
                }
                
                Console.WriteLine("Do you wish to continue? [y/n] ");
                s = Console.ReadLine();
            } while (s.ToLower() == "y");
            con.Close();
        }
    }
}