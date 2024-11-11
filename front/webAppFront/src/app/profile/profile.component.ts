import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  validateForm = new FormGroup({
      name: new FormControl(),
      surname: new FormControl(),
      email: new FormControl({ value: '', disabled: true }),
      password: new FormControl(),
      newPassword: new FormControl(),
      date: new FormControl(),
  });
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
    this.id = localStorage.getItem('id');
    this.userService.GetUser(Number(this.id)).subscribe((data: any)=>{
      console.log(data)
      this.password = data.password;
      this.validateForm = this.fb.group({
        name: [data.name,[Validators.required]],
        surname: [data.surname, [Validators.required]],
        email: [{ value: data.email, disabled: true }, [Validators.required]],
        password: [null],
        newPassword: [null],
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
