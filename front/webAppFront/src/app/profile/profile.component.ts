import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Route, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

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
      new: new FormControl(),
      date: new FormControl(),
  });
  hide: boolean = true;
  files: File[] = [];
  base64textString: any = "";
  id: any;
  password: any;
  imagePath: any;
  image: any;

  constructor(private fb: FormBuilder, private userService: UserService, private router: Router, private _snackBar: MatSnackBar) { }

  public hasError = (controlName: string, errorName: string) =>{
    return this.validateForm.controls[controlName].hasError(errorName);
  }

  ngOnInit(): void {
    this.id = localStorage.getItem('id');
    
    this.userService.GetUser(Number(this.id)).subscribe((data: any)=>{

      this.password = data.password;
      this.image = atob(data.image);
      this.imagePath = "data:image/png;base64,"+ atob(data.image);
      this.validateForm = this.fb.group({
        name: [data.name,[Validators.required, , Validators.pattern('[A-ZČĆŠĐŽčćđžš][a-zčćđžš]*')]],
        surname: [data.surname, [Validators.required, , Validators.pattern('[A-ZČĆŠĐŽčćđžš][a-zčćđžš]*')]],
        email: [{ value: data.email, disabled: true }, [Validators.required]],
        password: [null],
        new: [null],
        date: [data.dateOfBirth, [Validators.required]]
      }); 
  }, error => {
    alert("You must be logged in!");
    this.router.navigate(['/login']);
  });  
  }

  submitForm() : void {
   
    const body = {
      id: Number(localStorage.getItem('id')),
      checkPassword: this.validateForm.value.password,
      newPassword: this.validateForm.value.new,
      name: this.validateForm.value.name,
      surname: this.validateForm.value.surname,
      dateOfBirth: this.validateForm.value.date,
      image: this.base64textString
    }
    if(this.validateForm.valid){
      this.userService.UpdateUser(body).subscribe((data: any) =>{
        this._snackBar.open('User updated!', '', {
          duration: 2000
        });
          this.ngOnInit();
      }, error => {
        if(error.status == 400){
          alert("Old password doesn't match!");
        }
      });
    }else{
      alert("All fields are reguired and format must be valid!")
    }
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
