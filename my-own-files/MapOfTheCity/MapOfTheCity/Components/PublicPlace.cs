using System.Linq;

namespace MapOfTheCity
{
    public class PublicPlace : Place
    {
        private const string _publicPlace = "Public Place";
        public override string TypeOfPlace { get { return _publicPlace; }  }        

        public PublicPlace(string name, double latitude, double longitude) 
            : base (name, latitude, longitude) {
        }

        public PlaceToEat GetNearestPlaceToEat(MapOfCity map) {
           return map.Places
                .Where(t => t is PlaceToEat)
                .Select(t => (PlaceToEat)t)
                .OrderByDescending(t => t.GetDistance(this))
                .First();
        }
    }
}
