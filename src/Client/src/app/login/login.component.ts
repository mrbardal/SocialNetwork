import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private service: UserService,
    private toastr: ToastrService) {
    this.loginForm = fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  login() {
    console.log(this.loginForm.value);

    this.service.login(this.loginForm.value as User).subscribe((data: any) => {
      // console.log(data);
      localStorage.setItem("userName", data.userName);
      localStorage.setItem("accessToken", data.accessToken);
      this.router.navigate(['/']);
    }, (error) => {
      this.toastr.error("Login Failed", "Login");
    });
  }
}
