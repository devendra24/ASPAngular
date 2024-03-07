import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import ValidateForm from 'src/app/helper/validateform';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForms!:FormGroup;

  constructor(private fb: FormBuilder ,private authService:AuthService ,private rout:Router){}
  ngOnInit(): void {
    this.loginForms = this.fb.group({
      username:['',Validators.required],
      password:['',Validators.required],
    })
  }

  onLogin(){
    if(this.loginForms.valid){
      console.log(this.loginForms.value);
      this.authService.logIn(this.loginForms.value).subscribe({
        next:(res)=>{
          alert(res.message);
          this.loginForms.reset();
          this.rout.navigate(['dashboard']);
        },
        error:(err)=>{
          alert(err.error.message)
        }
      })
    }
    else{
      console.log("invalid");
      ValidateForm.validateAllForms(this.loginForms)
    }
  }
}
