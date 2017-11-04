import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public rows: Task[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Task').subscribe(
            result => { this.rows = result.json() as Task[]; },
            error => console.error(error));
    }
}

interface Task {
    id: number;
    name: string;
    isCompleted: boolean;
}
