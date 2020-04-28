using CadastroClienteEntity.Cadastro;
using FluentNHibernate.Mapping;

namespace CadastroClienteServices.Mapping
{
	public class ClienteMap: ClassMap<ClienteEntity>
	{
		public ClienteMap()
		{
			Schema("dbo");
			Table("Clientes");
			Id(x => x.Id,"Id").Not.Nullable();
			Map(x => x.Nome, "Nome").Length(255).Not.Nullable();
			Map(x => x.Porte).CustomType<PorteEnum>().Column("Porte");
		}
	}
}
