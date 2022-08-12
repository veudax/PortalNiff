import { Component, OnInit } from '@angular/core';
import { Categoria } from '../../classes/categoria';
import { CategoriaServico } from '../../servicos/categoria.service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { parse } from 'querystring';
import { Criptografia } from '../../servicos/criptografia';


@Component({
    moduleId: module.id,
    templateUrl: 'views/categoria.component.html'
})

export class CategoriaComponent implements OnInit{
    
  categoria: Categoria = new Categoria();
  idCategoria: Number = 0;
  listaCategorias: Categoria[];
  private qtdeclick = 1;
  categoriaOb: Categoria; //any; //Observable<string>;
  dcripTxt: any;

  constructor(private router: ActivatedRoute, private categoriaService: CategoriaServico, private cripto : Criptografia){
    //this.categoriaService.proximo();
  }
 
  ngOnInit(){
    const cat = this.router.snapshot.queryParams['categoria'];
    
    this.dcripTxt = this.cripto.descriptografar(cat);
    
    this.categoria = JSON.parse(this.dcripTxt);
    console.log(this.categoria);
  }
  
  proximo(){
    this.categoriaService.proximo();
    this.categoria = this.categoriaService.categoria;
    this.categoria.PossuiModulos = this.categoria.POSSUIMODULOS === "S";
    this.categoria.Ativo = this.categoria.ATIVO === "S";
    //console.log(this.categoria);
  }

  listar(){
    this.categoriaService.listar();

    this.qtdeclick = 0;

    if (this.listaCategorias != undefined)
      this.qtdeclick = this.listaCategorias.length;

    if (this.qtdeclick != this.categoriaService.listaCategoria.length)
      this.listaCategorias = this.categoriaService.listaCategoria;
      
      console.log("categoria componente");
      console.log(this.listaCategorias);
  }

  selecionar(item: Categoria) {
    this.categoria = item;
    this.categoria.Ativo = this.categoria.ATIVO === "S";
    this.categoria.PossuiModulos = this.categoria.POSSUIMODULOS === "S";
  }

  gravar(item: Categoria){
    if (item.Ativo)
      item.ATIVO = "S"
    else
      item.ATIVO = "N";

    if (item.PossuiModulos)
      item.POSSUIMODULOS = "S"
    else
      item.POSSUIMODULOS = "N";
    
      this.categoriaService.gravar(item);
      
      this.categoria = new Categoria();
      this.listaCategorias = null;

  }

  excluir(item: Categoria){
    this.categoriaService.excluir(item);
    this.categoria = new Categoria();
    this.listaCategorias = null;

    this.qtdeclick = 1;
  }
}
