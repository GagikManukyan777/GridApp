import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/observable/throw';

@Injectable()
export class DataServiceService {

  _baseUrl: string = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }
  
  private handleError(error: Response | any) {
    console.error(error.message || error);
    return Observable.throw(error.status);
  }

  public GetProviderData(filter: string, pageNumber: number, pageSize: number) {

    let params = new HttpParams();
    params.append('filter', filter);
    params.append('pageNumber', pageNumber.toString());
    params.append('pageSize', pageSize.toString());

    //return this.http.get(this._baseUrl + 'api/Provider/ProviderSearch' + '?filter=' + filter + '&pageNumber=' + pageNumber + '&pageSize=' + pageSize)
    //.map((res: Response) => {
    //  return res;
    //})
    //.catch(this.handleError);


    return this.http.get(this._baseUrl + 'api/Provider/ProviderSearch' + '?filter=' + filter + '&pageNumber=' + pageNumber + '&pageSize=' + pageSize)
      .map((res: ProviderDataResult) => {
        return res;
      })
      .catch(this.handleError);
}
}

interface ProviderData {
  provider_Name: string;
  federal_Provider_Number: string;
  provider_Address: string;
  provider_Phone_Number: string;
  provider_City: string;
  provider_State: string;
  provider_Zip_Code: string;
}

interface ProviderDataResult {
  items: ProviderData[],
  totalRowCount: number,
  pageNumber: number,
  pageSixe: number
}
