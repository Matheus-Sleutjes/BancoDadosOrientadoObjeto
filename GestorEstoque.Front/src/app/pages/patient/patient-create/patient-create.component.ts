import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
//import { CreatePatient } from '../../../models/create-patient';
import { firstValueFrom } from 'rxjs';
//import { ResponseCreatePatient } from '../../../models/response-create-patient';
import { AuthenticationInterceptor } from '../../../interceptors/auth-interceptor';
import { AuthenticationResponse } from '../../../models/response-auth';

@Component({
  standalone: true,  
  selector: 'app-patient-create',
  templateUrl: './patient-create.component.html',
  styleUrl: './patient-create.component.scss',
  imports: [ReactiveFormsModule,CommonModule, HttpClientModule, FormsModule],
  providers: [AuthenticationInterceptor]
})
export class PatientCreateComponent implements OnInit {
  
  patientForm: FormGroup;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder, private router: Router, private http: HttpClient) {
    this.patientForm = this.fb.group({
      patientName: ['', [Validators.required, Validators.minLength(3)]],
    });
  }
  ngOnInit(): void {
  }
  needsLogin = true;
  login?: string;
  password?: string;

  async createpatient() {
    if (this.patientForm.valid) {
      // var newpatient : CreatePatient = {
      //   name : this.patientForm.get('patientName')?.value,
      //   //TODO Add options to choose items
      //   pokerItems : ['1', '2', '3', '5', '8', '13'],
      // }; 
      // //TODO Extract request in service file
      // var response = await firstValueFrom(this.http.post<ResponseCreatePatient>('https://localhost:7277/patient', newpatient));
      // localStorage.setItem("patientOwner", 'true');
      // localStorage.setItem("playerId", response.playerId);
      // this.router.navigate(['/patient/' + response.tagpatient]);
    } else {
      this.errorMessage = 'Por favor, insira um nome de sala válido.';
    }
  }

  async doLogin(login?: string, password?: string) {
    if (login && password) {
      try {
        //TODO Extract request in service file
        let response = await firstValueFrom(this.http.post<AuthenticationResponse>('https://localhost:7277/Authentication/Login', {
          login,
          password
        }));
        if(response.token != null){
          sessionStorage.setItem("token", response.token);
          this.needsLogin = false;
        }
        else{
          alert("Incorrect username/password");
        }
      } catch (e) {
        alert("Incorrect username/password");
      }
    }
  }
}
