using NUnit.Framework;
using StreamingTinderClassLibrary.StreamingService;
using StreaminTinderClassLibrary.Authentication;
using StreaminTinderClassLibrary.Hashing;
using StreaminTinderClassLibrary.Users;
using StreaminTinderClassLibrary.Users.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.Test
{
    [TestFixture]
    public class StreamingPlatformServiceTests
    {


        private IStreamingPlatformService _streamingPlatformService;

        [SetUp]
        public void SetUp()
        {
            _streamingPlatformService = ServiceFactory.GetStreamingPlatformService();
        }


        [Test]
        public void GetStreamingPlatforms_Valid_ShouldReturnListOfIStreamingPlatforms()
        {
            // Arrange
            List<IStreamingPlatform> streamingPlatforms;

            // Act
            streamingPlatforms = _streamingPlatformService.GetStreamingPlatforms();

            // Assert
            Assert.IsNotNull(streamingPlatforms);
            Assert.IsNotEmpty(streamingPlatforms);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetStreamingPlatformById_ExistingPlatform_ShouldReturnIStreamingPlatform(int platformID)
        {
            // Arrange
            IStreamingPlatform streamingPlatform;

            // Act
            streamingPlatform = _streamingPlatformService.GetStreamingPlatformById(platformID);

            // Assert
            Assert.IsNotNull(streamingPlatform);
            Assert.IsNotNull(streamingPlatform.Id);
            Assert.IsNotNull(streamingPlatform.Name);
            Assert.IsNotEmpty(streamingPlatform.Name);
        }

        [Test]
        [TestCase(null)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void GetStreamingPlatformById_InvalidInput_ShouldThrowArgumentException(int platformID)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _streamingPlatformService.GetStreamingPlatformById(platformID));
        }

        [Test]
        [TestCase(100)]
        [TestCase(150)]
        [TestCase(int.MaxValue)]
        public void GetStreamingPlatformById_NonExistingPlatform_ShouldReturnNull(int platformID)
        {
            // Arrange
            IStreamingPlatform streamingPlatform;

            // Act
            streamingPlatform = _streamingPlatformService.GetStreamingPlatformById(platformID);

            // Assert
            Assert.IsNull(streamingPlatform);
        }

        [Test]
        [TestCase("Netflix")]
        [TestCase("HBO")]
        [TestCase("Viaplay")]
        public void GetStreamingPlatformByName_ExistingPlatform_ShouldReturnIStreamingPlatform(string platformName)
        {
            // Arrange
            IStreamingPlatform streamingPlatform;

            // Act
            streamingPlatform = _streamingPlatformService.GetStreamingPlatformByName(platformName);

            // Assert
            Assert.IsNotNull(streamingPlatform);
            Assert.IsNotNull(streamingPlatform.Id);
            Assert.IsNotNull(streamingPlatform.Name);
            Assert.IsNotEmpty(streamingPlatform.Name);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void GetStreamingPlatformByName_InvalidInput_ShouldThrowArgumentException(string platformName)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>( () => _streamingPlatformService.GetStreamingPlatformByName(platformName));
        }

        [Test]
        [TestCase("qweqweqwe")]
        [TestCase("qwesadzxczxcc")]
        public void GetStreamingPlatformByName_NonExistingPlatform_ShouldReturnNull(string platformName)
        {
            // Arrange
            IStreamingPlatform streamingPlatform;

            // Act
            streamingPlatform = _streamingPlatformService.GetStreamingPlatformByName(platformName);

            // Assert
            Assert.IsNull(streamingPlatform);
        }

    }
}
