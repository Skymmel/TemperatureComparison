using System;
using System.Linq;
using Spectre.Console;

namespace TemperatureComparison
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            AnsiConsole.MarkupLine("[green]Welcome to [bold]TemperatureComparison[/][/]");
            AnsiConsole.MarkupLine("[gray]Console project by [underline]Vojtěch Wilhelm Skyba[/][/]");
            bool runProgram = true;
            while (runProgram)
            {
                bool correctYear = false;
                short year;
                do
                {
                    year = AnsiConsole.Ask<short>("Year: ");
                    if (year <= 0)
                    {
                        AnsiConsole.MarkupLine("[red]Year can not to be below 1.[/]");
                    }
                    else
                    {
                        correctYear = true;
                    }
                } while (!correctYear);
                
                bool correctMonth = false;
                short month;
                do
                {
                    month = AnsiConsole.Ask<short>("Month: ");
                    if (month <= 0 || month > 12)
                    {
                        AnsiConsole.MarkupLine("[red]Months are a maximum of 12 and do not go below 1.[/]");
                    }
                    else
                    {
                        correctMonth = true;
                    }
                } while (!correctMonth);

                int daysInMonth = DateTime.DaysInMonth(year, month);
                float[] temperature = new float[daysInMonth];

                for (int i = 1; i <= daysInMonth; i++)
                {
                    Console.Clear();
                    var calendar = new Calendar(year, month);
                    calendar.AddCalendarEvent(year, month, i);
                    calendar.HighlightStyle(Style.Parse("blue bold"));
                    AnsiConsole.Write(calendar);
                    temperature[i - 1] = AnsiConsole.Ask<float>("Measured temperature in this day: ");
                }

                float maxTemperature = temperature.Max();
                float minTemperature = temperature.Min();
                float aveTemperature = temperature.Average();
            
                AnsiConsole.MarkupInterpolated($"[red]\u2191[/] Maximal temperature: [bold green]{maxTemperature}°C[/]\n");
                AnsiConsole.MarkupInterpolated($"[yellow]-[/] Average temperature: [bold green]{aveTemperature}°C[/]\n");
                AnsiConsole.MarkupInterpolated($"[aqua]\u2193[/] Minimal temperature: [bold green]{minTemperature}°C[/]\n");

                string input;
                bool correctReply = false;
                do
                {
                    input = AnsiConsole.Ask<string>("Do you want continue (Y/N): ").Trim().ToUpper();
                    if (input.Equals("Y"))
                    {
                        correctReply = true;
                    }
                    else if (input.Equals("N")) 
                    {
                        AnsiConsole.MarkupLine("[green]Ending...[/]");
                        correctReply = true;
                        runProgram = false;
                    }else
                    {
                        AnsiConsole.MarkupLine("[red]ERROR[/]");
                    }
                } while (!correctReply);
            }
        }
    }
}