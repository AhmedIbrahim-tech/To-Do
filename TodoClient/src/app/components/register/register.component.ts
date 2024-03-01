import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  registerForm !: FormGroup;

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private snackBar: MatSnackBar,
  ) {
    this.registerForm = this.fb.group({
      userName: ['', [Validators.required, Validators.pattern(/^[a-zA-Z\s]*$/)]],
      email: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^(010|012|011|015)\d{8}$/)]],
      password: ['', [Validators.required]]
      //password: ['', [Validators.required, Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&\s])[A-Za-z\d@$!%*#?&\s]{8,}$/)]]
    });

  }

  register(): void {
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value).subscribe(() => {
        this.snackBar.open('Account created successfully', 'Close', {
          duration: 3000,
        });
        this.router.navigateByUrl('/login');
      }, (erro) => {
        this.snackBar.open('Error, check your data', 'Close', {
          duration: 3000,
        });
      })
    }
  }

  login() {
    this.router.navigateByUrl('/login');
  }
}
