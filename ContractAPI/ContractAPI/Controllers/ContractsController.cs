using ContractAPI.Data;
using ContractAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : Controller
    {
        private readonly ContractsAPIDbContext dbContext;

        public ContractsController(ContractsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContracts()
        {
            return Ok(await dbContext.Contracts.ToListAsync()); 
          
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContract([FromRoute] Guid id)
        {
            var contract = await dbContext.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            return Ok(contract);
        }


        [HttpPost]
        public async Task<IActionResult> AddContracts(AddContractRequest addContractRequest)
        {
            var contracts = new Contract()
            {
                ID = Guid.NewGuid(),  
                Address = addContractRequest.Address,
                Email = addContractRequest.Email,
                Fullname = addContractRequest.Fullname,
                Phone = addContractRequest.Phone
            };

            await dbContext.Contracts.AddAsync(contracts);
            await dbContext.SaveChangesAsync();

            return Ok(contracts);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContracts([FromRoute] Guid id, UpdateContractsRequests updateContractsRequests)
        {
           var contract =  await dbContext.Contracts.FindAsync(id);
            if ( contract != null)
            {
                contract.Fullname = updateContractsRequests.Fullname;
                contract.Address = updateContractsRequests.Address;
                contract.Phone = updateContractsRequests.Phone;
                contract.Email = updateContractsRequests.Email;

                await dbContext.SaveChangesAsync();

                return Ok(contract);
            }

            return NotFound();
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContract([FromRoute] Guid id)
        {
            var contract = await dbContext.Contracts.FindAsync(id);
            if(contract != null)
            {
                dbContext.Remove(contract);
                await dbContext.SaveChangesAsync();
                return Ok(contract);
            }
            return NotFound(nameof(contract));
        }

    }
}
