import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TodoService } from 'src/app/services/todo.service';
import { AuthService } from 'src/app/services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {
  updateForm !: FormGroup;
  todo !: any;
  id!: number;

  constructor(
    private authService: AuthService,
    private todoService: TodoService,
    private router: Router,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private activeRouter: ActivatedRoute
  ) {
    const userid = this.authService.getId();
    this.updateForm = this.fb.group({
      userId: [parseInt(userid)],
      title: ['', [Validators.required]],
      completed: [false, [Validators.required]],
    });
  }
  ngOnInit(): void {
    this.activeRouter.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.id = parseInt(id, 10);
        this.todoService.getTodoById(this.id).subscribe((response) => {
          console.log(response);
          sessionStorage.setItem('todo', JSON.stringify(response));
          this.updateForm.patchValue({
            title: response.title,
            completed: response.completed
          });
        });
      }
    });
  }

  update(): void {
    this.todo = sessionStorage.getItem('todo');
    this.todo = JSON.parse(this.todo);
    this.updateForm.get('completed')?.setValue(this.updateForm.value.completed == 'true' ? true : this.todo.completed);
    this.todoService.updateTodo(this.id, this.updateForm.value).subscribe((response) => {
      this.snackBar.open('Todo updated successfully', 'Close', {
        duration: 3000,
      });
      sessionStorage.removeItem('todo');
      this.router.navigateByUrl('/todo/update');
    }, (error) => {
      sessionStorage.removeItem('todo');
      this.snackBar.open('Error, check your data', 'Close', {
        duration: 3000,
      });
      this.router.navigateByUrl('/todo/update');
    });
  }

  back() {
    sessionStorage.removeItem('todo');
    this.router.navigateByUrl('/todo/update');
  }

}
