import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  validateForm!: FormGroup;
  hide: boolean = true;
  files: File[] = [];
  base64textString: any;
  id: any;
  password: any;

  constructor(private fb: FormBuilder, private userService: UserService, private router: Router) { }

  public hasError = (controlName: string, errorName: string) =>{
    return this.validateForm.controls[controlName].hasError(errorName);
  }

  ngOnInit(): void {

    this.validateForm = this.fb.group({
      name: [null,[Validators.required]],
      surname: [null, [Validators.required]],
      email: [null, [Validators.required]],
      password: [null, [Validators.required]],
      date: [null, [Validators.required]]
    })

    this.id = localStorage.getItem('id');
    this.userService.GetUser(Number(this.id)).subscribe((data: any)=>{
      console.log(data)
      this.password = data.password;
      this.validateForm = this.fb.group({
        name: [data.name,[Validators.required]],
        surname: [data.surname, [Validators.required]],
        email: [data.email, [Validators.required]],
        password: [null, [Validators.required]],
        date: [data.dateOfBirth, [Validators.required]]
      }); 
  }, error => {
    alert("You must be logged in!");
    this.router.navigate(['/login']);
  });  
  }

  submitForm() : void {

  }

  onSelect(event:any) {
    this.files.push(...event.addedFiles);
    var file = this.files[0];
    if (this.files && file) 
    {
      var reader = new FileReader();
      reader.onload =this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt:any) {
    var binaryString = readerEvt.target.result;
    this.base64textString= btoa(binaryString);
   }

  onRemove(event:any) {
    this.files.splice(this.files.indexOf(event), 1);
  }


}
