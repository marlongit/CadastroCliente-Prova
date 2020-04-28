using CadastroClienteServices.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace CadastroClienteServices.Infraestrutura
{
	public class ConnectionSession
	{
		private ConnectionSession()
		{
		}

		public const string connectionString = "Server=server;Database=clientesBD;User ID=user; Password=***";

		private static ConnectionSession _instance;

		public static ConnectionSession Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (typeof(ConnectionSession))
					{
						_instance = new ConnectionSession();
					}
				}

				return _instance;
			}
		}

		
		public ISessionFactory CreateSessionFactory()
		{
			return Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
				.Mappings(m =>
	  m.FluentMappings.AddFromAssemblyOf<ClienteMap>()).BuildSessionFactory();
		}
	}
}
