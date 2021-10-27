import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedServiceService {

  constructor() { }

  storedPostResult: any;
  storedIbisAccessRequestGroupID: any;
  storedIbisGroupID: any;
}