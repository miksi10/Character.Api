using BusinessLogic.Models;
using CharacterApi.BusinessLogic;
using CharacterApi.BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharacterApi.Controllers
{
    /// <summary>
    /// Controller for item manipulation
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        #region Private fields
        private readonly IItemBusinessLogic _itemBusinessLogic;
        #endregion

        #region Public constructor 
        /// <summary>
        /// ItemsController constructor
        /// </summary>
        /// <param name="itemBusinessLogic">Instance of ItemBusinessLogic</param>
        public ItemsController(IItemBusinessLogic itemBusinessLogic)
        {
            _itemBusinessLogic = itemBusinessLogic;
        }
        #endregion

        /// <summary>
        /// Action that returns all items
        /// </summary>
        /// <returns>Command response with all items in data property</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "GameMaster")]
        public ActionResult<CommandResponse<List<ItemGet>>> GetItems()
        {
            var response = _itemBusinessLogic.GetItems();
            return Ok(response);
        }

        /// <summary>
        /// Action that returns item based on id
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Command response with item in data property</returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult<CommandResponse<ItemGetById>> GetItem(int id)
        {
            var response = _itemBusinessLogic.GetItemById(id);
            return Ok(response);
        }

        /// <summary>
        /// Action for adding new item
        /// </summary>
        /// <param name="item">Item to insert</param>
        /// <returns>Command response with inserted item in data property</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult<CommandResponse<ItemPost>> PostItem(ItemPost item)
        {
            var response = _itemBusinessLogic.CreateItem(item);
            return Ok(response);
        }

        /// <summary>
        /// Action that adds item to specific character
        /// </summary>
        /// <param name="item">Item fod adding to specific character</param>
        /// <returns>Command response with added item to character</returns>
        [HttpPost(nameof(Grant))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult<CommandResponse<GrantItem>> Grant(GrantItem item)
        {
            var response = _itemBusinessLogic.AddItemToCharacter(item);
            return Ok(response);
        }

        /// <summary>
        /// Action for gifting item fron one character to another
        /// </summary>
        /// <param name="item">Item to gift from one character to another</param>
        /// <returns>Command response with gifted item</returns>
        [HttpPost(nameof(Gift))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult<CommandResponse<GrantItem>> Gift(GiftItemRequest item)
        {
            var response = _itemBusinessLogic.GiftItem(item);
            return Ok(response);
        }
    }
}
