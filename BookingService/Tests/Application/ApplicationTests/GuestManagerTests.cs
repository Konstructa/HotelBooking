using Application.Guest;
using Application.Guest.DTO;
using Application.Guest.Requests;
using Application.Responses;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace ApplicationTests
{
    public class Tests
    {
        GuestManager guestManager;

        [SetUp]
        public void Setup()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(1));

            guestManager = new GuestManager(fakeRepo.Object);   
        }

        [Test]
        public async Task Test1()
        {
            var guestDto = new GuestDTO
            {
                Name = "Fulano",
                Surname = "Beltrano",
                Email = "teste@gmail.com",
                IdNumber = "1412-12A",
                IdTypeCode = 2
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var res = await guestManager.CreateGuest(request);
            Assert.That(res, Is.Not.Null);
            Assert.That(res.Success, Is.True);
        }

        [TestCase("", "teste", "tesom@gmail.com")]
        [TestCase(null, "teste", "tesom@gmail.com")]

        [TestCase("teste", "", "tesom@gmail.com")]
        [TestCase("teste", null, "tesom@gmail.com")]

        [TestCase("teste", "teste", null)]
        [TestCase("teste", "teste", "")]
        public async Task ShouldReturnMissingRequiredInformationWhenDocsAreInvalid(string name, string surname, string email)
        {
            var guestDto = new GuestDTO
            {
                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "1412-12A",
                IdTypeCode = 2
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            var res = await guestManager.CreateGuest(request);

            Assert.That(res, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(res.Success, Is.False);
                Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodes.MISSING_REQUIRED_INFORMATION));
                Assert.That(res.Message, Is.EqualTo("Missing required information"));
            });
        }

        [Test]
        public async Task Should_Return_GuestNotFound_When_GuestDoesntExist()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Get(300)).Returns(Task.FromResult<Guest?>(null));

            guestManager = new GuestManager(fakeRepo.Object);

            var fakeGuest = new Guest
            {
                Id = 333,
                Name = "Test"
            };

            var res = await guestManager.GetGuest(300);

            Assert.That(res, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(res.Success, Is.False);
                Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodes.GUEST_NOT_FOUND));
                Assert.That(res.Message, Is.EqualTo("Guest not found"));
            });
        }

        [Test]
        public async Task Should_Return_Guest_Sucess()
        {
            var fakeGuest = new Guest
            {
                Id = 333,
                Name = "Test",
                DocumentId = new Domain.ValueObjects.PersonId
                {
                    DocumentType = Domain.Enums.DocumentType.DriveLicence,
                    IdNumber = "123"
                }
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Get(300)).Returns(Task.FromResult((Guest?)fakeGuest));

            guestManager = new GuestManager(fakeRepo.Object);


            var res = await guestManager.GetGuest(300);

            Assert.That(res, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(res.Success);
                Assert.That(res.Data.Id, Is.EqualTo(fakeGuest.Id));
                Assert.That(res.Data.Name, Is.EqualTo(fakeGuest.Name));
            });
        }
    }
}