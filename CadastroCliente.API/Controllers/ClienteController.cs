using CadastroClienteEntity.Cadastro;
using CadastroClienteEntity.Models;
using CadastroClienteServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace CadastroCliente.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClienteController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			var clientes = ClienteService.Instance.ListarTodos();

			return Ok(clientes);
		}

		[HttpGet, Route("{id}")]
		public IActionResult Get(int id)
		{
			var clientes = ClienteService.Instance.RetornarClientePorId(id);

			return Ok(clientes);
		}

		[HttpPost]
		public IActionResult Post(ClienteModel entity)
		{
			var clientes = ClienteService.Instance.Inserir(entity);

			return Ok(clientes);
		}

		[HttpPut]
		public IActionResult Put(ClienteModel entity)
		{
			ClienteService.Instance.Editar(entity, entity.Id);

			return Ok(entity);
		}

		[HttpDelete, Route("{id}")]
		public IActionResult Delete(int id)
		{
			ClienteModel entity = new ClienteModel { Id = id };

			ClienteService.Instance.Deletar(entity);

			return Ok(entity);
		}
	}
}