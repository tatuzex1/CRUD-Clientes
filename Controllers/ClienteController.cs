using crmall.Domain.Commands;
using crmall.Domain.Commands.BaseResponse;
using crmall.Domain.Handlers;
using crmall.Domain.Handlers.Contract;
using crmAll.Domain.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace crmall.Api.Controllers
{
    [ApiController]
    [Route("v1/clientes")]
    public class ClienteController : Controller
    {
        private readonly IClienteHandler _clienteHandler;

        public ClienteController(IClienteHandler handler)
        {
            this._clienteHandler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        [HttpPost]
        public async Task<BaseResponse> Create(ClienteCommand command){

            return await _clienteHandler.Handle(command);
        }

        [HttpPut("{clientId}")]
        public async Task<BaseResponse> Edit(EditClientCommand command, int clientId)
        {
            command.Id = clientId;
            return await _clienteHandler.Handle(command);
        }

        [HttpDelete("{clientId}")]
        public async Task<BaseResponse> Delete(int clientId)
        {
            return await _clienteHandler.Delete(clientId);
        }

        [HttpGet("{clientId}")]
        public async Task<BaseResponse> Get(int clientId)
        {
            return await _clienteHandler.Get(clientId);
        }

        [HttpGet]
        public async Task<BaseResponse> Get()
        {
            return await _clienteHandler.Get();
        }
    }
}
