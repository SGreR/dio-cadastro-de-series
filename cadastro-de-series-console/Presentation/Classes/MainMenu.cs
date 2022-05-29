using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Classes;
using BLL.Enum;
using DAL.Repositories;

namespace Presentation.Classes
{
    public static class MainMenu
    {
        public static SeriesRepository repository = new SeriesRepository();


        public static void InitialMenu()
        {
			string userChoice = GetUserChoice();

			while (userChoice.ToUpper() != "X")
			{
				switch (userChoice)
				{
					case "1":
						ListAllSeries();
						break;
					case "2":
						GetSeriesByText();
						break;
					case "3":
						InsertSeries();
						break;
					case "4":
						UpdateSeries();
						break;
					case "5":
						RemoveSeries();
						break;
					case "6":
						ShowDetails();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				userChoice = GetUserChoice();
			}

			Console.WriteLine("Thank you for using the app");
			Console.WriteLine("Press [enter] to exit...");
			Console.ReadLine();
		}

		private static string GetUserChoice()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to the TV series catalogue");
			Console.WriteLine("Please select one of the options below:");

			Console.WriteLine("1 - List all TV series");
			Console.WriteLine("2 - Search by text");
			Console.WriteLine("3 - Add new TV series");
			Console.WriteLine("4 - Update TV series");
			Console.WriteLine("5 - Remove TV series");
			Console.WriteLine("6 - View TV series details");
			Console.WriteLine("C - Clear menu");
			Console.WriteLine("X - Exit");
			Console.WriteLine();

			string userChoice = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return userChoice;
		}

		private static void ListAllSeries()
		{
			Console.WriteLine("List TV series");

			var list = repository.GetAll();

			if (list.Count == 0)
			{
				Console.WriteLine("No TV series added yet.");
				return;
			}

			foreach (var series in list)
			{
				Console.WriteLine("#ID {0}: - {1} {2}", series.GetId(), series.GetTitle(), (series.GetRemoved() ? "*Removed*" : ""));
			}
		}

		private static void InsertSeries()
		{
			Console.WriteLine("Insert new TV series:");

			foreach (int i in Enum.GetValues(typeof(Genre)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genre), i));
			}
			int genreInput;
			bool validGenre = false;
            do
            {
				Console.Write("Choose the TV series genre from the options above: ");
				validGenre = (int.TryParse(Console.ReadLine(), out genreInput) && genreInput >= 0 && genreInput < Enum.GetValues(typeof(Genre)).Length);
			} while (!validGenre);
			

			Console.Write("Please insert the name of the TV series: ");
			string titleInput = Console.ReadLine();

			Console.Write("Please insert the year the series premiered: ");
			int yearInput = int.Parse(Console.ReadLine());

			Console.Write("Please insert a short description of the series: ");
			string descriptionInput = Console.ReadLine();

			Series newSeries = new Series(id: repository.NextId(),
										genre: (Genre)genreInput,
										title: titleInput,
										year: yearInput,
										description: descriptionInput);

			repository.Insert(newSeries);
		}

		private static void UpdateSeries()
		{
			Console.Write("Please type the ID of the series you would like to update: ");
			int seriesIndex = int.Parse(Console.ReadLine());
			foreach (int i in Enum.GetValues(typeof(Genre)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genre), i));
			}
			Console.Write("Choose the TV series genre from the options above ");
			int genreInput = int.Parse(Console.ReadLine());

			Console.Write("Please insert the name of the TV series: ");
			string titleInput = Console.ReadLine();

			Console.Write("Please insert the year the series premiered: ");
			int yearInput = int.Parse(Console.ReadLine());

			Console.Write("Please insert a short description of the series: ");
			string descriptionInput = Console.ReadLine();

			Series newSeries = new Series(id: repository.NextId(),
										genre: (Genre)genreInput,
										title: titleInput,
										year: yearInput,
										description: descriptionInput);

			repository.Update(seriesIndex, newSeries);
		}

		private static void RemoveSeries()
		{
			Console.Write("Please type the ID of the series you would like to delete: ");
			int seriesIndex = int.Parse(Console.ReadLine());

			repository.Delete(seriesIndex);
		}

		private static void ShowDetails()
		{
			Console.Write("Please type the series' ID: ");
			int seriesIndex = int.Parse(Console.ReadLine());

			var series = repository.GetById(seriesIndex);

			Console.WriteLine(series);
		}

		private static void GetSeriesByText()
        {
			Console.WriteLine("Please type the name, year or part of the description of the TV series: ");
			string searchString = Console.ReadLine().ToUpper();
			var matches = repository.GetByText(searchString);
			if (!matches.Any())
            {
				Console.WriteLine("No results were found");
				return;
            }
			foreach (var series in matches)
            {
				Console.WriteLine("#ID {0}: - {1} {2}", series.GetId(), series.GetTitle(), (series.GetRemoved() ? "*Removed*" : ""));
			}
        }
	}
}
