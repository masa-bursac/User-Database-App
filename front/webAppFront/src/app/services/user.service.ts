import { HttpClient, HttpParams } from '@angular/common/http';
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

  public DeleteUser(id: number) : any {
    return this.http.post(auth_url+'/delete', id);
  }

  public GetUser(id: number) : Observable<any> {
    return this.http.get(`${auth_url}/findById/${id}`);
  }

  public UpdateUser(body: any): Observable<any>  {
    return this.http.post(auth_url+"/update", body,  { responseType: 'text' as 'json'});
  }

  public Search(body: any): Observable<any>  {
    return this.http.post(auth_url+"/search", body);
  }
}
