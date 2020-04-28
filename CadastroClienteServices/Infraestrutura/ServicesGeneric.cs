using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CadastroClienteServices.Infraestrutura
{
	public class ServicesGeneric
	{
		private ServicesGeneric()
		{
		}

		private static ServicesGeneric _instance;

		public static ServicesGeneric Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (typeof(ServicesGeneric))
					{
						_instance = new ServicesGeneric();
					}
				}

				return _instance;
			}
		}

		public void Create<T>(T entity) where T : class
		{
			using (var sessionFactory = ConnectionSession.Instance.CreateSessionFactory())
			{
				using (var session = sessionFactory.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{
						session.Save(entity);
						transaction.Commit();
					}

				}
			}
		}
		public void Update<T>(T entity) where T : class
		{
			using (var sessionFactory = ConnectionSession.Instance.CreateSessionFactory())
			{
				using (var session = sessionFactory.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{
						session.Update(entity);
						transaction.Commit();
					}
				}
			}
		}
		public void Delete<T>(T entity) where T : class
		{
			using (var sessionFactory = ConnectionSession.Instance.CreateSessionFactory())
			{
				using (var session = sessionFactory.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{
						session.Delete(entity);
						transaction.Commit();
					}
				}
			}
		}
		public IList<T> GetAll<T>() where T : class
		{
			using (var sessionFactory = ConnectionSession.Instance.CreateSessionFactory())
			{
				using (var session = sessionFactory.OpenSession())
				{
					return session.CreateCriteria(typeof(T)).List<T>();
				}
			}
		}
		public T Get<T>(Func<T, bool> predicate) where T : class
		{
			using (var sessionFactory = ConnectionSession.Instance.CreateSessionFactory())
			{
				using (var session = sessionFactory.OpenSession())
				{
					return session.CreateCriteria(typeof(T)).List<T>().FirstOrDefault(predicate);
				}
			}
		}
		public IList<T> GetAllForQuery<T>(string query) where T : class
		{
			using (var sessionFactory = ConnectionSession.Instance.CreateSessionFactory())
			{
				using (var session = sessionFactory.OpenSession())
				{
					return session.CreateSQLQuery(query).List<T>();
				}
			}
		}
		public SqlDataReader ExecuteQuery(string queryString, Dictionary<string, object> parameters)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionSession.connectionString))
			{
				SqlCommand command = new SqlCommand(queryString, connection);
				connection.Open();
				foreach (var param in parameters)
				{
					command.Parameters.AddWithValue(param.Key, param.Value);
				}

				return command.ExecuteReader();
			}
		}
		public void ExecuteQuery(string queryString, TypeCommandSql typeCommandSql, Dictionary<string, object> parameters)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionSession.connectionString))
			{
				SqlCommand command = new SqlCommand(queryString, connection);
				connection.Open();

				foreach (var param in parameters)
				{
					command.Parameters.AddWithValue(param.Key, param.Value);
				}

				if (typeCommandSql != TypeCommandSql.Select)
				{
					command.ExecuteNonQuery();
				}
				command.Dispose();

			}
		}
	}

	public enum TypeCommandSql
	{
		Insert = 1,
		Update,
		Delete,
		Select
	}
}

