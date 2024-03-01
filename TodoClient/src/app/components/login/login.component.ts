import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  loginForm !:FormGroup;
  token: any;

  constructor(
    private authService:AuthService,
    private fb:FormBuilder,
    private router:Router,
    private snackBar:MatSnackBar
  ) {
    sessionStorage.clear();
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)]],
      password: ['', [Validators.required]]
      //password: ['', [Validators.required, Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&\s])[A-Za-z\d@$!%*#?&\s]{8,}$/)]]
    });
  }

  ngOnInit(): void {

  }

  login(){
    if(this.loginForm.valid){
      this.authService.login(this.loginForm.value).subscribe((response)=>{
        if(response.data.hasOwnProperty('token')){
          this.token = response.data.token;
          sessionStorage.setItem('token',this.token);
          this.snackBar.open('Welcome','Close',{
            duration:3000
          });
          this.router.navigateByUrl('/todo');
        }
      },(error)=>{
        this.snackBar.open('Error, check your data', 'Close', {
          duration: 3000,
        });
      })
    }
  }

  register(){
    this.router.navigateByUrl('/register');
  }
}
