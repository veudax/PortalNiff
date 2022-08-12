import { Router } from '@angular/router';
import { Injectable, EventEmitter, Output, Input } from '@angular/core';
import { Usuario } from '../classes/usuario';
import { Http } from '@angular/http';
import { app_url } from '../app.api';
import 'rxjs/Rx';
import 'rxjs/add/operator/toPromise'

@Injectable()
export class AutenticaServico {
    //headers: Headers;
    public mostrarMenuEmitter : boolean = false;
    private usuarioOk : boolean = false;
    private url : string;
    promise: Promise<Usuario[]>;
    
    private retorno: Usuario = new Usuario();
    private listaUsuarios: Usuario[];

    constructor( private router: Router, private _http: Http){
      //this.headers = new Headers();
      //this.headers.append('Content-Type', 'application/json');
    }
    /*
    private extractData(res: Response | any) {
      let body = res.json();
      return body;
    } 
    
    private handleErrorPromise (error: Response | any) {
      console.error(error.message || error);
      return Promise.reject(error.message || error);
    }*/

    getUsuarioLogin(usuario:Usuario): Promise<Usuario[]>{
      this.url = `${app_url}/usuarios/usuarioAcesso/${usuario.EMAIL}`;
      return this._http.get(this.url)
        .toPromise()
        .then(res => res.json())
        .catch();
    }
    
    fazerLogin(usuario: Usuario)
    {
      // funcionando
      this.promise = this.getUsuarioLogin(usuario);
      this.promise.then((response: Usuario[]) => {
        this.listaUsuarios = response;
        this.retorno = this.listaUsuarios[0];
       
        this.usuarioOk = this.retorno.SENHA === usuario.SENHA;

        console.log( this.retorno );
        //console.log(this.usuarioOk);

        if (!this.usuarioOk)
              alert('Senha inválida.');
            else
            {
                if (this.retorno.ATIVO === 'N')
                    alert('Usuário inativo.');
                else
                {
                  // armazena o conteudo até que seja fechado a aba do navegador
                    sessionStorage.setItem('Usuario', JSON.stringify(this.retorno));
                  // recupera o conteudo do comando acima 
                  //  sessionStorage.getItem('Usuario');
                    this.router.navigate(['/']);
                }
            }
      })
    }
    
    usuarioEstaAutenticado(){
      return this.usuarioOk;
    }

    nomeUsuarioAutenticado(){
      return this.retorno.NOME;
    }

}
