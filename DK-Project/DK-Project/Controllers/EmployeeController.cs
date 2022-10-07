using BookStore.BL.Interfaces;
using DK_Project.Models.Mediatr.Commands.AuthorCommands;
using DK_Project.Models.Mediatr.Commands.BookCommands;
using DK_Project.Models.Models;
using DK_Project.Models.Models.Users;
using DK_Project.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DK_Project.Controllers
{
    [Authorize(AuthenticationSchemes ="Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService, IUserInfoService userInfoService)
        {
            _employeeService = employeeService;
            _userInfoService = userInfoService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetNamesAndId")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employeeService.GetEmployeeDetails());
        }

        [HttpGet("GetByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByID(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest($"Parameter id:{Id} must be greater than 0 or 0");
            }
            var result = await _employeeService.GetEmployeeDetails(Id);
            if (result == null)
            {
                return NotFound("Book Doesn't Exists");
            }
            return Ok(await _employeeService.GetEmployeeDetails(Id));
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] Employee employee)
        {
            await _employeeService.AddEmployee(employee);
            return Ok();
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] Employee authorRequest)
        {
            if (authorRequest == null) return BadRequest(authorRequest);
            var authorExist = await _employeeService.GetEmployeeDetails(authorRequest.EmployeeID);
            if (authorExist == null) return BadRequest("Author Dosen't Exist");
            await _employeeService.UpdateEmployee(authorRequest);
            return Ok();
        }
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var authorExist = await _employeeService.GetEmployeeDetails(id);
            if (authorExist == null) return BadRequest("Author Doesn't Exists");
            await _employeeService.DeleteEmployee(id);
            return Ok();
        }
    }
}
