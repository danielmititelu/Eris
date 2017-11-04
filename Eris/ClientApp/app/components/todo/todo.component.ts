import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';

import { TodoService } from '../../services/todo.service'

@Component({
    selector: 'todo',
    templateUrl: './todo.component.html'
})
export class TodoComponent implements OnInit {

    public rows: TodoItem[];

    constructor(private todoService: TodoService){ }

    ngOnInit(): void {
        this.todoService.getHeroes().subscribe(
            result => this.rows = result,
            error => console.error(error)
        );
    }
}
