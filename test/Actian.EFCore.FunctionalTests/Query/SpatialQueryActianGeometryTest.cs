using System.Linq;
using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestModels.SpatialModel;
using NetTopologySuite.Geometries;
using Xunit;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class SpatialQueryActianGeometryTest : SpatialQueryTestBase<SpatialQueryActianGeometryFixture>
    {
        public SpatialQueryActianGeometryTest(SpatialQueryActianGeometryFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestEnvironment.Log(this, testOutputHelper);
            Helpers = new ActianSqlFixtureHelpers(fixture, testOutputHelper);
        }

        public ActianSqlFixtureHelpers Helpers { get; }
        public void AssertSql(params string[] expected) => Helpers.AssertSql(expected);
        public void LogSql() => Helpers.LogSql();

        [ActianTodo]
        public override async Task SimpleSelect(bool isAsync)
        {
            await base.SimpleSelect(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Geometry"", ""p"".""Point""
                FROM ""PointEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task WithConversion(bool isAsync)
        {
            await base.WithConversion(isAsync);
            AssertSql(@"
                SELECT ""g"".""Id"", ""g"".""Location""
                FROM ""GeoPointEntity"" AS ""g""
            ");
        }

        [ActianTodo]
        public override async Task Area(bool isAsync)
        {
            await base.Area(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Polygon"".STArea() AS ""Area""
                FROM ""PolygonEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task AsBinary(bool isAsync)
        {
            await base.AsBinary(isAsync);
        }

        [ActianTodo]
        public override async Task AsText(bool isAsync)
        {
            await base.AsText(isAsync);
        }

        [ActianTodo]
        public override async Task Boundary(bool isAsync)
        {
            await base.Boundary(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Polygon"".STBoundary() AS ""Boundary""
                FROM ""PolygonEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task Buffer(bool isAsync)
        {
            await base.Buffer(isAsync);
        }

        [ActianTodo]
        public override Task Buffer_quadrantSegments(bool isAsync)
        {
            return Task.CompletedTask;
        }

        [ActianTodo]
        public override async Task Centroid(bool isAsync)
        {
            await base.Centroid(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Polygon"".STCentroid() AS ""Centroid""
                FROM ""PolygonEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task Contains(bool isAsync)
        {
            await base.Contains(isAsync);
        }

        [ActianTodo]
        public override async Task ConvexHull(bool isAsync)
        {
            await base.ConvexHull(isAsync);
        }

        [ActianTodo]
        public override async Task IGeometryCollection_Count(bool isAsync)
        {
            await base.IGeometryCollection_Count(isAsync);
            AssertSql(@"
                SELECT ""m"".""Id"", ""m"".""MultiLineString"".STNumGeometries() AS ""Count""
                FROM ""MultiLineStringEntity"" AS ""m""
            ");
        }

        [ActianTodo]
        public override async Task LineString_Count(bool isAsync)
        {
            await base.LineString_Count(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""LineString"".STNumPoints() AS ""Count""
                FROM ""LineStringEntity"" AS ""l""
            ");
        }

        [ActianTodo]
        public override Task CoveredBy(bool isAsync)
        {
            return base.CoveredBy(isAsync);
        }

        [ActianTodo]
        public override Task Covers(bool isAsync)
        {
            return Task.CompletedTask;
        }

        [ActianTodo]
        public override async Task Crosses(bool isAsync)
        {
            await base.Crosses(isAsync);
        }

        [ActianTodo]
        public override async Task Difference(bool isAsync)
        {
            await base.Difference(isAsync);
        }

        [ActianTodo]
        public override async Task Dimension(bool isAsync)
        {
            await base.Dimension(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Point"".STDimension() AS ""Dimension""
                FROM ""PointEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task Disjoint(bool isAsync)
        {
            await base.Disjoint(isAsync);
        }

        [ActianTodo]
        public override async Task Distance(bool isAsync)
        {
            await base.Distance(isAsync);
        }

        [ActianTodo]
        public override async Task Distance_geometry(bool isAsync)
        {
            await base.Distance_geometry(isAsync);
        }

        [ActianTodo]
        public override async Task Distance_constant(bool isAsync)
        {
            await base.Distance_constant(isAsync);
        }

        [ActianTodo]
        public override async Task Distance_constant_srid_4326(bool isAsync)
        {
            await AssertQuery(
                isAsync,
                ss => ss.Set<PointEntity>().Select(
                    e => new { e.Id, Distance = e.Point == null ? (double?)null : e.Point.Distance(new Point(1, 1) { SRID = 4326 }) }),
                elementSorter: e => e.Id,
                elementAsserter: (e, a) =>
                {
                    Assert.Equal(e.Id, a.Id);
                    Assert.Null(a.Distance);
                });
        }

        [ActianTodo]
        public override async Task Distance_constant_lhs(bool isAsync)
        {
            await base.Distance_constant_lhs(isAsync);
        }

        [ActianTodo]
        public override async Task Distance_on_converted_geometry_type(bool isAsync)
        {
            await base.Distance_on_converted_geometry_type(isAsync);
        }

        [ActianTodo]
        public override async Task Distance_on_converted_geometry_type_lhs(bool isAsync)
        {
            await base.Distance_on_converted_geometry_type_lhs(isAsync);
        }

        [ActianTodo]
        public override async Task Distance_on_converted_geometry_type_constant(bool isAsync)
        {
            await base.Distance_on_converted_geometry_type_constant(isAsync);
            AssertSql(@"
                SELECT ""g"".""Id"", ""g"".""Location"".STDistance(geometry::Parse('POINT (0 1)')) AS ""Distance""
                FROM ""GeoPointEntity"" AS ""g""
            ");
        }

        [ActianTodo]
        public override async Task Distance_on_converted_geometry_type_constant_lhs(bool isAsync)
        {
            await base.Distance_on_converted_geometry_type_constant_lhs(isAsync);
            AssertSql(@"
                SELECT ""g"".""Id"", geometry::Parse('POINT (0 1)').STDistance(""g"".""Location"") AS ""Distance""
                FROM ""GeoPointEntity"" AS ""g""
            ");
        }

        [ActianTodo]
        public override async Task EndPoint(bool isAsync)
        {
            await base.EndPoint(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""LineString"".STEndPoint() AS ""EndPoint""
                FROM ""LineStringEntity"" AS ""l""
            ");
        }

        [ActianTodo]
        public override async Task Envelope(bool isAsync)
        {
            await base.Envelope(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Polygon"".STEnvelope() AS ""Envelope""
                FROM ""PolygonEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task EqualsTopologically(bool isAsync)
        {
            await base.EqualsTopologically(isAsync);
        }

        [ActianTodo]
        public override async Task ExteriorRing(bool isAsync)
        {
            await base.ExteriorRing(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Polygon"".STExteriorRing() AS ""ExteriorRing""
                FROM ""PolygonEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task GeometryType(bool isAsync)
        {
            await base.GeometryType(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Point"".STGeometryType() AS ""GeometryType""
                FROM ""PointEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task GetGeometryN(bool isAsync)
        {
            await base.GetGeometryN(isAsync);
        }

        [ActianTodo]
        public override async Task GetInteriorRingN(bool isAsync)
        {
            await base.GetInteriorRingN(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", CASE
                    WHEN ""p"".""Polygon"" IS NULL OR (""p"".""Polygon"".STNumInteriorRing() = 0) THEN NULL
                    ELSE ""p"".""Polygon"".STInteriorRingN(0 + 1)
                END AS ""InteriorRing0""
                FROM ""PolygonEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task GetPointN(bool isAsync)
        {
            await base.GetPointN(isAsync);
        }

        [ActianTodo]
        public override async Task InteriorPoint(bool isAsync)
        {
            await base.InteriorPoint(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Polygon"".STPointOnSurface() AS ""InteriorPoint"", ""p"".""Polygon""
                FROM ""PolygonEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task Intersection(bool isAsync)
        {
            await base.Intersection(isAsync);
        }

        [ActianTodo]
        public override async Task Intersects(bool isAsync)
        {
            await base.Intersects(isAsync);
        }

        [ActianTodo]
        public override async Task ICurve_IsClosed(bool isAsync)
        {
            await base.ICurve_IsClosed(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""LineString"".STIsClosed() AS ""IsClosed""
                FROM ""LineStringEntity"" AS ""l""
            ");
        }

        [ActianTodo]
        public override async Task IMultiCurve_IsClosed(bool isAsync)
        {
            await base.IMultiCurve_IsClosed(isAsync);
            AssertSql(@"
                SELECT ""m"".""Id"", ""m"".""MultiLineString"".STIsClosed() AS ""IsClosed""
                FROM ""MultiLineStringEntity"" AS ""m""
            ");
        }

        [ActianTodo]
        public override async Task IsEmpty(bool isAsync)
        {
            await base.IsEmpty(isAsync);
            AssertSql(@"
                SELECT ""m"".""Id"", ""m"".""MultiLineString"".STIsEmpty() AS ""IsEmpty""
                FROM ""MultiLineStringEntity"" AS ""m""
            ");
        }

        [ActianTodo]
        public override async Task IsRing(bool isAsync)
        {
            await base.IsRing(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""LineString"".STIsRing() AS ""IsRing""
                FROM ""LineStringEntity"" AS ""l""
            ");
        }

        [ActianTodo]
        public override async Task IsSimple(bool isAsync)
        {
            await base.IsSimple(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""LineString"".STIsSimple() AS ""IsSimple""
                FROM ""LineStringEntity"" AS ""l""
            ");
        }

        [ActianTodo]
        public override async Task IsValid(bool isAsync)
        {
            await base.IsValid(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Point"".STIsValid() AS ""IsValid""
                FROM ""PointEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task IsWithinDistance(bool isAsync)
        {
            await base.IsWithinDistance(isAsync);
        }

        [ActianTodo]
        public override async Task Item(bool isAsync)
        {
            await base.Item(isAsync);
        }

        [ActianTodo]
        public override async Task Length(bool isAsync)
        {
            await base.Length(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""LineString"".STLength() AS ""Length""
                FROM ""LineStringEntity"" AS ""l""
            ");
        }

        [ActianTodo]
        public override async Task M(bool isAsync)
        {
            await base.M(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Point"".M AS ""M""
                FROM ""PointEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task NumGeometries(bool isAsync)
        {
            await base.NumGeometries(isAsync);
            AssertSql(@"
                SELECT ""m"".""Id"", ""m"".""MultiLineString"".STNumGeometries() AS ""NumGeometries""
                FROM ""MultiLineStringEntity"" AS ""m""
            ");
        }

        [ActianTodo]
        public override async Task NumInteriorRings(bool isAsync)
        {
            await base.NumInteriorRings(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Polygon"".STNumInteriorRing() AS ""NumInteriorRings""
                FROM ""PolygonEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task NumPoints(bool isAsync)
        {
            await base.NumPoints(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""LineString"".STNumPoints() AS ""NumPoints""
                FROM ""LineStringEntity"" AS ""l""
            ");
        }

        [ActianTodo]
        public override async Task OgcGeometryType(bool isAsync)
        {
            await base.OgcGeometryType(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", CASE ""p"".""Point"".STGeometryType()
                    WHEN N'Point' THEN 1
                    WHEN N'LineString' THEN 2
                    WHEN N'Polygon' THEN 3
                    WHEN N'MultiPoint' THEN 4
                    WHEN N'MultiLineString' THEN 5
                    WHEN N'MultiPolygon' THEN 6
                    WHEN N'GeometryCollection' THEN 7
                    WHEN N'CircularString' THEN 8
                    WHEN N'CompoundCurve' THEN 9
                    WHEN N'CurvePolygon' THEN 10
                END AS ""OgcGeometryType""
                FROM ""PointEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task Overlaps(bool isAsync)
        {
            await base.Overlaps(isAsync);
        }

        [ActianTodo]
        public override async Task PointOnSurface(bool isAsync)
        {
            await base.PointOnSurface(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Polygon"".STPointOnSurface() AS ""PointOnSurface"", ""p"".""Polygon""
                FROM ""PolygonEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task Relate(bool isAsync)
        {
            await base.Relate(isAsync);
        }

        [ActianTodo]
        public override Task Reverse(bool isAsync)
        {
            return base.Reverse(isAsync);
        }

        [ActianTodo]
        public override async Task SRID(bool isAsync)
        {
            await base.SRID(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Point"".STSrid AS ""SRID""
                FROM ""PointEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task SRID_geometry(bool isAsync)
        {
            await base.SRID_geometry(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Geometry"".STSrid AS ""SRID""
                FROM ""PointEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task StartPoint(bool isAsync)
        {
            await base.StartPoint(isAsync);
            AssertSql(@"
                SELECT ""l"".""Id"", ""l"".""LineString"".STStartPoint() AS ""StartPoint""
                FROM ""LineStringEntity"" AS ""l""
            ");
        }

        [ActianTodo]
        public override async Task SymmetricDifference(bool isAsync)
        {
            await base.SymmetricDifference(isAsync);
        }

        [ActianTodo]
        public override async Task ToBinary(bool isAsync)
        {
            await base.ToBinary(isAsync);
        }

        [ActianTodo]
        public override async Task ToText(bool isAsync)
        {
            await base.ToText(isAsync);
        }

        [ActianTodo]
        public override async Task Touches(bool isAsync)
        {
            await base.Touches(isAsync);
        }

        [ActianTodo]
        public override async Task Union(bool isAsync)
        {
            await base.Union(isAsync);
        }

        [ActianTodo]
        public override Task Union_void(bool isAsync)
        {
            return Task.CompletedTask;
        }

        [ActianTodo]
        public override async Task Within(bool isAsync)
        {
            await base.Within(isAsync);
        }

        [ActianTodo]
        public override async Task X(bool isAsync)
        {
            await base.X(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Point"".STX AS ""X""
                FROM ""PointEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task Y(bool isAsync)
        {
            await base.Y(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Point"".STY AS ""Y""
                FROM ""PointEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task Z(bool isAsync)
        {
            await base.Z(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Point"".Z AS ""Z""
                FROM ""PointEntity"" AS ""p""
            ");
        }
    }
}
