import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  //@Input() usersFromHomeCpmponent: any
  @Output() isCancel = new EventEmitter()

  constructor(private router: Router, private accountService: AccountService, private toastr: ToastrService) { }
  model: any = {}

  register() {
    this.accountService.register(this.model).subscribe({
      error: err => this.toastr.error(err.error),
      next: () => this.router.navigateByUrl('/members')
    })
  }
  cancel() {
    this.isCancel.emit(true)
  }
}
