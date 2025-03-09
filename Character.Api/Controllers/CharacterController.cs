using BusinessLogic.Models;
using CharacterApi.BusinessLogic;
using CharacterApi.BusinessLogic.Models;
using CharacterApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharacterApi.Controllers
{
    /// <summary>
    /// Controller for character manipulation
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        #region Private fields
        private readonly ICharacterBusinessLogic _characterBusinessLogic;
        #endregion

        #region Public constructor
        /// <summary>
        /// CharacterController constructor
        /// </summary>
        /// <param name="characterBusinessLogic"> Instance of CharacterBusinessLogic</param>
        public CharacterController(ICharacterBusinessLogic characterBusinessLogic)
        {
            _characterBusinessLogic = characterBusinessLogic;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Action that returns all characters
        /// </summary>
        /// <returns>Command response with list of characters in data property</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "GameMaster")]
        public ActionResult<CommandResponse<List<CharacterGet>>> GetCharacter()
        {
            var response = _characterBusinessLogic.GetCharacters();
            return Ok(response);
        }

        /// <summary>
        /// Action that returns character based on id
        /// </summary>
        /// <param name="id">Character id</param>
        /// <returns>Command response with character in data property</returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "GameMasterOrCharacterOwner")]
        public ActionResult<CharacterGetById> GetCharacter(int id)
        {
            var response = _characterBusinessLogic.GetCharacterById(id);
            return Ok(response);
        }

        // POST: api/Character
        /// <summary>
        /// Action for adding new character
        /// </summary>
        /// <param name="character">Character to insert</param>
        /// <returns>Command response with inserted character in data property</returns>
        [HttpPost]
        public ActionResult<Character> PostCharacter(CharacterPost character)
        {
            var response = _characterBusinessLogic.CreateCharacter(character);
            return Ok(response);
        }
        #endregion
    }
}