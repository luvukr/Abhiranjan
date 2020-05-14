using System;
using DbEngine;
using Xunit;
using FluentAssertions;

namespace test
{
    public class DbEngineTask3Test:IClassFixture<TestFixture3>
    {
        TestFixture3 _fixture;
        private QueryTransform qtr;
        public DbEngineTask3Test(TestFixture3 fixture)
        {
            _fixture = fixture;
            qtr = _fixture.Qtr;
        }

        [Fact]
        public void TestGetLogicalOperators()
        {
            //Arrange
            string query = "select season,winner,player_match from ipl.csv where season > 2014 and city = 'Bangalore' or date > '31-12-2014'";

            //Act
            string[] actual = qtr.GetLogicalOperators(query);
            string[] expected = new string[] { "and", "or" };

            //Assert
            actual.Should().NotBeNull("because we passed the query with and/or conditions");
            actual.Length.Should().BeGreaterThan(0, "because we passed the query with and/or conditions");
            actual.Should().BeEquivalentTo(expected, "because we passed the query with and/or conditions");
        }

        [Fact]
        public void TestGetOrderByFields()
        {
            //Arrange
            string query = "select * from ipl.csv where season > 2016 and city= 'Bangalore' order by win_by_runs";

            //Act
            string[] actual = qtr.GetOrderByFields(query);
            string[] expected = new string[] { "win_by_runs"};

            //Assert
            actual.Should().NotBeNull("because we passed the query with one order by construct");
            actual.Length.Should().BeGreaterThan(0, "because we passed the query with one order by construct");
            actual.Should().BeEquivalentTo(expected, "because we passed the query with one order by construct");
        }
    }

    public class TestFixture3 : IDisposable
    {
        public QueryTransform Qtr { get; private set; }
        public TestFixture3()
        {
            Qtr = new QueryTransform();
        }
        public void Dispose()
        {
            Qtr = null;
        }
    }
}
