using CadastroClienteEntity.Cadastro;

namespace CadastroClienteEntity.Models
{
	public class ClienteModel
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Porte { get; set; }

		public PorteEnum PorteMap { get; set; }

		public string PorteDescricao
		{
			get
			{
				if (Porte == "1" || PorteMap == PorteEnum.Grande)
				{
					return "Grande";
				}
				else if (Porte == "2" || PorteMap == PorteEnum.Média)
				{
					return "Média";
				}
				else if (Porte == "3" || PorteMap == PorteEnum.Pequena)
				{
					return "Pequena";
				}
				else
					return "";
			}
		}

		public static ClienteEntity GetClienteEntity(ClienteModel model)
		{
			return new ClienteEntity
			{
				Nome = model.Nome,
				Porte = model.Porte.Equals("1") ? PorteEnum.Grande : model.Porte.Equals("2") ? PorteEnum.Média : PorteEnum.Pequena
			};
		}
	}
}
