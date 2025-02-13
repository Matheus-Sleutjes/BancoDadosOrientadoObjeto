import { Component, OnInit } from '@angular/core';
import { AuthenticationInterceptor } from '../../interceptors/auth-interceptor';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Login } from '../../models/login';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  providers: [AuthenticationInterceptor],
})
export class LoginComponent implements OnInit{
  public form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService) {

    this.form = this.fb.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {

  }

  async onSubmit(){
    var dto: Login =
    {
      // password: this.form.get("password")?.value,
      // email: this.form.get("email")?.value
      senha: "matheus123",
      email: "matheussleutjes@gmail.com"
    }
    await this.authService.login(dto).subscribe((response) =>{
      console.log(response);
    });
    this.onRedirect();
  }

  onRedirect(){
    this.router.navigateByUrl("/usuario");
  }
}
