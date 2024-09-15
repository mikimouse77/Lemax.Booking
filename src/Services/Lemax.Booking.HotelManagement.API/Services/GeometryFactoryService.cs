using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Lemax.Booking.HotelManagement.API.Services
{
    public class GeometryFactoryService
    {
        private readonly GeometryFactory _geometryFactory;

        public GeometryFactoryService()
        {
            _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        }

        public Point CreatePoint(double longitude, double latitude)
        {
            return _geometryFactory.CreatePoint(new Coordinate(longitude, latitude));
        }
    }
}
