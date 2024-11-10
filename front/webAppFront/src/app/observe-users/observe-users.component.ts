import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-observe-users',
  templateUrl: './observe-users.component.html',
  styleUrls: ['./observe-users.component.css']
})
export class ObserveUsersComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'surname', 'email', 'date of birth','user type', 'delete'];
  dataSource = [];
  id: any = "";

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
  }, error => {
    alert("User not authorized! Only admin has access!");
    this.router.navigate(['/login'])
  })
  }
}
