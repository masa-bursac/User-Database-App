import { Component, OnInit } from '@angular/core';
import { NavBarComponent } from "../nav-bar/nav-bar.component";
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-observe-users',
  templateUrl: './observe-users.component.html',
  styleUrls: ['./observe-users.component.css']
})
export class ObserveUsersComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'surname', 'email', 'date of birth','user type'];
  dataSource = [];
  id: any = "";

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.userService.GetAllUsers().subscribe((data: any)=>{
    this.dataSource = data;
  }, error => {
    alert("User not authorized! Only admin has access!");
    this.router.navigate(['/login'])
  })
  }
}
