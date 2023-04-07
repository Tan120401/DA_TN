using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Resources;
using MISA.AMIS.DL.EmployeeDL;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.UnitTests
{
    public class EmployeeBLTests
    {
        #region Field

        private IEmployeeBL _employeeBL;
        private IEmployeeDL _fakeEmployeeDL;

        #endregion

        [SetUp]
        public void SetUp()
        {
            _fakeEmployeeDL = Substitute.For<IEmployeeDL>();
            _employeeBL = new EmployeeBL(_fakeEmployeeDL);
        }

        /// <summary>
        /// Test mã nhân viên để trống
        /// Created By: NVTAN (09/02/2023)
        /// </summary>
        [Test]
        public void InsertRecord_EmptyCode_ReturnsInvalid()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeCode = String.Empty,
                FullName = "NVTan",
                DateOfBirth = DateTime.Now,
                Gender = Common.Enums.Gender.Male,
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "CNTT",
                PositionId = Guid.NewGuid(),
                PositionName = "GĐ",
                Address = "HN",
                Email = "T@gmail.com"
            };

            _fakeEmployeeDL.InsertRecord(employee).Returns(0);

            var expectedResult = new ServiceResult
            {
                IsSuccess = false,
                ErrorCode = Common.Enums.ErrorCode.IsValidData,
                Message = "Dữ liệu đầu vào không hợp lệ!",
                Data = new List<string>(){ "Mã nhân viên không được bỏ trống" },
            };

            // Act
            var actualResult = _employeeBL.InsertRecord(employee);

            // Assert
            Assert.That(expectedResult.IsSuccess, Is.EqualTo(actualResult.IsSuccess));
            Assert.That(expectedResult.ErrorCode, Is.EqualTo(actualResult.ErrorCode));
            Assert.That(expectedResult.Data, Is.EqualTo(actualResult.Data));
        }

        /// <summary>
        /// Test tên nhân viên trống
        /// Created By: NVTAN (09/02/2023)
        /// </summary>
        [Test]
        public void InsertRecord_EmptyFullName_ReturnsInvalid()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeCode = "NV-99999",
                FullName = "",
                DateOfBirth = DateTime.Now,
                Gender = Common.Enums.Gender.Male,
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "CNTT",
                PositionId = Guid.NewGuid(),
                PositionName = "GĐ",
                Address = "HN",
                Email = "T@gmail.com"
            };

            _fakeEmployeeDL.InsertRecord(employee).Returns(0);

            var expectedResult = new ServiceResult
            {
                IsSuccess = false,
                ErrorCode = Common.Enums.ErrorCode.IsValidData,
                Message = "Dữ liệu đầu vào không hợp lệ!",
                Data = new List<string>() { "Tên nhân viên không được bỏ trống" },
            };

            // Act
            var actualResult = _employeeBL.InsertRecord(employee);

            // Assert
            Assert.That(expectedResult.IsSuccess, Is.EqualTo(actualResult.IsSuccess));
            Assert.That(expectedResult.ErrorCode, Is.EqualTo(actualResult.ErrorCode));
            Assert.That(expectedResult.Data, Is.EqualTo(actualResult.Data));
        }

        /// <summary>
        /// Test insert thành công
        /// Created By: NVTAN (09/02/2023)
        /// </summary>
        [Test]
        public void InsertRecord_ValidData_ReturnsValidDataResult()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeCode = "NV-99999",
                FullName = "NVTAN",
                DateOfBirth = DateTime.Now,
                Gender = Common.Enums.Gender.Male,
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "CNTT",
                PositionId = Guid.NewGuid(),
                PositionName = "GĐ",
                Address = "HN",
                Email = "T@gmail.com"
            };

            _fakeEmployeeDL.InsertRecord(employee).Returns(1);
            _fakeEmployeeDL.DuplicateCode(employee).Returns(1);

            var expectedResult = new ServiceResult
            {
                IsSuccess = true,
            };

            // Act
            var actualResult = _employeeBL.InsertRecord(employee);

            // Assert
            Assert.That(expectedResult.IsSuccess, Is.EqualTo(actualResult.IsSuccess));
        }

        /// <summary>
        /// Test insert trùng mã
        /// Created By: NVTAN (09/02/2023)
        /// </summary>
        [Test]
        public void InsertRecord_DuplicateCode_ReturnInValid()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeCode = "NV-98920",
                FullName = "Nguyễn Văn Tân",
                DepartmentId = Guid.NewGuid(),
                PositionId = Guid.NewGuid(),
                DateOfBirth = DateTime.Now,
                IdentityNumber = "0045533",
                IdentityDate = DateTime.Now,
                IdentityPlace = "HN",
                Address = "VN",
                PhoneNumber = "0376747564",
                TelephoneNumber = "02477884",
                Email = "abc@gmail.com",
                BankAccount = "1232",
                BankName = "ACB",
                BankBranchName = "HN",
                Gender = Common.Enums.Gender.Female,
                CreatedDate = DateTime.Now,
                CreatedBy = "NVTAN",
                ModifiedBy = "NVTAN",
                ModifiedDate = DateTime.Now
            };

            _fakeEmployeeDL.InsertRecord(employee).Returns(1);
            _fakeEmployeeDL.DuplicateCode(employee).Returns(1);

            var expectedResult = new ServiceResult
            {
                IsSuccess = false,
                ErrorCode = Common.Enums.ErrorCode.DuplicateCode,
                Data = Resource.ErrorDuplicate
            };

            // Act
            var actualResult = _employeeBL.InsertRecord(employee);

            // Asert
            Assert.That(actualResult.IsSuccess, Is.EqualTo(expectedResult.IsSuccess));
            Assert.That(actualResult.ErrorCode, Is.EqualTo(expectedResult.ErrorCode));
            Assert.That(actualResult.Data, Is.EqualTo(expectedResult.Data));
        }


    }
}
