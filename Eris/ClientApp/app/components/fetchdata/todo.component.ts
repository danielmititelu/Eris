import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'todo',
    templateUrl: './todo.component.html'
})
export class TodoComponent {
    public rows: TodoItem[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Task').subscribe(
            result => { this.rows = result.json() as TodoItem[]; },
            error => console.error(error));
    }
}
