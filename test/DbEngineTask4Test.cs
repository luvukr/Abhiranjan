using System;
using DbEngine;
using Xunit;
using FluentAssertions;

namespace test
{
    public class DbEngineTask4Test:IClassFixture<TestFixture4>
    {
        TestFixture4 _fixture;
        private QueryTransform qtr;
        public DbEngineTask4Test(TestFixture4 fixture)
        {
            _fixture = fixture;
            qtr = _fixture.Qtr;
        }

        [Fact]
        public void TestGetGroupByFields()
        {
            //Arrange
            string query = "select team1, city from ipl.csv where season > 2016 and city='Bangalore' group by team1";

            //Act
            string[] actual = qtr.GetGroupByFields(query);
            string[] expected = new string[] { "team1" };

            //Assert
            actual.Should().NotBeNull("because we passed the query with one group by construct");
            actual.Length.Should().BeGreaterThan(0, "because we passed the query with one group by construct");
            actual.Should().BeEquivalentTo(expected, "because we passed the query with one group by construct");
        }

        [Fact]
        public void TestGetAggregateFunctions()
        {
            //Arrange
            string query = "select avg(win_by_wickets),min(win_by_runs) from ipl.csv";

            //Act
            string[] actual = qtr.GetAggregateFunctions(query);
            string[] expected = new string[] { "avg(win_by_wickets)", "min(win_by_runs)" };

            //Assert
            actual.Should().NotBeNull("because we passed the query with avg and min functions");
            actual.Length.Should().BeGreaterThan(0, "because we passed the query with avg and min functions");
            actual.Should().BeEquivalentTo(expected, "because we passed the query with avg and min functions");
        }
    }

    public class TestFixture4 : IDisposable
    {
        public QueryTransform Qtr { get; private set; }
        public TestFixture4()
        {
            Qtr = new QueryTransform();
        }
        public void Dispose()
        {
            Qtr = null;
        }
    }
}
