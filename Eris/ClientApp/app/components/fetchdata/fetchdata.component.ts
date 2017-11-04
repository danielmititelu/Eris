import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public rows: ToDoItem[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Task').subscribe(
            result => { this.rows = result.json() as ToDoItem[]; },
            error => console.error(error));
    }
}

interface ToDoItem {
    id: number;
    name: string;
    isCompleted: boolean;
}
