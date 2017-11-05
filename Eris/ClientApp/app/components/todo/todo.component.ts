import { Component, OnInit } from '@angular/core';

import { TodoService } from 'services/todo.service'

@Component({
    selector: 'todo',
    templateUrl: './todo.component.html',
    styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {

    rows: TodoItem[];
    error: any;

    constructor(private todoService: TodoService) { }

    ngOnInit(): void {
        this.getData();

    }

    getData() {
        this.todoService.getTodo().subscribe(
            result => this.rows = result,
            error => this.error = error
        );
    }

    addTodo(value: string) {
        let todoItem: TodoItem = {
            id: 0,
            isCompleted: false,
            name: value
        };

        this.todoService.addTodo(todoItem).subscribe(
            result => this.rows.push(result),
            error => this.error = error
        );
    }

    deleteTodo(id: number) {
        let rows = this.rows;
        this.todoService.deleteTodo(id).subscribe(
            result => {
                let index = rows.findIndex(r => r.id === result.id);
                if (index != -1) { rows.splice(index, 1) }
            },
            error => this.error = error
        );
    }
}
