import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  validateForm!: FormGroup;
  name: string = "";
  surname : string = "";
  email : string = "";
  password : string = "";
  checkPassword : string = "";
  date : Date = new Date();
  hide: boolean = true;
  hideRp: boolean = true;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) { }

  public hasError = (controlName: string, errorName: string) =>{
    return this.validateForm.controls[controlName].hasError(errorName);
  }


  ngOnInit(): void {
    this.validateForm = this.fb.group({
      name: [null,[Validators.required, Validators.pattern('[A-ZČĆŠĐŽčćđžš][a-zčćđžš]*')]],
      surname: [null, [Validators.required, Validators.pattern('[A-ZČĆŠĐŽčćđžš][a-zčćđžš]*')]],
      email: [null, [Validators.required, Validators.pattern('^(.+)@(.+)$')]],
      password: [null, [Validators.required]],
      checkPassword: [null, [Validators.required, this.confirmationValidator]],
      date: [null, [Validators.required]]
    });
  }

  confirmationValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.validateForm.controls['password'].value) {
      return { confirm: true, error: true };
    }
    return {};
  };

  submitForm(): void {
    this.name = this.validateForm.value.name;
    this.surname = this.validateForm.value.surname;
    this.email = this.validateForm.value.email;
    this.password = this.validateForm.value.password;
    this.date = this.validateForm.value.date;

    
    const body = {
      name: this.name,
      surname: this.surname,
      email : this.email,
      password : this.password,
      dateOfBirth: this.date
    }

    if(this.validateForm.valid){

      console.log(body.dateOfBirth)
      this.authService.registration(body).subscribe(data => { 
          alert("Registration successfull");
          this.router.navigate(['login']);
      }, error => {
        console.log(error.status);
        if(error.status == 400){
          alert("Input is not in valid format");
        }
      });
    }else{
      alert("All fields are reguired and format must be valid!")
    }
  }
}
