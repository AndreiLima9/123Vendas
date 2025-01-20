using AndreiLima._123Vendas.Domain.Entities;
using AndreiLima._123Vendas.Domain.Interfaces.Services;
using AndreiLima._123Vendas.Models.Requests;
using AndreiLima._123Vendas.Models.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AndreiLima._123Vendas.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/purchase")]
    public class PurchaseController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService, IMapper mapper)
        {
            _mapper = mapper;
            _purchaseService = purchaseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseAsync([FromBody] PurchaseRequest model)
        {
            var purchase = _mapper.Map<Purchase>(model);
            var purchaseId = await _purchaseService.CreateAsync(purchase);
            return Ok(new { id = purchaseId });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePurchaseAsync([FromRoute] Guid id, [FromBody] PurchaseRequest model)
        {
            var purchase = _mapper.Map<Purchase>(model);
            await _purchaseService.UpdateAsync(id, purchase);
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPurchase([FromRoute] Guid id)
        {
            var purchase = await _purchaseService.GetAsync(id);
            if (purchase == null)
                return Ok();

            return Ok(_mapper.Map<PurchaseDetailsResponse>(purchase));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchases()
        {
            var purchases = await _purchaseService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PurchaseResponse>>(purchases));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePurchase(Guid id)
        {
            await _purchaseService.DeleteAsync(id);
            return Ok();
        }
    }
}
