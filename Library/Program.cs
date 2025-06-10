using System;
using System.Collections.Generic;
using Spectre.Console;
using Library;

namespace Library
{
    class Program
    {
        static List<LibraryItem> libraryItems = new List<LibraryItem>();
        static int nextId = 1;

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                try
                {
                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[green]Library System Menu[/]")
                            .PageSize(10)
                            .AddChoices(new[]
                            {
                                "Add Item",
                                "Remove Item",
                                "Search Item by ID",
                                "Display All Items",
                                "Exit"
                            }));

                    switch (choice)
                    {
                        case "Add Item":
                            AddItem();
                            break;
                        case "Remove Item":
                            RemoveItem();
                            break;
                        case "Search Item by ID":
                            SearchItem();
                            break;
                        case "Display All Items":
                            DisplayItems();
                            break;
                        case "Exit":
                            exit = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
                }
            }
        }

        static void AddItem()
        {
            var type = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Select the type of item to add[/]")
                    .AddChoices(new[] { "Book", "Magazine", "DVD" }));

            string title = AnsiConsole.Ask<string>("Enter the [green]Title[/]:");
            int yearPublished = AnsiConsole.Ask<int>("Enter the [green]Year Published[/]:");
            LibraryItem newItem = null;

            switch (type)
            {
                case "Book":
                    string author = AnsiConsole.Ask<string>("Enter the [green]Author[/]:");
                    string genre = AnsiConsole.Ask<string>("Enter the [green]Genre[/]:");
                    newItem = new Book
                    {
                        ID = nextId++,
                        Title = title,
                        YearPublished = yearPublished,
                        Author = author,
                        Genre = genre
                    };
                    break;
                case "Magazine":
                    int issueNumber = AnsiConsole.Ask<int>("Enter the [green]Issue Number[/]:");
                    string monthName = AnsiConsole.Ask<string>("Enter the [green]Month[/]:");
                    if (!Enum.TryParse<MonthsEnum>(monthName, true, out MonthsEnum parsedMonthEnum))
                    {
                        throw new Exception("Invalid month entered.");
                    }
                    newItem = new Magazine
                    {
                        ID = nextId++,
                        Title = title,
                        YearPublished = yearPublished,
                        IssueNumber = issueNumber,
                        MonthsEnum = parsedMonthEnum
                    };
                    break;
                case "DVD":
                    string director = AnsiConsole.Ask<string>("Enter the [green]Director[/]:");
                    int minutes = AnsiConsole.Ask<int>("Enter the [green]Duration in minutes[/]:");
                    newItem = new DVD
                    {
                        ID = nextId++,
                        Title = title,
                        YearPublished = yearPublished,
                        Director = director,
                        Duration = TimeSpan.FromMinutes(minutes)
                    };
                    break;
                default:
                    throw new Exception("Invalid item type.");
            }

            libraryItems.Add(newItem);
            AnsiConsole.MarkupLine("[green]Item added successfully![/]");
        }

        static void RemoveItem()
        {
            try
            {
                int id = AnsiConsole.Ask<int>("Enter the [green]ID[/] of the item to remove:");
                LibraryItem item = libraryItems.Find(x => x.ID == id);
                if (item != null)
                {
                    libraryItems.Remove(item);
                    AnsiConsole.MarkupLine("[green]Item removed successfully![/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Item not found![/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            }
        }

        static void SearchItem()
        {
            try
            {
                int id = AnsiConsole.Ask<int>("Enter the [green]ID[/] of the item to search:");
                LibraryItem item = libraryItems.Find(x => x.ID == id);
                if (item != null)
                {
                    AnsiConsole.MarkupLine("[blue]Item found:[/]");
                    AnsiConsole.MarkupLine(item.GetDetails());
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]No item found with the given ID.[/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            }
        }

        static void DisplayItems()
        {
            if (libraryItems.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Library is empty.[/]");
                return;
            }

            var table = new Table();
            table.Border = TableBorder.Rounded;
            table.AddColumn("ID");
            table.AddColumn("Title");
            table.AddColumn("Year Published");
            table.AddColumn("Details");

            foreach (var item in libraryItems)
            {
                table.AddRow(
                    item.ID.ToString(),
                    item.Title,
                    item.YearPublished.ToString(),
                    item.GetDetails());
            }

            AnsiConsole.Write(table);
        }
    }
}