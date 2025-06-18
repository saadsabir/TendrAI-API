using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Infrastructure.Adapters.Parsers;
using Xunit;

namespace TendrAI.Tests.Infrastructure
{
    public class AppelOffreParserTests
    {
        private readonly AppelOffreParser _parser;

        public AppelOffreParserTests()
        {
            _parser = new AppelOffreParser();
        }

        [Fact]
        public void ParseAppelOffre_Should_ParseStructuredTextCorrectly()
        {
            // Arrange
            var rawResponse = """
            Titre : Fourniture de matériel informatique
            Description : Achat d’ordinateurs pour les écoles.
            Date limite : 2025-11-30
            """
            ;

            // Act
            var result = _parser.ParseAppelOffre(rawResponse);

            // Assert
            Assert.Equal("Fourniture de matériel informatique", result.Titre);
            Assert.Equal("Achat d’ordinateurs pour les écoles.", result.Description);
            Assert.Equal(new DateTime(2025, 11, 30), result.DateLimite);
        }

        [Fact]
        public void ParseAppelOffre_Should_HandleMissingFieldsGracefully()
        {
            // Arrange
            var rawResponse = """
            Titre : Marché de nettoyage
            Date limite : 2025-08-01
            """
            ;

            // Act
            var result = _parser.ParseAppelOffre(rawResponse);

            // Assert
            Assert.Equal("Marché de nettoyage", result.Titre);
            Assert.Null(result.Description);
            Assert.Equal(new DateTime(2025, 8, 1), result.DateLimite);
        }

        [Fact]
        public void ParseAppelOffre_Should_Return_MinDate_IfDateInvalid()
        {
            // Arrange
            var rawResponse = """
            Titre : Fourniture
            Description : Test
            Date limite : not-a-date
            """
            ;

            // Act
            var result = _parser.ParseAppelOffre(rawResponse);

            // Assert
            Assert.Equal(DateTime.MinValue, result.DateLimite);
        }

        [Fact]
        public void TryParseFrenchDate_InvalidInput_ReturnsMinValue()
        {
            // Act
            var result = _parser.TryParseFrenchDate("not a date");

            // Assert
            Assert.Equal(DateTime.MinValue, result);
        }

        [Fact]
        public void TryParseFrenchDate_Should_Parse_Correctly()
        {
            //Act
            var result = _parser.TryParseFrenchDate("Vendredi 17 septembre 2021 à 15h00 GMT");

            //Assert
            Assert.Equal(new DateTime(2021, 9, 17, 15, 0, 0), result);
        }
    }
}
