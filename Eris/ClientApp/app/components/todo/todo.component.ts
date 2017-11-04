import { Component, OnInit } from '@angular/core';

import { TodoService } from 'services/todo.service'

@Component({
    selector: 'todo',
    templateUrl: './todo.component.html'
})
export class TodoComponent implements OnInit {

    rows: TodoItem[];
    error: any;

    constructor(private todoService: TodoService){ }

    ngOnInit(): void {
        this.todoService.getTodo().subscribe(
            result => this.rows = result,
            error => this.error = error
        );
    }
}
