using HW4.Utilities;

namespace HW4_Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FormatMethods_FormatMovieRuntime_LessThanOneHourRuntime_ReturnsCorrectMinutes()
    {
        // Arrange
        var runtime = 59;
        var expected = "59 minutes";
        // Act
        var actual = FormatMethods.FormatMovieRuntime(runtime);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void FormatMethods_FormatMovieRuntime_OneHourRuntime_ReturnsOneHour()
    {
        // Arrange
        var runtime = 60;
        var expected = "1 hour";
        // Act
        var actual = FormatMethods.FormatMovieRuntime(runtime);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void FormatMethods_FormatMovieRuntime_TwoHoursNoMinutesRuntime_ReturnsTwoHours()
    {
        // Arrange
        var runtime = 120;
        var expected = "2 hours";
        // Act
        var actual = FormatMethods.FormatMovieRuntime(runtime);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void FormatMethods_FormatMovieRuntime_OneHourOneMinuteRuntime_ReturnsOneHourOneMinute()
    {
        // Arrange
        var runtime = 61;
        var expected = "1 hour 1 minute";
        // Act
        var actual = FormatMethods.FormatMovieRuntime(runtime);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }    

    [Test]
    public void FormatMethods_FormatMovieRuntime_OneHour30MinutesRuntime_ReturnsOneHour30Minutes()
    {
        // Arrange
        var runtime = 90;
        var expected = "1 hour 30 minutes";
        // Act
        var actual = FormatMethods.FormatMovieRuntime(runtime);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void FormatMethods_FormatMovieRuntime_TwoHoursOneMinuteRuntime_ReturnsTwoHoursOneMinute()
    {
        // Arrange
        var runtime = 121;
        var expected = "2 hours 1 minute";
        // Act
        var actual = FormatMethods.FormatMovieRuntime(runtime);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void FormatMethods_FormatMovieRuntime_TwoHours35MinutesRuntime_ReturnsTwoHours35Minutes()
    {
        // Arrange
        var runtime = 155;
        var expected = "2 hours 35 minutes";
        // Act
        var actual = FormatMethods.FormatMovieRuntime(runtime);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void FormatMethods_FormatMovieRuntime_ZeroMinutes_ReturnsNA()
    {
        // Arrange
        var runtime = 0;
        var expected = "N/A";
        // Act
        var actual = FormatMethods.FormatMovieRuntime(runtime);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void FormatMethods_FormatMovieReleaseDate_NullReleaseDate_ReturnsNA()
    {
        // Arrange
        string releaseDate = null;
        var expected = "N/A";
        // Act
        var actual = FormatMethods.FormatMovieReleaseDate(releaseDate);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void FormatMethods_FormatMovieReleaseDate_EmptyStringReleaseDate_ReturnsNA()
    {
        // Arrange
        var releaseDate = "";
        var expected = "N/A";
        // Act
        var actual = FormatMethods.FormatMovieReleaseDate(releaseDate);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void FormatMethods_FormatMovieReleaseDate_ValidReleaseDate_ReturnsCorrectReleaseDate()
    {
        // Arrange
        string releaseDate = "1980-07-25";
        var expected = "July 25, 1980";
        // Act
        var actual = FormatMethods.FormatMovieReleaseDate(releaseDate);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void FormatMethods_FormatMovieRevenue_ZeroRevenue_ReturnsNA()
    {
        // Arrange
        var revenue = 0;
        var expected = "N/A";
        // Act
        var actual = FormatMethods.FormatMovieRevenue(revenue);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void FormatMethods_FormatMovieRevenue_ValidRevenue_ReturnsCorrectRevenue()
    {
        // Arrange
        var revenue = 1000000;
        var expected = "$1,000,000";
        // Act
        var actual = FormatMethods.FormatMovieRevenue(revenue);
        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }
}