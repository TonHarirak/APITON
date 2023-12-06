import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  model: { username: string | undefined, password: string | undefined } = {
    username: undefined,
    password: undefined
  }
  currentUser$: Observable<User | null> = of(null)


  constructor(private toastr: ToastrService, private router: Router, public accountService: AccountService) { }



  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => console.log(user), // user?true:false
      error: err => console.log(err)
    })
  }

  login(): void {
    this.accountService.login(this.model).subscribe({
      next: response => {
        this.router.navigateByUrl('/members')

      },
      error: err => this.toastr.error(err.error)
    })
    console.log(this.model)
  }

  logout(): void {
    this.accountService.logout()

  }
}
