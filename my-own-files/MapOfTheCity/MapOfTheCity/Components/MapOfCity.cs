using System;
using System.Collections.Generic;
using System.Linq;

namespace MapOfTheCity {
	public class MapOfCity {

		public List<Place> Places;

		public MapOfCity() {
			Places = new List<Place>();
		}

		// Краще не передавати сюди нічого, оскільки в тебе в класі є колекція місць, просто вибирати з них ті, які мають рейтинг
		// Також краще не виводити результат в цьому методі. Нехай метод повертає об'єкт типу Place,
		// а той хто буде викликати цей меод вже собі вирішить що робити з результатом і куди його виводити.
		// Тому що можливо буде потреба зберегти таке місце в файл, прийдеться писати нови метод
		public void GetMostPopularPlace(List<IRatingable> places) {
			Place mostPopularPlace = places
				.OrderByDescending(t => t.Rating)
				.Select(t => (Place)t)
				.FirstOrDefault();

			Console.WriteLine(mostPopularPlace.Coordinates);
		}

		public void SetRating(IRatingable place, float newRating) {
			// Тут place завжди IRatingable, тому що тип параметру такий.
			// Тобто ця перевірка завжди буде повертати true і немає сенсу щось перевіряти.
			// Також ти просто навсього не зможеш передати сюди об'єкт класу, який не реалізовує цей інтерфейс.
			// Тобто цей метод безпечний по відношенню до типу
			if (place is IRatingable)
				place.Rating = newRating;
			else
				throw new Exception("Can't set rating for this place");
		}
	}
}
