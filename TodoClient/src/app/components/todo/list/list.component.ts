import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Todo } from 'src/app/models/todo/todo';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  todos !: Todo[];
  token: any;
  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private todoService: TodoService
  ) { }
  ngOnInit(): void {
    this.todoService.getAllByUserId().subscribe((response) => {
      this.token = this.authService.getId();
      this.todos = response;
      // this.todoService.getAllTodos().subscribe((response) => {
      //   this.todos = this.todos.concat(response);
      // });
    }, (error) => {
      this.snackBar.open('Error, check your data', 'Close', {
        duration: 3000,
      });
    })
  }

  logOut(){
    this.authService.logOut();
  }
}
