import { Injectable } from '@angular/core';
import {environment} from '../../../../environments/environment' 

@Injectable()
export class ConfigService {
     
    _apiURI : string;
 
    constructor() {
        this._apiURI = environment.apiEndpoint;
     }
 
     getApiURI() {
         return this._apiURI;
     }    
}