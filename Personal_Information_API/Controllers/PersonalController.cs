using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal_Information_API.Models;
using Personal_Information_API.Models.Value_Object;
using Personal_Information_API.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Personal_Information_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly IPersonalService _personalService;
        public PersonalController(IPersonalService personalService)
        {
            _personalService = personalService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "GET All Personal")]
        public IEnumerable<Personal_Information> Get()
        {
            return _personalService.GetPersonals();
        }
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "GET Personal By ID")]
        public IActionResult GetById(int id)
        {
            var personalList = _personalService.GetPersonals().ToList();
            var selectedPersonal = personalList.FirstOrDefault(x=>x.Id == id);
            if(selectedPersonal != null)
            {
                return Ok(selectedPersonal);
            }
            return NotFound();

        }
        [HttpPost]
        [SwaggerOperation(Summary ="Add Personal")]
        public async Task<IActionResult> AddPersonal([FromBody] Personal_Information newPersonal)
        {
            var personalList = _personalService.GetPersonals().ToList();
            if(personalList.FirstOrDefault(x=>x.Id == newPersonal.Id) == null)
            {
                personalList.Add(newPersonal);
               await _personalService.CreateNewPersonalList(personalList);
                return StatusCode(201);
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Personel By ID")]
        public async Task<IActionResult> UpdatePersonal(int id, [FromForm] Personal updatePersonal)
        {
            var personelList = _personalService.GetPersonals().ToList();
            var personel = personelList.FirstOrDefault(x=>x.Id == id);
            if(personel != null)
            {
                personel.FirstName = updatePersonal.FirstName != default ? updatePersonal.FirstName : personel.FirstName;
                personel.LastName = updatePersonal.LastName != default ? updatePersonal.LastName : personel.LastName;
                personel.Email = updatePersonal.Email != default ? updatePersonal.Email : personel.Email;
                personel.Gender = updatePersonal.Gender != default ? updatePersonal.Gender : personel.Gender;
                personel.PhoneNumber = updatePersonal.PhoneNumber != default ? updatePersonal.PhoneNumber : personel.PhoneNumber;
                personel.Adress = updatePersonal.Adress != default ? updatePersonal.Adress : personel.Adress;
                personel.Country = updatePersonal.Country != default ? updatePersonal.Country : personel.Country;
                personelList.Where(x => x.Id == id).ToList().Add(personel);
                await _personalService.CreateNewPersonalList(personelList);
                return Ok();
            }
            return BadRequest();

        }

        [HttpPatch("{id}")]
        [SwaggerOperation(Summary =" Update Personel Email By ID")]
        public async Task<IActionResult> UpdatePersonalEmail(int id,[FromForm]PersonalEmail newEmail)
        {
            var personalList = _personalService.GetPersonals().ToList();
            var selectedPersonal = personalList.FirstOrDefault(x => x.Id == id);
            if (selectedPersonal != null)
            {
                selectedPersonal.Email = newEmail.Email != default ? newEmail.Email : selectedPersonal.Email;
                await _personalService.CreateNewPersonalList(personalList);
                return Ok(selectedPersonal);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary ="Delete Personal By ID")]
        public async Task<IActionResult> DeletePersonal(int id)
        {
            var personalList = _personalService.GetPersonals().ToList();
            var selectedPersonal = personalList.FirstOrDefault(x => x.Id == id);
            if(selectedPersonal != null)
            {
                personalList.RemoveAll(x => x.Id == id);
                await _personalService.CreateNewPersonalList(personalList);
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("{colon}/{order}")]
        [SwaggerOperation(Summary ="Order Personal",Description ="Order Personal with FirstName/LastName colon and ASC/DESC order type")]
        public IActionResult OrderPersonal(string colon="FirstName",string order = "ASC")
        {
            var personalList = _personalService.GetPersonals();
            var orderedList = new List<Personal_Information>();
            if(colon == "FirstName")
            {
                if(order.ToUpper() == "ASC")
                {
                    orderedList = personalList.OrderBy(x => x.FirstName).ToList();
                    
                }
                else if(order.ToUpper() == "DESC")
                {
                   orderedList = personalList.OrderByDescending(x => x.FirstName).ToList();
                   
                }
                else
                {
                    return BadRequest();
                }
            }
            else if(colon == "LastName")
            {
                if (order.ToUpper() == "ASC")
                {
                    orderedList = personalList.OrderBy(x => x.LastName).ToList();
                    
                }
                else if (order.ToUpper() == "DESC")
                {
                    orderedList = personalList.OrderByDescending(x => x.LastName).ToList();
                    
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
            return Ok(orderedList);
        }

    }
}
