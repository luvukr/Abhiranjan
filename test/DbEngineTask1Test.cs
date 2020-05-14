using System;
using DbEngine;
using Xunit;
using FluentAssertions;

namespace test
{
    public class DbEngineTask1Test:IClassFixture<TestFixture>
    {
        TestFixture _fixture;
        private QueryTransform qtr;
        public DbEngineTask1Test(TestFixture fixture)
        {
            _fixture = fixture;
            qtr = _fixture.qtr;
        }

        [Fact]
        public void TestGetSplitStrings()
        {
            //Arrange
            string query = "select * from ipl.csv where season > 2014 and city = 'Bangalore'";

            //Act
        //    QueryTransform qtr = new QueryTransform();
            string[] actual= qtr.GetSplitString(query);
            string[] expected = new string[] { "select" , "*" , "from" , "ipl.csv" , "where" , "season" ,">" , "2014" , "and" ,"city" ,"=" ,"'bangalore'" };
            //Assert
            actual.Should().NotBeNull("because we passed a proper query string and it was not null");
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void TestGetFileName()
        {
            //Arrange
            string query = "select * from ipl.csv where season > 2014 and city = 'Bangalore'";

            //Act
          //  QueryTransform qtr = new QueryTransform();
            string actual = qtr.GetFileName(query);
            string expected = "ipl.csv";

            //Assert
            actual.Should().BeEquivalentTo(expected,"bacause file name provided was ipl.csv");
        }

        [Fact]
        public void TestGetBaseQuery()
        {
            //Arrange
            string query = "select * from ipl.csv where season > 2014 and city = 'Bangalore'";

            //Act
            //QueryTransform qtr = new QueryTransform();
            string actual = qtr.GetBaseQuery(query);
            string expected = "select * from ipl.csv";

            //Assert
            actual.Length.Should().BeGreaterThan(0, "bacause querystring was select * from ipl.csv where season > 2014 and city = 'Bangalore'");
            actual.Should().BeEquivalentTo(expected, "bacause querystring was select * from ipl.csv where season > 2014 and city = 'Bangalore'");
        }
    }

    public class TestFixture : IDisposable
    {
        public QueryTransform qtr { get; private set; }
        public TestFixture()
        {
            qtr = new QueryTransform();
        }
        public void Dispose()
        {
            qtr = null;
        }
    }
}
