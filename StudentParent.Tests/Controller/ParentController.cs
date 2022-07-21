using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using StudentParent_WebApI.Controllers;
using StudentParent_WebApI.Dto;
using StudentParent_WebApI.Interface;
using StudentParent_WebApI.Models;


namespace StudentParent.Tests.Controller
{
    public class ParentControllerTest
    {

        private readonly IParentRepository _parentRepository;
        private readonly IMapper _mapper;
        private readonly ISchoolClubRepository _schoolClubRepository;
        public ParentControllerTest()
        {
            _parentRepository = A.Fake<IParentRepository>();
            _schoolClubRepository = A.Fake<ISchoolClubRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void ParentController_Getparents_ReturnOK()
        {
            //Arrange
            var parents = A.Fake<ICollection<ParentDto>>();
            var parentList = A.Fake<List<ParentDto>>();

            //using fakeItEasy to call map (ParentDto and parents)
            A.CallTo(() => _mapper.Map<List<ParentDto>>(parents)).Returns(parentList);


            // to instantiate ParentController
            var controller = new ParentController(_parentRepository, _mapper, _schoolClubRepository);

            //Act
            var result = controller.GetParents();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }


        [Fact]
        public void ParentController_UpdateParent_ReturnOK()
        {
            //Arrange
            var parentId = 1;
            var parent = A.Fake<Parent>();
            var parentUpdate = A.Fake<ParentDto>();
            A.CallTo(() => _mapper.Map<Parent>(parentUpdate)).Returns(parent);


            var controller = new ParentController(_parentRepository , _mapper, _schoolClubRepository);
            //Act 
            var result = controller.UpdateParent(parentId, parentUpdate);
            //Assert
            result.Should().NotBeNull();
        }
    }
}

