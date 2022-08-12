import { Component } from '@angular/core';
import { AutenticaServico } from './servicos/autentica.service';

@Component({
  selector: 'my-app',
  template: '<menu></menu>',
})
export class AppComponent  { 
  name = 'Angular'; 

  mostrarMenu: boolean = false;

  constructor(private authService: AutenticaServico){

  }
 
  ngOnInit(){
    this.mostrarMenu = this.authService.usuarioEstaAutenticado();
  }
}
