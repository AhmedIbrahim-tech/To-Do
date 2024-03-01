import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Todo } from 'src/app/models/todo/todo';
import { TodoService } from 'src/app/services/todo.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-update-list',
  templateUrl: './update-list.component.html',
  styleUrls: ['./update-list.component.css']
})
export class UpdateListComponent implements OnInit {

  todos !: Todo[];
  token: any;
  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private todoService: TodoService,
    private router: Router
  ) { }
  ngOnInit(): void {
    this.todoService.getAllTodos().subscribe((response) => {
      this.token = this.authService.getId();
      this.todos = response;
    }, (error) => {
      this.snackBar.open('Error, check your data', 'Close', {
        duration: 3000,
      });
    })
  }

  back() {
    this.router.navigateByUrl('/todo');
  }
  
  logOut(){
    this.authService.logOut();
  }
}
