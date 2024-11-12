import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-observe-users',
  templateUrl: './observe-users.component.html',
  styleUrls: ['./observe-users.component.css']
})
export class ObserveUsersComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'surname', 'email', 'date of birth','user type', 'delete'];
  dataSource = [];
  id: any = "";
  startDate: any;
  endDate: any;

  validateForm = new FormGroup({
    email: new FormControl(),
    startDate: new FormControl('1924-01-01T00:00:00'),
    endDate: new FormControl('2074-01-31T23:59:59'),
});

  constructor(private userService: UserService, private router: Router, private _snackBar: MatSnackBar) { }

  Delete(element: { id: number }){
    this.userService.DeleteUser(element.id).subscribe((data: any) =>{
      this.ngOnInit();
      this._snackBar.open('User deleted!', '', {
        duration: 2000
      });
    });
    
  }

  ngOnInit(): void {
    this.userService.GetAllUsers().subscribe((data: any)=>{
    this.dataSource = data;
    console.log(data)
  }, error => {
    alert("User not authorized! Only admin has access!");
    this.router.navigate(['/login'])
  })
  }

  search(): void{
    const body = {
      email: this.validateForm.value.email,
      startDate: this.validateForm.value.startDate,
      endDate: this.validateForm.value.endDate
    }
    this.userService.Search(body).subscribe((data: any)=>{
      this.dataSource = data;
    })
  }
}
