import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  validateForm!: FormGroup;
  email: any;
  password: any;

  hide: boolean = true;

  constructor(private fb: FormBuilder, private authService: AuthService) { }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      email: [null, [Validators.required]],
      password: [null, [Validators.required]]
    });
  }

  getDecodedToken(token: string): any{
    try{
      return jwtDecode(token);
    }
    catch(Error){
      token = "";
    }
  }

  submitForm(): void {
    
    this.email = this.validateForm.value.email;
    this.password = this.validateForm.value.password;

    const body = {
      email: this.email,
      password: this.password
    }

    this.authService.login(body).subscribe(data => {

      console.log(data);
      localStorage.setItem("jwtToken", data);
      let tokenInfo = this.getDecodedToken(data);
      localStorage.setItem('id', tokenInfo.id);
      localStorage.setItem('role', tokenInfo.role);

    }, error => {
      alert("User not found! Check your email and password!");
    })
  }

}
