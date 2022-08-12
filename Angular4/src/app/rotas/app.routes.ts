import { Routes } from '@angular/router';

import { MenuComponent } from '../menu/menu.component';
import { LoginComponent } from '../menu/login.component';
import { CategoriaComponent } from '../cadastros/sistemas/categoria.component';
import { NotFoundComponent } from '../erro/notFound.component';
import { ListaCategoriaComponent } from '../Consultas/sistemas/listaCategoria.component';

export const appRotas : Routes = [
    {path: '', component: MenuComponent},
    {path: 'login', component: LoginComponent},
    {path: 'categoria', component: CategoriaComponent},
    {path: 'listaCategoria', component: ListaCategoriaComponent},

    {path: '**', component: NotFoundComponent}// sempre o ultimo
];