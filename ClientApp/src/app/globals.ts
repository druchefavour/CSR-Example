import { Injectable } from '@angular/core';

@Injectable()
export class Globals {
    url: string = window.location.origin;
}