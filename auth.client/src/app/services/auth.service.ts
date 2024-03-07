import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseURL:string ='http://localhost:5232/api/Auth/';
  constructor(private http:HttpClient) { }

  signUp(userObj:any){
    return this.http.post<any>(`${this.baseURL}Register`,userObj);
  }

  logIn(loginObj:any){
    return this.http.post<any>(`${this.baseURL}Authenticate`,loginObj);
  }
}
