import { Component, OnInit } from '@angular/core';
import { Categoria } from '../../classes/categoria';
import { CategoriaServico } from '../../servicos/categoria.service';
import { Router } from '@angular/router';
import { Criptografia } from '../../servicos/criptografia';

@Component({
    moduleId: module.id,
    templateUrl: 'views/listaCategoria.component.html'
})

export class ListaCategoriaComponent implements OnInit{
    

    listaComCategorias: Categoria[];

    constructor(private router: Router, private categoriaService: CategoriaServico, private cripto : Criptografia){
        this.categoriaService.listar();
        this.listaComCategorias = this.categoriaService.listaCategoria;
    }
    
    ngOnInit(){}

    
  listar(){
    this.categoriaService.listar();
    this.listaComCategorias = this.categoriaService.listaCategoria;
  }
  
  excluir(item: Categoria){
    this.categoriaService.excluir(item);
    
    const index: number = this.listaComCategorias.indexOf(item);
    if (index !== -1)
      this.listaComCategorias.splice(index,1);
  }

  selecionar(item: Categoria){
    //original
    //this.router.navigate(['/categoria'], {queryParams: {'categoria': JSON.stringify(item)}});
    this.router.navigate(['/categoria'], {queryParams: {'categoria': this.cripto.criptografar(JSON.stringify(item))}});
  }

}
