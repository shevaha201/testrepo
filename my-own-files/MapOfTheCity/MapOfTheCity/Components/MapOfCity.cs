using System;
using System.Collections.Generic;
using System.Linq;

namespace MapOfTheCity {
	public class MapOfCity {

		public List<Place> Places;

		public MapOfCity() {
			Places = new List<Place>();
		}

		public void GetMostPopularPlace(List<IRatingable> places) {
			Place mostPopularPlace = places
				.OrderByDescending(t => t.Rating)
				.Select(t => (Place)t)
				.FirstOrDefault();

			Console.WriteLine(mostPopularPlace.Coordinates);
		}

		public void SetRating(IRatingable place, float newRating) {
			if (place is IRatingable)
				place.Rating = newRating;
			else
				throw new Exception("Can't set rating for this place");
		}
	}
}
