using Microsoft.AspNetCore.Mvc;

namespace PetNameGenerator.Controllers
{
    public class AnimalTypeRequest
    {
        public string AnimalType { get; set; }
        public bool TwoPart { get; set; } = false;
    }

    [ApiController]
    [Route("[controller]")]
    public class PetNameController : ControllerBase
    {
        private static readonly Dictionary<string, string[]> petNames = new()
        {
            { "dog", new[] { "Buddy", "Max", "Charlie", "Rocky", "Rex" } },
            { "cat", new[] { "Whiskers", "Mittens", "Luna", "Simba", "Tiger" } },
            { "bird", new[] { "Tweety", "Sky", "Chirpy", "Raven", "Sunny" } }
        };

        [HttpPost("generate")]
        public IActionResult GeneratePetName([FromBody] AnimalTypeRequest request)
        {
            if (string.IsNullOrEmpty(request.AnimalType) || !petNames.ContainsKey(request.AnimalType.ToLower()))
                return BadRequest(new { error = "Invalid or missing 'animalType'. Allowed values: dog, cat, bird." });

            var names = petNames[request.AnimalType.ToLower()];
            var rnd = new Random();
            string name = names[rnd.Next(names.Length)] + (request.TwoPart ? names[rnd.Next(names.Length)] : "");

            return Ok(new { name });
        }
    }
}
