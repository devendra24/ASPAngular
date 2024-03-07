import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import ValidateForm from 'src/app/helper/validateform';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  signupfrom!:FormGroup;

  constructor(private fb: FormBuilder,private authService:AuthService,private rout:Router){}
  ngOnInit(): void {
    this.signupfrom = this.fb.group({
      username:['',Validators.required],
      email:['',Validators.required],
      password:['',Validators.required],
    })
  }

  onSignUp(){
    if(this.signupfrom.valid){
      this.authService.signUp(this.signupfrom.value).subscribe({
        next:(res)=>{
          alert(res.message);
          this.signupfrom.reset();
          this.rout.navigate(['login']);
        },
        error:(err)=>{
          alert(err.error.message)
        }
      })
    }
    else{
      console.log("invalid");
      ValidateForm.validateAllForms(this.signupfrom)
    }
  }

  
}
