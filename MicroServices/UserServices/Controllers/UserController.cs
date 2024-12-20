﻿using AutoMapper;
using UserServices.Data;
using UserServices.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            System.Console.WriteLine("Getting users");

            var users = _repo.GetAllUsers();

            return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
        }
       
        [HttpGet("{id}", Name ="GetUserById")]
        public ActionResult<UserDTO> GetUserById(int id){
            var cat = _repo.GetUserById(id);
            if (cat != null){
                return Ok(_mapper.Map<UserDTO>(cat));
            }
            return NotFound();
        }

        [HttpGet("email/{email}")]
        public ActionResult<UserDTO> GetUserByEmail(string email)
        {
            var user = _repo.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPost]
        public ActionResult<UserDTO> AddUser(UserDTO user){
            var catModel = _mapper.Map<User>(user);

            _repo.CreateUser(catModel);

            var catDto = _mapper.Map<UserDTO>(catModel);

            return CreatedAtRoute(nameof(GetUserById), new {id = catDto.UserId}, catDto);
        }
        [HttpDelete("{id}")]
        public ActionResult<UserDTO> DeleteUser(int id){
            try{
                _repo.DeleteUser(id);
                return Ok(new {message = "User delete successfully"});

            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new {message = "User not found"});
            }
            catch (Exception e){
                return StatusCode(500, new {message = "An error while deleting User"});
            }

        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, UserDTO userUpdateDto)
        {
            var existingUser = _repo.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound(new { message = "User not found" });
            }
            _mapper.Map(userUpdateDto, existingUser);

            try
            {
                _repo.UpdateUser(existingUser);
                return Ok(new { message = "User updated successfully" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpPost("login")]
        public ActionResult<UserDTO> Login(UserLoginDTO userLoginDto)
        {
            var user = _repo.Login(userLoginDto);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPost("register")]
        public ActionResult<UserDTO> Register(UserDTO user)
        {
            // Kiểm tra nếu người dùng đã tồn tại
            var existingUser = _repo.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "User already exists" });
            }

            // Chuyển đổi từ DTO sang model
            var userModel = _mapper.Map<User>(user);

            // Tạo người dùng mới
            _repo.CreateUser(userModel);

            // Lưu thay đổi vào cơ sở dữ liệu
            if (!_repo.SaveChanges())
            {
                return StatusCode(500, new { message = "An error occurred while registering the user" });
            }

            // Chuyển đổi lại thành DTO để trả về
            var userDto = _mapper.Map<UserDTO>(userModel);

            // Trả về kết quả với HTTP 201 Created và dữ liệu người dùng mới
            return CreatedAtRoute(nameof(GetUserById), new { id = userDto.UserId }, userDto);
        }

    }
}
