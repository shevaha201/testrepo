using System;

namespace MapOfTheCity {
	public abstract class Place {

		private string _name;
		public GeographicCoordinates Coordinates;
		public abstract string TypeOfPlace { get; }

		public string Name {
			get {
				return _name;
			}
			set {
				if (value != null)
					_name = value;
				else
					throw new Exception("Name can't be null");
			}
		}

		public Place(string name, double latitude, double longitude) {
			Name = name;
			Coordinates = new GeographicCoordinates(latitude, longitude);
		}

		public double GetDistance(Place place) {
			return Math.Sqrt(
						Math.Pow(place.Coordinates.Latitude - Coordinates.Latitude, 2.0)
						+ Math.Pow(place.Coordinates.Longitude - Coordinates.Longitude, 2.0)
						);
		}

		public override string ToString() {
			return string.Format($"{TypeOfPlace,-20} {Name,-20} \t {Coordinates} \t ");
		}
	}
}
