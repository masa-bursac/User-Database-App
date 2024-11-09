import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const auth_url = 'http://localhost:5000/api/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  public GetAllUsers() : Observable<any>{
    return this.http.get(auth_url);
  }
}
