import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      email: [null, [Validators.required]],
      password: [null, [Validators.required]]
    });
  }

  submitForm(): void {
    
    this.email = this.validateForm.value.email;
    this.password = this.validateForm.value.password;

    const body = {
      email: this.email,
      password: this.password
    }

    console.log(body);

    /*this.authService.login(body).subscribe(data => {
      const user = data;
      localStorage.setItem('user', JSON.stringify(user));
      localStorage.setItem('token', JSON.stringify(user.token));

      sessionStorage.setItem('username', user.username);
      let authString = 'Basic ' + btoa(user.username + ':' + user.password);
      sessionStorage.setItem('basicauth', authString);
    }, error => {
      alert("");
    })*/
  }

}
