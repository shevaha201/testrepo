using System;

namespace MapOfTheCity {
	public class HistoricalMonument : PublicPlace, IRatingable {

		private const string _historicalMonument = "Historical Monument";
		private float _rating;

		public override string TypeOfPlace { get { return _historicalMonument; } }

		public float Rating {
			get {
				return _rating;
			}
			set {
				if (value > 0 && value <= 5)
					_rating = value;
				else
					throw new Exception("Invalid Rating");
			}
		}

		public HistoricalMonument(string name, double latitude, double longitude, float rating)
			: base(name, latitude, longitude) {
			Rating = rating;
		}

		public override string ToString() {
			return base.ToString() + string.Format($"{Rating}");
		}
	}
}
