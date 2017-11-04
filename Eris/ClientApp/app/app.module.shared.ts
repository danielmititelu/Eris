import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { TodoComponent } from './components/todo/todo.component';
import { LoadingComponent } from './components/loading/loading.component';
import { TodoService } from './services/todo.service'

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        TodoComponent,
        HomeComponent,
        LoadingComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'to-do', component: TodoComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        TodoService
    ]
})
export class AppModuleShared {
}
