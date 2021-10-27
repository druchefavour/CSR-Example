import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SharedServiceService } from '../../shared-service.service'
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  public users: Users[];

  constructor(private http: HttpClient, private searchService: SearchService, private sharedService: SharedServiceService) { }

getAll():Observable<any> {
  let baseurl = window.location.origin
  return this.http.get(baseurl + "/api/listUsers/listall");
  }

loadMore():Observable<any> {
  let baseurl = window.location.origin
  return this.http.get(baseurl + "/api/listUsers/listmore");
  }

  
search(q: string): Observable<any> {
  if (!q || q === '*') {
    q = '';
  } else {
    q = q.toLowerCase();
  }
  return this.getAll().pipe(
    map((data: any) => data
      .filter(item => JSON.stringify(item).toLowerCase().includes(q)))
  );
}

//return this.searchService.getAll()
  //.pipe(
   // map((data: any) => data
   //     .map(item => !!localStorage['user' + item.id] ?
   //       JSON.parse(localStorage['user' + item.id]) : item)
   //     .filter(item => JSON.stringify(item).toLowerCase().includes(q))
   // ));

list2ndPage(q: string): Observable<any> {
    if (!q || q === '*') {
      q = '';
    } else {
      q = q.toLowerCase();
    }
    return this.loadMore().pipe(
      map((data: any) => data
        .filter(item => JSON.stringify(item).toLowerCase().includes(q)))
    );
  }

get(id: string) {
  return this.getAll().pipe(map((all: any) => {
    if (localStorage['user' + id]) {
      return JSON.parse(localStorage['user' + id]);
    }
    return all.find((e: { id: string; }) => e.id === id);
  }));
}

save(user: Users) {
  localStorage['user' + user.id] = JSON.stringify(user);
  }
}

export class Link {
  pageLink: string;
}

export class Profile {
  firstName: string;
  lastName: string;
  login: string;
  email: string;
  birthdate: string;
  drivers_license: string;
  ibisId: string;
  security_answer: string;
  streetAddress: string;
  city: string;
  state:string;
  zipCode:string;
}

export class Password {
  value: string;
}

export class RecoveryQuestion {
  question: string;
}

export class Provider {
  type: string;
  name: string;
}

export class Credentials {
  password: Password;
  recovery_question: RecoveryQuestion;
}

export class Users {
  id: string;
  status: string;
  profile: Profile;
  credentials: Credentials;
}


function data(data: any, any: any) {
  throw new Error('Function not implemented.');
}

