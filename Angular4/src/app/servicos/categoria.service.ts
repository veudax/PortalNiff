import { Router } from '@angular/router';
import { Injectable, EventEmitter, Output, Input } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { app_url } from '../app.api';
import 'rxjs/Rx';
import 'rxjs/add/operator/toPromise'
import { Categoria } from '../classes/categoria';

@Injectable()
export class CategoriaServico {
    private url : string;
    promise: Promise<Categoria[]>;
    promiseItem: Promise<Categoria>;

    idCategoria : Number;
    categoria : Categoria;

    listaCategoria: Categoria[];
    private retorno: Categoria = new Categoria();
    
    constructor( private router: Router, private _http: Http){
    }

    getCategoria(categ:Categoria): Promise<Categoria[]>{
      this.url = `${app_url}/categorias/${categ.IDCATEGORIA}`;
      return this._http.get(this.url)
        .toPromise()
        .then(res => res.json())
        .catch();
    }

    getTodasCategoria(): Promise<Categoria[]>{
      this.url = `${app_url}/categorias`;
      return this._http.get(this.url)
        .toPromise()
        .then(res => res.json())
        .catch();
    }

    getProximo(): Promise<Categoria[]>{
        this.url = `${app_url}/categorias/proximo`;

        return this._http.get(this.url)
          .toPromise()
          .then(res => res.json())
          .catch();
      }
      
    insert(categ: Categoria): Promise<Categoria>{
      let header = new Headers({'Content-Type' : 'application/json'});
      let option = new RequestOptions({headers : header});

      let json = JSON.stringify({idCategoria: categ.IDCATEGORIA
                                , Descricao : categ.DESCRICAO
                                , Ativo : categ.ATIVO
                                , PossuiModulos: categ.POSSUIMODULOS});
      this.url = `${app_url}/categorias`;

      return this._http.post(this.url, json, option)
      .toPromise()
      .then(res => res.json())
      .catch();
    }

    update(categ: Categoria): Promise<Categoria>{
      let header = new Headers({'Content-Type' : 'application/json'});
      let option = new RequestOptions({headers : header});

      let json = JSON.stringify({idCategoria: categ.IDCATEGORIA
                                , Descricao : categ.DESCRICAO
                                , Ativo : categ.ATIVO
                                , PossuiModulos: categ.POSSUIMODULOS});
      this.url = `${app_url}/categorias`;

      return this._http.put(this.url, json, option)
      .toPromise()
      .then(res => res.json())
      .catch();
    }

    
    delete(categ: Categoria): Promise<Categoria>{
      let header = new Headers({'Content-Type' : 'application/json'});

      this.url = `${app_url}/categorias/${categ.IDCATEGORIA}`;

      return this._http.delete(this.url)
      .toPromise()
      .then(res => res.json())
      .catch();
    }

    validaCategoria(categ: Categoria)
    {
      // funcionando
      this.promise = this.getCategoria(categ);
      this.promise.then((response: Categoria[]) => {
        this.listaCategoria = response;
        this.retorno = this.listaCategoria[0];
       
        console.log( this.retorno );

        if (!this.retorno === undefined)
              alert('Categoria não cadastrada.');
            else
            {
                if (this.retorno.ATIVO === 'N')
                    alert('Categoria inativa.');
            }
      })
    }
    
    
    proximo(): any
    {
      // funcionando
      this.promise = this.getProximo();
      this.promise.then((response: Categoria[]) => {
        //this.listaCategoria = response;
        this.categoria = response[0];
        //console.log(this.categoria);
        
      })
    }

    listar(): any{
      this.promise = this.getTodasCategoria();
      this.promise.then((response: Categoria[]) => {
      this.listaCategoria = response;   
      console.log("Categoria serviço") ; 
      console.log(this.listaCategoria) ;
    })
    }

    gravar(item: Categoria){
      if (item.EXISTE === "N")
      {
          this.promiseItem = this.insert(item);
          this.promise.then((response: Categoria[]) => {
              this.categoria = response[0];
              alert("Categoria Incluída com sucesso");
          })
      }
      else
      {
        this.promiseItem = this.update(item);
          this.promise
             .then((response: Categoria[]) => {
              this.categoria = response[0];
              alert("Categoria Alterada com sucesso");
              })
      }
      console.log("gravou");
    }

    excluir(item: Categoria){
      this.promiseItem = this.delete(item);
          this.promise
             .then((response: Categoria[]) => {
              this.categoria = response[0];
              alert("Categoria Excluída com sucesso");
              })
      console.log("excluir");
    }

}
