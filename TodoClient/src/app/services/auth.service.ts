import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environment/environment';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserRegister } from '../models/register/user';
import { UserLogin } from '../models/login/Uset';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = `${environment.baseApi}Account`;
  token: any;
  decodedToken: any;
  helper = new JwtHelperService();

  constructor(private httpClient: HttpClient) { }

  register(model: UserRegister): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/register`, model);
  }

  login(model: UserLogin): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/login`, model);
  }

  logOut() {
    sessionStorage.removeItem('token');
  }

  isLoggedIn() {
    return sessionStorage.getItem('token') != null;
  }

  getId() {
    this.token = sessionStorage.getItem('token') != null ? sessionStorage.getItem('token')?.toString() : '';
    this.decodedToken = this.helper.decodeToken(this.token);
    return this.decodedToken['userId'];
  }

  getToken() {
    return this.token = sessionStorage.getItem('token') != null ? sessionStorage.getItem('token')?.toString() : '';
  }
}
