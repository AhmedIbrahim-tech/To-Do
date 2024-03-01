import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TodoService } from 'src/app/services/todo.service';
import { AuthService } from 'src/app/services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent {
  createForm !: FormGroup;
  choose = [true, false];

  constructor(
    private authService: AuthService,
    private todoService: TodoService,
    private router: Router,
    private fb: FormBuilder,
    private snackBar: MatSnackBar
  ) {
    const userid = this.authService.getId();
    this.createForm = this.fb.group({
      userId: [parseInt(userid)],
      title: ['', [Validators.required]],
      completed: [false, [Validators.required]],
    });
  }

  create():void{
    this.createForm.get('completed')?.setValue(this.createForm.value.completed=='true'?true:false);
    this.todoService.createTodo(this.createForm.value).subscribe((response)=>{
      this.snackBar.open('Todo created successfully', 'Close', {
        duration: 3000,
      });
      this.router.navigateByUrl('/todo');
    }, (error) => {
      this.snackBar.open('Todo creation failed', 'Close', {
        duration: 3000,
      });
      this.router.navigateByUrl('/todo');
    });
  }

  back() {
    this.router.navigateByUrl('/todo');
  }

}

