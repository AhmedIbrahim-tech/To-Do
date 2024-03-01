import { Injectable } from '@angular/core';
import { environment } from 'src/environment/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Todo } from '../models/todo/todo';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  private baseUrl = `${environment.baseApi}Todo`;

  constructor(private httpClient: HttpClient, private authService:AuthService) { }

  getAllTodos(): Observable<Todo[]> {
    const userId = this.authService.getId();
    return this.httpClient.get<Todo[]>(`${this.baseUrl}/search?userId=${userId}`);
  }

  getAllByUserId():Observable<Todo[]>{
    const userId = this.authService.getId();
    return this.httpClient.get<Todo[]>(`${this.baseUrl}/search?userId=${userId}`);
  }
  getTodoById(id: number): Observable<any> {
    return this.httpClient.get<Todo>(this.baseUrl + `/${id}`);
  }

  createTodo(todo: Todo): Observable<any> {
    return this.httpClient.post<any>(`${this.baseUrl}/CreateToDo`, todo);
  }

  updateTodo(id: number, updatedTodo: any): Observable<any> {
    return this.httpClient.put<Todo>(`${this.baseUrl}/UpdateTodo` + `/${id}`, updatedTodo);
  }

  deleteTodo(id: number): Observable<any> {
    return this.httpClient.delete<Todo>(`${this.baseUrl}/DeleteTodo` + `/${id}`);
  }
}
