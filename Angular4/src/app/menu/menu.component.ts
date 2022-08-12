import { Component, OnInit } from '@angular/core';
import { AutenticaServico } from '../servicos/autentica.service';

@Component({
    moduleId: module.id,
    selector: 'menu',
    templateUrl: 'views/menu.component.html'
})

export class MenuComponent implements OnInit{
    
  mostrarMenu: boolean = false;
  nomeUsuario: string = "Bem-vindo usuário";

  constructor(private authService: AutenticaServico){
    this.mostrarMenu = this.authService.usuarioEstaAutenticado();
    this.nomeUsuario = "Olá, " + this.authService.nomeUsuarioAutenticado();
  }
 
  ngOnInit(){
    this.mostrarMenu = this.authService.usuarioEstaAutenticado();
  }

}
