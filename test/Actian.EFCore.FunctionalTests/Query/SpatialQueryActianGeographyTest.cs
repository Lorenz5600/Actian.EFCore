using System.Threading.Tasks;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Query;
using Xunit.Abstractions;

namespace Actian.EFCore.Query
{
    public class SpatialQueryActianGeographyTest : SpatialQueryTestBase<SpatialQueryActianGeographyFixture>
    {
        public SpatialQueryActianGeographyTest(SpatialQueryActianGeographyFixture fixture, ITestOutputHelper testOutputHelper)
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
        public override Task Boundary(bool isAsync)
        {
            return Task.CompletedTask;
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
        public override Task Centroid(bool isAsync)
        {
            return Task.CompletedTask;
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
            return Task.CompletedTask;
        }

        [ActianTodo]
        public override Task Covers(bool isAsync)
        {
            return Task.CompletedTask;
        }

        [ActianTodo]
        public override Task Crosses(bool isAsync)
        {
            return Task.CompletedTask;
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
        public override Task Distance_constant(bool isAsync)
        {
            return Task.CompletedTask;
        }

        [ActianTodo]
        public override async Task Distance_constant_srid_4326(bool isAsync)
        {
            await base.Distance_constant_srid_4326(isAsync);
        }

        [ActianTodo]
        public override Task Distance_constant_lhs(bool isAsync)
        {
            return Task.CompletedTask;
        }

        [ActianTodo]
        public override async Task Distance_on_converted_geometry_type(bool isAsync)
        {
            await base.Distance_on_converted_geometry_type(isAsync);
            AssertSql(@"
                @__point_0='0xE6100000010C000000000000F03F0000000000000000' (Nullable = false) (Size = 22) (DbType = Object)
                
                SELECT ""g"".""Id"", ""g"".""Location"".STDistance(@__point_0) AS ""Distance""
                FROM ""GeoPointEntity"" AS ""g""
            ");
        }

        [ActianTodo]
        public override async Task Distance_on_converted_geometry_type_lhs(bool isAsync)
        {
            await base.Distance_on_converted_geometry_type_lhs(isAsync);
            AssertSql(@"
                @__point_0='0xE6100000010C000000000000F03F0000000000000000' (Nullable = false) (Size = 22) (DbType = Object)
                
                SELECT ""g"".""Id"", @__point_0.STDistance(""g"".""Location"") AS ""Distance""
                FROM ""GeoPointEntity"" AS ""g""
            ");
        }

        [ActianTodo]
        public override Task Distance_on_converted_geometry_type_constant(bool isAsync)
        {
            return Task.CompletedTask;
        }

        [ActianTodo]
        public override Task Distance_on_converted_geometry_type_constant_lhs(bool isAsync)
        {
            return Task.CompletedTask;
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
        public override Task Envelope(bool isAsync)
        {
            return Task.CompletedTask;
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
                SELECT ""p"".""Id"", ""p"".""Polygon"".RingN(1) AS ""ExteriorRing""
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
                    WHEN ""p"".""Polygon"" IS NULL OR ((""p"".""Polygon"".NumRings() - 1) = 0) THEN NULL
                    ELSE ""p"".""Polygon"".RingN(0 + 2)
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
        public override Task InteriorPoint(bool isAsync)
        {
            return Task.CompletedTask;
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
        public override Task IsRing(bool isAsync)
        {
            return Task.CompletedTask;
        }

        [ActianTodo]
        public override Task IsSimple(bool isAsync)
        {
            return Task.CompletedTask;
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
            AssertSql(@"
                @__point_0='0xE6100000010C000000000000F03F0000000000000000' (Size = 22) (DbType = Object)
                
                SELECT ""p"".""Id"", CASE
                    WHEN ""p"".""Point"" IS NULL THEN NULL
                    ELSE CASE
                        WHEN ""p"".""Point"".STDistance(@__point_0) <= 1.0E0 THEN TRUE
                        ELSE FALSE
                    END
                END AS ""IsWithinDistance""
                FROM ""PointEntity"" AS ""p""
            ");
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
                SELECT ""p"".""Id"", ""p"".""Polygon"".NumRings() - 1 AS ""NumInteriorRings""
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
                    WHEN N'FullGlobe' THEN 126
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
        public override Task PointOnSurface(bool isAsync)
        {
            return Task.CompletedTask;
        }

        [ActianTodo]
        public override Task Relate(bool isAsync)
        {
            return Task.CompletedTask;
        }

        [ActianTodo]
        public override Task Reverse(bool isAsync)
        {
            return Task.CompletedTask;
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
        public override Task Touches(bool isAsync)
        {
            return Task.CompletedTask;
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
                SELECT ""p"".""Id"", ""p"".""Point"".Long AS ""X""
                FROM ""PointEntity"" AS ""p""
            ");
        }

        [ActianTodo]
        public override async Task Y(bool isAsync)
        {
            await base.Y(isAsync);
            AssertSql(@"
                SELECT ""p"".""Id"", ""p"".""Point"".Lat AS ""Y""
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
