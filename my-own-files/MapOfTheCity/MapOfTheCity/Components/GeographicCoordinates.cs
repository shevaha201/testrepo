using System;

namespace MapOfTheCity {
	public class GeographicCoordinates {

		private double _latitude; //Широта
		private double _longitude; //довгота

		public double Latitude {
			get {
				return _latitude;
			}
			private set {
				if (value > -90 && value < 90)
					_latitude = value;
				else
					throw new Exception("Invalid latitude");
			}
		}

		public double Longitude {
			get {
				return _longitude;
			}
			private set {
				if (value > -180 && value < 180)
					_longitude = value;
				else
					throw new Exception("Invalid longitude");
			}
		}

		public GeographicCoordinates(double latitude, double longitude) {
			Latitude = latitude;
			Longitude = longitude;
		}

		public override string ToString() {
			return string.Format($"({Latitude}, {Longitude})");
		}
	}
}
