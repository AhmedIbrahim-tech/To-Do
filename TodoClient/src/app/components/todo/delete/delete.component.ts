import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TodoService } from 'src/app/services/todo.service';
import { AuthService } from 'src/app/services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeleteComponent {

  id!:number;

  constructor(
    private router:Router,
    private activedRoute: ActivatedRoute,
    private snackBar:MatSnackBar,
    private todoService:TodoService,
    private authService: AuthService
  ) {}

  cancel(): void {
    this.router.navigateByUrl('/todo/update');
  }

  confirm(): void {
    this.activedRoute.paramMap.subscribe(params => {
      const id = params.get('id');
      if(id){
        this.id = parseInt(id,10);
        this.todoService.deleteTodo(this.id).subscribe(()=>{
          this.snackBar.open('Todo Deleted successfully', 'Close', {
            duration: 3000,
          });
          this.router.navigateByUrl('/todo/update');
        },()=>{
          this.snackBar.open('Error', 'Close', {
            duration: 3000,
          });
          this.router.navigateByUrl('/todo/update');
        })
      }
    })
  }

}
