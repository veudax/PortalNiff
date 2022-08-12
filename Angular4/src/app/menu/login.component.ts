import { Component, OnInit } from '@angular/core';
import { AutenticaServico } from '../servicos/autentica.service';
import { Usuario } from '../classes/usuario';

@Component({
    moduleId: module.id,
    //selector: 'login',
    templateUrl: 'views/login.component.html'
})

export class LoginComponent implements OnInit{

    private usuario: Usuario = new Usuario();
    private retorno: Usuario = new Usuario();
    private listaUsuarios: Usuario[];
    private usuarioOk : boolean = false;
    mostrarMenu: boolean = false;

    constructor(private authService: AutenticaServico){
        this.mostrarMenu = this.usuarioOk;
    }
    
    ngOnInit(){
       
    }

    fazerLogin(){
        this.authService.fazerLogin(this.usuario);
    }
    
}
