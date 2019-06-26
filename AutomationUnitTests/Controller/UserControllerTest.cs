using AutomationFrontEnd.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutomationUnitTests.Controller
{
    public class UserControllerTest
    {
        [Fact]
        public void ConstuctorReturnsContoller()
        {
            var controller = new UserController();

            Assert.IsType<UserController>(controller);
        }
    }
}
