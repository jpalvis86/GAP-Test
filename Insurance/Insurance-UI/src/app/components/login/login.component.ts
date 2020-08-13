import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
} from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  loading: boolean;
  errorMessage: string;

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  get email(): AbstractControl {
    return this.form.get('email');
  }
  get password(): AbstractControl {
    return this.form.get('password');
  }

  async onSubmit(): Promise<void> {
    this.loading = true;

    const email = this.email.value;
    const password = this.password.value;

    try {
      if (email !== 'jpan_2009@hotmail.com' && password !== '12345678') {
        throw new Error('Password Invalid');
      }
    } catch (error) {
      this.errorMessage = error;
    }
    this.loading = false;
  }
}
