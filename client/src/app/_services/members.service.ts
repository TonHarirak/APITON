import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { Member } from '../_models/members';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl
  members: Member[] = []

  constructor(private http: HttpClient) { }

  // getHttpOptions() {
  //  const userString = localStorage.getItem('user')
  // if (!userString) return
  // const user: User = JSON.parse(userString)
  // return {
  // headers: new HttpHeaders({
  //  Authorization: 'Bearer ' + user.token
  //})
  //}
  //}

  getMembers() {
    if (this.members.length > 0) return of(this.members)
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(users => {
        this.members = users
        return users
      })
    )
  }

  getMember(username: string) {
    const member = this.members.find(user => user.userName == username)
    if (member) return of(member)
    return this.http.get<Member>(this.baseUrl + 'users/username/' + username)
  }

  updateProfile(member: Member) {
    const endpoint = `${this.baseUrl} users`
    return this.http.put(endpoint, member).pipe(
      map(_ => {
        const index = this.members.indexOf(member)
        this.members[index] = { ...this.members[index], ...member }
      })
    )
  }
}

