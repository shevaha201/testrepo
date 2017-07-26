using System;

namespace MapOfTheCity
{
    public class PlaceToEat : Place, IRatingable
    {
        private const string _placeToEat = "Places To Eat";
        private float _rating;

        public string ContactInfo;
        public string Schedule;
        public override string TypeOfPlace { get { return _placeToEat; } }
        
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

        public PlaceToEat(string name, double latitude, double longitude, string contactInfo, string schedule, float rating) 
            : base(name, latitude, longitude) {

            ContactInfo = IsStringDataNotNull(contactInfo);
            Schedule = IsStringDataNotNull(schedule);
            Rating = rating;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format($"{ContactInfo} \t {Schedule} \t {Rating}");
        }

        private string IsStringDataNotNull(string value) {
            if (value != null) {
                return value;
            }
            else {
                return "";
            }
        }
    }
}
