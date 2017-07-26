using System;
using System.Collections.Generic;
using System.Linq;

namespace MapOfTheCity {
	class Program {
		static void Main(string[] args) {
			MapOfCity mapOfCity = new MapOfCity();

			AddPublicPlaces(mapOfCity);
			AddHistoricalMonuments(mapOfCity);
			AddPlacesToEat(mapOfCity);

			List<IRatingable> placesWithRating = GetPlacesWithRating(mapOfCity);

			char operationNumber;
			if (!IsEmpty(mapOfCity.Places) && !IsEmpty(placesWithRating)) {
				do {
					Console.WriteLine(
					"_____________________________________" +
					"\nEnter operation number:" +
					"\n1) Display list of places" +
					"\n2) Display distance between two places" +
					"\n3) Display sorted list of places by rating" +
					"\n4) Display coordinates of most popular place" +
					"\n5) Set new rating" +
					"\n6) Get the nearest place to eat" +
					"\n7) Close programm\n"
					);

					operationNumber = char.Parse(Console.ReadLine());

					switch (operationNumber) {
						case '1':
							DisplayPlaces<Place>(mapOfCity.Places);
							break;
						case '2':
							DisplayPlaces<Place>(mapOfCity.Places);
							CalculateDistanceBetweenTwoPlaces(mapOfCity);
							break;
						case '3':
							DisplaySortedListOfPlacesByrating(placesWithRating);
							break;
						case '4':
							DisplayCoordinatesOfMostPopularPlace(mapOfCity, placesWithRating);
							break;
						case '5':
							SetNewRating(mapOfCity, placesWithRating);
							break;
						case '6':
							List<PlaceToEat> placesToEat = mapOfCity.Places
								.Where(t => t is PlaceToEat)
								.Select(t => (PlaceToEat)t)
								.ToList();
							if (!IsEmpty(placesToEat))
								DisplayNearestPlaceToEat(mapOfCity);
							break;
						case '7':
							break;
						default:
							Console.WriteLine("Wrong number");
							break;
					}
				} while (operationNumber != '7');
			} else {
				Console.WriteLine("No items");
				Console.ReadKey();
			}
		}

		public static void AddPublicPlaces(MapOfCity map) {
			map.Places.Add(new PublicPlace("Park Shevchenka", 48.282474, 25.939158));
			map.Places.Add(new PublicPlace("Park Zhovtnevy", 48.259101, 25.936579));
			map.Places.Add(new PublicPlace("Botanical Garden", 48.279742, 25.936596));
		}

		public static void AddHistoricalMonuments(MapOfCity map) {
			map.Places.Add(new HistoricalMonument("Holy Spirit Cathedral", 48.288016, 25.936780, 4.8f));
			map.Places.Add(new HistoricalMonument("Ratusha", 48.291829, 25.934716, 4));
			map.Places.Add(new HistoricalMonument("House-Ship", 48.295311, 25.936796, 2));
		}

		public static void AddPlacesToEat(MapOfCity map) {
			map.Places.Add(new PlaceToEat("Potato House", 48.298967, 25.933362, "(0372) 51-24-00", "10:00 - 22:00", 3));
			map.Places.Add(new PlaceToEat("Cafe Schiller", 48.286937, 25.938849, "066 444-47-61", "10:00 - 24:00", 3));
			map.Places.Add(new PlaceToEat("NYSP", 48.269841, 25.940333, "050 379 7878", "10:00 - 23:00", 5));
			map.Places.Add(new PlaceToEat("Pizza Monopoli", 48.291390, 25.934254, "099 339-33-93", "11:00 - 23:00", 4.2f));
			map.Places.Add(new PlaceToEat("Veranda", 48.291390, 25.934254, "099 339-33-93", null, 3.9f));
		}

		// Метод корисний, але вартувало б стоврити статичний клас з такими допоміжними методами
		public static bool IsEmpty<T>(List<T> list) {
			if (list.Count() > 0) {
				return false;
			} else {
				return true;
			}
			// Можна ще так return list.Count() == 0;
		}
		// Гарніший виклик би був, якщо зробити як метод розширення
		// Треба створити окремий файл для цього класу
		//public static class ListExtensions {

		//	public static bool IsEmpty<T>(this List<T> list) {
		//		return list.Count() == 0;
		//	}
		//}
		// Тоді так можна використовувати
		// List<Place> list = new List<Place>();
		// list.IsEmpty();

		// OKAY
		public static void DisplayPlaces<T>(List<T> places) {
			for (int i = 0; i < places.Count(); i++) {
				Console.WriteLine($"{i + 1}. {places[i].ToString()}");
			}
		}

		// Краще такий метод мати в карті
		public static List<IRatingable> GetPlacesWithRating(MapOfCity map) {
			return map.Places
					.Where(t => t is HistoricalMonument || t is PlaceToEat) // Краще перевіряти чи t is IRatingable. Тоді якщо в майбутньому появиться новий клас з рейтингом - тут не потрібно нічого міняти
					.Select(t => (IRatingable)t)
					.ToList();
		}

		// В той самий клас закинути, що й IsEmpty()
		public static int IsIndexCorrect(int index, int placesCount) {
			bool isIndexCorrect = false;
			
			while (!isIndexCorrect) {
				if (index >= 0 && index < placesCount) {
					isIndexCorrect = true;
				} else {
					Console.WriteLine("Wrong index. Enter correct index");
					index = int.Parse(Console.ReadLine()) - 1;
				}
			}

			return index;
		}

		// OKAY
		public static void CalculateDistanceBetweenTwoPlaces(MapOfCity map) {
			Console.Write("\nEnter number of first place: ");
			int idOfFirstPlace = int.Parse(Console.ReadLine()) - 1;
			idOfFirstPlace = IsIndexCorrect(idOfFirstPlace, map.Places.Count());

			Console.Write("Enter number of second place: ");
			int idOfSecondPlace = int.Parse(Console.ReadLine()) - 1;
			idOfSecondPlace = IsIndexCorrect(idOfSecondPlace, map.Places.Count());

			double distance = map.Places[idOfFirstPlace].GetDistance(map.Places[idOfSecondPlace]);
			Console.WriteLine($"\nDistance from {map.Places[idOfFirstPlace].Name} to {map.Places[idOfSecondPlace].Name} equals {distance}");
		}

		// Краще в класі MapOfCity написати метод який повертає відсортовані місця, а тут їх просто виводити
		public static void DisplaySortedListOfPlacesByrating(List<IRatingable> places) {
			Console.WriteLine("\nSorted list of places by rating:");

			List<IRatingable> sortedPlaces = places
				.OrderByDescending(t => t.Rating)
				.ToList();

			DisplayPlaces<IRatingable>(sortedPlaces);
		}

		public static void DisplayCoordinatesOfMostPopularPlace(MapOfCity map, List<IRatingable> places) {
			Console.Write("The most popular place: ");
			map.GetMostPopularPlace(places); // Комент в цьому методі
		}

		// Якщо б в карті бу метод, який повертає місця з рейтинго, не потрібно б було передавати сюди другий параметр.
		public static void SetNewRating(MapOfCity map, List<IRatingable> places) {
			DisplayPlaces<IRatingable>(places);

			Console.Write("\nEnter number of place for putting new rating: ");
			int number = int.Parse(Console.ReadLine()) - 1;
			number = IsIndexCorrect(number, places.Count());

			Console.Write("Enter new rating (0,0 - 5,0): ");
			float newRating = float.Parse(Console.ReadLine());

			map.SetRating(places[number], newRating);

			Console.WriteLine($"Place with new rating: {places[number]}");
		}

		public static void DisplayNearestPlaceToEat(MapOfCity map) {
			// Краще в карті мати метод який поверне публічні місця, а тут працювати з результатом цього методу
			List<PublicPlace> publicPlaces = map.Places
				.Where(t => t is PublicPlace)
				.Select(t => (PublicPlace)t)
				.ToList();
			DisplayPlaces<PublicPlace>(publicPlaces);

			Console.Write("Enter number of your place: ");
			int number = int.Parse(Console.ReadLine()) - 1;
			number = IsIndexCorrect(number, publicPlaces.Count());

			PlaceToEat nearestPlaceToEat = publicPlaces[number].GetNearestPlaceToEat(map);

			Console.WriteLine($"The nearest place to eat to {publicPlaces[number].Name} is {nearestPlaceToEat.Name}");
		}
	}
}
