import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class TodoService {

    constructor(private http: Http, @Inject('BASE_URL')
                private baseUrl: string) { }

    getHeroes(): Observable<TodoItem[]> {
        return this.http.get(this.baseUrl + 'api/Task')
                .map(response => response.json() as TodoItem[])
                .catch(error => Observable.of(error));
    }
}