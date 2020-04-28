import { Component, OnInit } from '@angular/core';
import { ClienteServiceService } from '../services/clienteService.service';
import { Cliente } from '../model/cliente';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {

  constructor(private clienteService: ClienteServiceService) { }

  cliente = new Cliente();
  clientes: Cliente[];

  ngOnInit(): void {
    this.listar();
  }

  excluir(cliente) {

    if (confirm("Deseja realmente ecluir este registro?")) {

      this.clienteService.delete(cliente)
        .subscribe(
          resultado => {
            this.listar();
            console.log(resultado);
          }, error => {
            console.log(error);
          });
    }
  }

  editar(cliente) {

    this.cliente = cliente;
  }

  inserir() { 
    if (this.cliente.id === 0 || this.cliente.id === undefined) {

      this.clienteService.post(this.cliente)
        .subscribe(
          resultado => {
            alert("Registro gravado com sucesso!");
            this.cliente = new Cliente();
            this.listar();
            console.log(resultado);
          }, error => {
            console.log(error);
          });
    }
    else {
      this.clienteService.put(this.cliente).subscribe(resultado => {
        alert("Registro atualizado com sucesso!");
        this.cliente = new Cliente();
        this.listar();
        console.log(resultado);
      }, error => {
        console.log(error);
      });

    }
  }

  listar() {
    this.clienteService.getAll().subscribe(
      resultado => {

        this.clientes = resultado;

      });
  }
}
