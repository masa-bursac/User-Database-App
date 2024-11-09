import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  public decodedToken: any;
  public role: any;

  constructor(private router: Router) { }

  ngOnInit(): void {
    if(localStorage.getItem('jwtToken')){
      this.getToken();
    }
  }

  private getToken(): void {
    this.decodedToken = this.getDecodedAccessToken(JSON.parse(localStorage.getItem('jwtToken') || '{}'));
    this.role = this.decodedToken.role;
    console.log(this.role);
  }

  getDecodedAccessToken(token: string): any {
    try {
      return jwtDecode(token);
    }
    catch (Error) {
      return null;
    }
  }

  public LogOut(): void {
    localStorage.clear();
    this.router.navigateByUrl('welcome');
  }

}
