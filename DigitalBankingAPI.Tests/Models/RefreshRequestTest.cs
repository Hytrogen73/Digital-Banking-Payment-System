using System;
using System.Collections.Generic;
using System.Text;
using DigitalBankingAPI.Models;
using Xunit;
using System.ComponentModel.DataAnnotations;

namespace DigitalBankingAPI.Tests.Models;

public class RefreshRequestTest
{
    [Fact]
    public void RefreshToken_WhenEmpty_ShouldFailValidation()
    {
        // Arrange
        var request = new RefreshRequest();

        var context = new ValidationContext(request);
        var results = new List<ValidationResult>();

        // Act
        bool isValid = Validator.TryValidateObject(
            request,
            context,
            results,
            validateAllProperties: true);

        // Assert
        Assert.False(isValid);
    }
    [Fact]
    public void RefreshToken_WhenTooLong_ShouldFailValidation()
    {
        // Arrange
        var request = new RefreshRequest
        {
            RefreshToken = new string('A', 101)
        };

        // Act and Assert
        var context = new ValidationContext(request);

        Assert.Throws<ValidationException>(() =>
            Validator.ValidateObject(
                request,
                context,
                validateAllProperties: true));
    }
    [Fact]
    public void RefreshToken_WhenValid_ShouldPassValidation()
    {
        // Arrange
        var request = new RefreshRequest
        {
            RefreshToken = new string('A', 86)
        };

        var context = new ValidationContext(request);

        // Act
        bool isValid = Validator.TryValidateObject(
            request,
            context,
            null,
            validateAllProperties: true);

        // Assert
        Assert.True(isValid);
    }
}
