using System;
using DbEngine;
using Xunit;
using FluentAssertions;

namespace test
{
    public class DbEngineTask2Test:IClassFixture<TestFixture2>
    {
        TestFixture2 _fixture;
        private QueryTransform qtr;
        public DbEngineTask2Test(TestFixture2 fixture)
        {
            _fixture = fixture;
            qtr = _fixture.Qtr;
        }

        [Fact]
        public void TestGetFieldNames()
        {
            //Arrange
            string query = "select city,winner,player_match from ipl.csv where season > 2014";

            //Act
            string[] actual= qtr.GetFieldsNames(query);
            string[] expected = new string[] { "city","winner","player_match" };
            //Assert
            actual.Should().NotBeNull("because we passed field names city,winner,player_match in our query");
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void TestGetConditionsPartQuery()
        {
            //Arrange
            string query = "select * from ipl.csv where season > 2014 and city = 'Bangalore'";

            //Act
            string actual = qtr.GetConditionsPartQuery(query);
            string expected = "season > 2014 and city = 'bangalore'";

            //Assert
            actual.Should().BeEquivalentTo(expected, "because we passed the query with where clause season > 2014 and city ='bangalore'");
        }

        [Fact]
        public void TestGetConditions()
        {
            //Arrange
            string query = "select * from ipl.csv where season > 2014 and city = 'Bangalore'";

            //Act
            string[] actual = qtr.GetConditions(query);
            string[] expected = new string[] { "season > 2014","city = 'bangalore'" };

            //Assert
            actual.Length.Should().BeGreaterThan(0, "because querystring was select * from ipl.csv where season > 2014 and city = 'Bangalore'");
            actual.Should().BeEquivalentTo(expected, "because querystring was select * from ipl.csv where season > 2014 and city = 'Bangalore'");
        }
    }

    public class TestFixture2 : IDisposable
    {
        public QueryTransform Qtr { get; private set; }
        public TestFixture2()
        {
            Qtr = new QueryTransform();
        }
        public void Dispose()
        {
            Qtr = null;
        }
    }
}
