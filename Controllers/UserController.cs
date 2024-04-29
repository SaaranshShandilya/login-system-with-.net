using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Loginsystem.Dto;
using Loginsystem.Interfaces;
using Loginsystem.Models;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Loginsystem.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        public readonly IUserInterface _userInterface;
        public readonly IMapper _mapper;

        public UserController(IUserInterface userInterface, IMapper mapper)
        {
            _mapper = mapper;
            _userInterface = userInterface;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [Route("/")]
        public IActionResult Ping(){
            return Ok("Pong");
        }
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateUser([FromBody] UserCreationDto usercreate){
            if(usercreate == null){
                return BadRequest(ModelState);
            }
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            byte[] messageBytes = Encoding.UTF8.GetBytes(usercreate.Password);
            byte[] hashValue = SHA256.HashData(messageBytes);
            var pw = Convert.ToHexString(hashValue);
            usercreate.Password = pw;
            var res = _mapper.Map<User>(usercreate);
            res.Id = Guid.NewGuid();
            if(!_userInterface.CreateUser(res)){
                ModelState.AddModelError("","Somthing went wrong");
                return BadRequest(ModelState);
            }
            return Ok("");
        }

        [HttpPost("/login")]
        [ProducesResponseType(200)]
        public IActionResult LoginUser([FromQuery]string email, [FromQuery]string password){
            if(email == null){
                return BadRequest(ModelState);
            }
            if(password == null){
                return BadRequest(ModelState);
            }
            if(!_userInterface.FindUserByEmail(email)){
                return NotFound();
            }
            if(!_userInterface.LoginUser(email, password)){
                ModelState.AddModelError("","Wrong email id or password");
                return BadRequest(ModelState);
            }
            return Ok("user logged in");
        }

    }
}