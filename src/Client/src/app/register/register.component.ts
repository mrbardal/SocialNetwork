import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private service: UserService,
    private toastr: ToastrService) {
    this.registerForm = fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  register() {
    this.service.register(this.registerForm.value as User).subscribe((data: any) => {
      this.toastr.success("Register Successfully!", "Register");

      this.router.navigate(['/login']);
    }, (error) => {
      this.toastr.error("Register Failed, Username already registered", "Register");
    });
  }
}
