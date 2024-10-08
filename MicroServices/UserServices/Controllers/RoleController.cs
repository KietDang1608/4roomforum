using AutoMapper;
using UserServices.Data;
using UserServices.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepo _repo;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RoleDTO>> GetAllRoles()
        {
            System.Console.WriteLine("Getting roles");

            var roles = _repo.GetAllRoles();

            return Ok(_mapper.Map<IEnumerable<RoleDTO>>(roles));
        }
        [HttpGet("{id}", Name = "GetRoleById")]
        public ActionResult<RoleDTO> GetRoleById(int id)
        {
            var role = _repo.GetRoleById(id);
            if (role != null)
            {
                return Ok(_mapper.Map<RoleDTO>(role));
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult<RoleDTO> AddRole(RoleDTO role)
        {
            var roleModel = _mapper.Map<Role>(role);

            _repo.CreateRole(roleModel);

            var roleDto = _mapper.Map<RoleDTO>(roleModel);

            return CreatedAtRoute(nameof(GetRoleById), new { id = roleDto.RoleId }, roleDto);
        }
        [HttpDelete("{id}")]
        public ActionResult<RoleDTO> DeleteRole(int id)
        {
            try
            {
                _repo.DeleteRole(id);
                return Ok(new { message = "Role delete successfully" });

            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = "Role not found" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "An error while deleting role" });
            }

        }

    }
}
