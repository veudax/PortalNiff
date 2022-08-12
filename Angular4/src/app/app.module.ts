import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule  } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { CommonModule } from '@angular/common';
import { AppComponent }  from './app.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

// criados
import { appRotas } from './rotas/app.routes'; // deve vir antes dos menus componentes pois é utilizado neles
import { MenuComponent } from './menu/menu.component'
import { LoginComponent } from './menu/login.component';
import { NotFoundComponent } from './erro/notFound.component';
import { CategoriaComponent } from './cadastros/sistemas/categoria.component';
import { ListaCategoriaComponent } from './Consultas/sistemas/listaCategoria.component';

// serviços
import { AutenticaServico } from './servicos/autentica.service';
import { CategoriaServico } from './servicos/categoria.service';
import { Criptografia } from './servicos/criptografia';

@NgModule({
  imports:      [ BrowserModule, 
                  FormsModule,  
                  HttpModule,
                  CommonModule,
                  BsDatepickerModule.forRoot(),
                  RouterModule.forRoot(appRotas) ],
  declarations: [ AppComponent, // <-- componente principal
                  MenuComponent, LoginComponent, NotFoundComponent,
                  CategoriaComponent, ListaCategoriaComponent ],
  providers: [AutenticaServico, CategoriaServico, Criptografia],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
