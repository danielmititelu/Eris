import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public rows: FirstTable[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/FirstTables').subscribe(result => {
            this.rows = result.json() as FirstTable[];
        }, error => console.error(error));
    }
}

interface FirstTable {
    id: number;
    name: string;
}
