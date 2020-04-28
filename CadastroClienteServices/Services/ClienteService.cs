using CadastroClienteEntity.Cadastro;
using CadastroClienteEntity.Models;
using CadastroClienteServices.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadastroClienteServices.Services
{
  public class ClienteService
  {
    private ClienteService()
    {
    }

    private static ClienteService _instance;

    public static ClienteService Instance
    {
      get
      {
        if (_instance == null)
        {
          lock (typeof(ClienteService))
          {
            _instance = new ClienteService();
          }
        }

        return _instance;
      }
    }

    public ClienteModel Inserir(ClienteModel entity)
    {
      try
      {
        if (string.IsNullOrEmpty(entity.Nome)) throw new ArgumentNullException("Nome");

        ClienteEntity entityNew = ClienteModel.GetClienteEntity(entity);

        ServicesGeneric.Instance.Create(entityNew);

        return entity;
      }
      catch (Exception ex)
      {
        throw new Exception("Não foi possível realizar a operação", ex);
      }
    }
    public void Editar(ClienteModel entity, int id)
    {
      try
      {
        if (string.IsNullOrEmpty(entity.Nome)) throw new ArgumentNullException("Nome");

        var entityBd = ServicesGeneric.Instance.Get<ClienteEntity>(x => x.Id == id);

        if (entityBd != null)
        {
          entityBd.Nome = entity.Nome;
          entityBd.Porte = entity.Porte.Equals("1") ? PorteEnum.Grande : entity.Porte.Equals("2") ? PorteEnum.Média : PorteEnum.Pequena;
          ServicesGeneric.Instance.Update(entityBd);
        }

      }
      catch (Exception ex)
      {
        throw new Exception("Não foi possível realizar a operação", ex);
      }
    }
    public void Deletar(ClienteModel entity)
    {
      try
      {
        var entityBd = ServicesGeneric.Instance.Get<ClienteEntity>(x => x.Id == entity.Id);

        ServicesGeneric.Instance.Delete(entityBd);
      }
      catch (Exception ex)
      {
        throw new Exception("Não foi possível realizar a operação", ex);
      }
    }
    public IList<ClienteModel> ListarTodos()
    {
      return ServicesGeneric.Instance.GetAll<ClienteEntity>().Select(x => new ClienteModel { Id = x.Id, Nome = x.Nome, PorteMap = x.Porte }).ToList();
    }
    public IList<ClienteModel> ListarPorQuery()
    {
      var sql = "Select * from clientes";
      return ServicesGeneric.Instance.GetAllForQuery<ClienteEntity>(sql).Select(x => new ClienteModel { Id = x.Id, Nome = x.Nome, PorteMap = x.Porte }).ToList();
    }
    public ClienteModel RetornarClientePorId(int id)
    {
      var sql = $"Select * from clientes where id= @id";
      var param = new Dictionary<string, object>();
      param.Add("id", id);
      var ret = ServicesGeneric.Instance.ExecuteQuery<ClienteEntity>(sql, param);

      var clienteModel = ret.Select(x => new ClienteModel { Id = x.Id, Nome = x.Nome, PorteMap = x.Porte }).FirstOrDefault();

      return clienteModel;
    }
  }
}
