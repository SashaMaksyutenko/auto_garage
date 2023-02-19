using ConsoleApp116.HelperModels;
using ConsoleApp116.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleApp116
{
    class Program
    {
        static void Main(string[] args)
        {
            GarageAuto garageAuto = new GarageAuto();
            try
            {
                garageAuto.ShowMainMenu();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
            }

            Console.WriteLine("Good bye");
            Console.ReadKey();
        }
    }
}
