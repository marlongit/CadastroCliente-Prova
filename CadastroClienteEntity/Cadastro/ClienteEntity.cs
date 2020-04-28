namespace CadastroClienteEntity.Cadastro
{
	public class ClienteEntity
	{
		public virtual int Id { get; protected set; }
		public virtual string Nome { get; set; }
		public virtual PorteEnum Porte { get; set; }
	}

	public enum PorteEnum
	{
		/// <summary>
		/// Grande
		/// </summary>		
		Grande = 1,
		/// <summary>
		/// Médio
		/// </summary>
		Média,
		/// <summary>
		/// Pequeno
		/// </summary>
		Pequena
	}
}