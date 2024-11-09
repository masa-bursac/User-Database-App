import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { AuthService } from './services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class Interceptor implements HttpInterceptor{

constructor(private authService: AuthService) { }

intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem("token");
   
    if (token) {
      req = req.clone({
            setHeaders: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        });
    }

      return next.handle(req);

    }
}

