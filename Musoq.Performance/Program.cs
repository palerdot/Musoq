﻿using System;
using System.Diagnostics;
using Musoq.Converter;
using Musoq.Schema;

namespace Musoq.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteQuery(
            @"select
                AgencyName,
                Count(AgencyName),
                Sum(ToDecimal(Amount))
            from #csv.file('C:\Users\Puchacz\Downloads\cards\res_purchase_card_(pcard)_fiscal_year_2014_3pcd-aiuu.csv', 0)
            group by AgencyName");

            Console.WriteLine();
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        private static void ExecuteQuery(string query)
        {
            var watch = new Stopwatch();

            watch.Start();
            var vm = InstanceCreator.Create(query, CreateSchema());
            var compiledTime = watch.Elapsed;
            var table = vm.Execute();
            watch.Stop();
            var executionTime = watch.Elapsed;

            Console.WriteLine($"Table {table.Name} contains {table.Count} rows.");
            Console.WriteLine($"Query compiled in {compiledTime}");
            Console.WriteLine($"Query prcessed in {executionTime - compiledTime}");
        }

        private static ISchemaProvider CreateSchema()
        {
            return new CsvSchemaProvider();
        }
    }
}
