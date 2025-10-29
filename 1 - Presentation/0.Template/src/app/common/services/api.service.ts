import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { AuthService } from '../../core/auth/auth.service';
import qs from 'qs';
import { NEVER, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import Swal, { SweetAlertIcon, SweetAlertOptions } from 'sweetalert2';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private urlBase: string = '';


  httpOptions = {
    headers: new HttpHeaders({
      'Content-type': 'application/json'
    })
  };

  constructor(
    private http: HttpClient,
    private authService: AuthService,
  ) {
    this.urlBase = environment.baseUrl;
    this.httpOptions.headers.append("Authorization", 'bearer ' + this.authService.accessToken);
  }

  public getUrlApi() {
    return this.urlBase;
  }

  public uploadCustom(formData: FormData, url: string, folder: string, rename: boolean = false): Observable<any> {

    let _count = 0;

    var post = this.http.post<any>(url, formData, this.requestOptions(false, { folder, rename }));
    return this.processResponse(post, _count, true, true);

  }

  public upload(file: File, folder: string, rename: boolean): Observable<any> {

    let formData: FormData = new FormData();
    formData.append('files', file, file.name);
    // formData.append('folder', folder);
    // formData.append('rename', rename ? "true" : "false");

    let url = this.urlBase + '/upload';
    return this.uploadCustom(formData, url, folder, rename);
  }

  public uploadOfx(file: File): Observable<any> {

    let formData: FormData = new FormData();
    formData.append('files', file, file.name);
    // formData.append('folder', folder);
    // formData.append('rename', rename ? "true" : "false");

    let url = this.urlBase + '/ConciliacaoArquivo/Upload';
    return this.uploadCustom(formData, url, null, null);
  }

  public uploadMultiple(files: File[], folder: string, rename: boolean): Observable<any> {

    let formData: FormData = new FormData();
    for (const file of files) {
      formData.append('files', file, file.name);
    }

    // formData.append('folder', folder);
    // formData.append('rename', rename ? "true" : "false");

    let url = this.urlBase + '/upload';
    return this.uploadCustom(formData, url, folder, rename);
  }

  public get(resource: any, route: any, filters: any, showLoading = true): Observable<any> {
    filters = filters || {};
    // filters.random = String(Math.random());
    return this.http
      .get(this.urlBase + '/' + resource + '/' + route, {
        params: this.setParameter(filters),
        headers: this.httpOptions.headers
      })
      .pipe(
        map((data: any) => {
          // Verificar se a resposta tem a estrutura esperada
          if (data && data.data !== undefined) {
            return data;
          }
          // Se não tem a estrutura esperada, retornar como está
          return { data: data };
        }),
        catchError((error) => {
          this.handleHttpError(error);
          return throwError(error);
        })
      );
  }

  public getDataItem(resource: string, filters: any = null): Observable<any> {
    const params = new HttpParams({ fromString: qs.stringify(filters || {}) });
    return this.http.get(`${this.urlBase}/${resource}/GetDataItem`, { headers: this.makeHeaders(), params })
      .pipe(
        map(this.handleMapDataItem),
        catchError((error) => {
          this.handleHttpError(error);
          return throwError(error);
        })
      );
  }

  private makeHeaders() {

    let headers = new HttpHeaders({
      'Access-Control-Allow-Origin': 'http://localhost:4200'
    });

    const accessToken = this.authService.accessToken;
    if (accessToken) {
      headers = headers.set('Authorization', `Bearer ${accessToken}`)
    }

    return headers;
  }

  public getCEP(resource: any): Observable<any> {
    return this.http
      .get('https://viacep.com.br/ws/' + resource + '/json')
      .pipe(
        map((data: any) => {
          return data;
        }),
        catchError((error) => {
          this.handleHttpError(error);
          return throwError(error);
        })
      );
  }

  private setParameter(routerParams: Params): HttpParams {

    if (!routerParams) return null;

    let queryParams = new HttpParams();
    for (const key in routerParams) {
      if (routerParams.hasOwnProperty(key)) {
        queryParams = queryParams.set(key, routerParams[key]);
      }
    }
    return queryParams;
  }

  private makeSearchParams(filters?: any): URLSearchParams {
    const params = new URLSearchParams('');
    if (filters != null) {
      for (const key in filters) {
        if (key.toLowerCase().startsWith('collection')) {
          if (filters[key]) {
            let values = filters[key].toString().split(',');
            for (let value in values) {
              params.append(key, values[value]);
            }
          }
        } else if (filters.hasOwnProperty(key)) {
          params.set(key, filters[key]);
        }
      }
    }

    return params;
  }

  public post(resource: any, model: any, showLoading: boolean = true, showNotification: boolean = true): Observable<any> {

    return this.http.post<any>(this.urlBase + '/' + resource + '?random=' + String(Math.random()), model, this.httpOptions).pipe(
      map((data: any) => {
        if (showNotification) {
          this.success('Salvo com sucesso')
        }
        return data;
      }),
      catchError((error) => {
        this.handleHttpError(error);
        return throwError(error);
      })
    );
  }

  public post_route(resource: any, route: any, model: any, showLoading: boolean = true, showNotification: boolean = true): Observable<any> {

    return this.http.post<any>(this.urlBase + '/' + resource + '/' + route, model, this.httpOptions);
  }

  public put(resource: any, model: any, showLoading: boolean = true, showNotification: boolean = true): Observable<any> {

    return this.http.put(this.urlBase + '/' + resource + '?random=' + String(Math.random()), model, this.httpOptions).pipe(
      map((data: any) => {
        if (showNotification) {
          this.success('Salvo com sucesso')
        }
        return data;
      }),
      catchError((error) => {
        this.handleHttpError(error);
        return throwError(error);
      })
    );
  }

  public put_route(resource: any, route: any, model: any, showLoading: boolean = true, showNotification: boolean = true): Observable<any> {
    return this.http.put(this.urlBase + '/' + resource + '/' + route, model, this.httpOptions);
  }

  public delete(resource: any, model: any, showLoading: boolean = true, showNotification: boolean = true): Observable<any> {
    var ro = Object.assign(this.httpOptions, this.makeSearchParams(model));
    return this.http.delete(this.urlBase + '/' + resource + '?random=' + String(Math.random()), {
      body: model,
      headers: this.httpOptions.headers
    }).pipe(
      map((data: any) => {
        if (showNotification) {
          this.success('Excluído com sucesso')
        }
        return data;
      }),
      catchError((error) => {
        this.handleHttpError(error);
        return throwError(error);
      })
    );
  }

  private requestOptions(contentType: boolean = true, params: any = null): any {
    return {
      headers: this.defaultHeaders(contentType),
      params: this.setParameter(params)
    }
  }

  private defaultHeaders(contentType: boolean = true) {

    if (contentType)
      return Object.assign(this.HeaderAuth(), this.HeaderContentType());

    return Object.assign(this.HeaderAuth());

  }

  private processResponse(response: Observable<any>, _count: number, notification: boolean, jsonResult: boolean): Observable<any> {
    return response.pipe(
      map(res => {
        _count = this.countReponse(res);
        return jsonResult ? this.successJsonResult(res) : this.successResult(res);

      }));
  }

  private countReponse(res: any) {
    return res.dataList ? res.dataList.length : res.data ? 1 : 0;
  }

  private errorResult(response: any): Observable<any> {

    if (response.status == 401) {
      //this.router.navigate(["/login"]);
    }

    if (response.status == 403) {
      //this.router.navigate(["/unauthorized"]);
    }

    let _response = response;
    let erros = "ocorreu um erro!";
    //if (_response.result != null) {
    //  erros = _response.result.errors[0];
    //}


    return this.riseThrow(erros);

  }

  private riseThrow(erros: any) {
    return throwError(erros);
    //return Observable.throw(erros);
  }

  private successResult(response: any): any {
    return response;
  }

  private successJsonResult(response: Observable<any>): Observable<any> {
    let _response = response
    return _response;
  }

  private HeaderAuth() {
    return {
      'Authorization': 'bearer ' + (this.authService.accessToken || '')
    }
  }

  private HeaderContentType() {
    return {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': 'http://localhost:4200'
    }
  }

  public success(msg?: string, modal: boolean = false) {
    this.show("success", msg || "Ação realizada com sucesso.", "", modal);
  }

  public fail(msg?: string, modal: boolean = false) {
    this.show("error", msg || "Erro ao executar ação.", "", modal);
  }

  show(type: SweetAlertIcon, title: string, text: string, modal: boolean) {

    let _obj: SweetAlertOptions = {
      icon: type,
      title: title,
      html: text,
      position: "top-end",
      toast: true,
      showConfirmButton: false,
      didOpen: (toast: any) => {
        toast.addEventListener("mouseenter", Swal.stopTimer);
        toast.addEventListener("mouseleave", Swal.resumeTimer);
      }
    }

    if (modal) {
      _obj.showConfirmButton = true;
      _obj.toast = false;
      _obj.position = "center";
    }
    else {
      _obj.timer = 5000;
      _obj.timerProgressBar = true;
    }

    Swal.fire(_obj)
  }

  public getUrlConfig(more: boolean, resource?: string, filterFieldName?: string, filterBehavior?: string, filters?: any, processResultsCustom?: any, labelInitial?: any) {
    var authConfig = this.defaultHeaders();
    var url = this.urlBase + '/' + resource + '/' + filterBehavior;
    var filterNew = filters;

    var processResultsDefault = function (result: any, params: any) {

      let dataList = result.data.dataList.map((item: any) => {
        let data = {
          id: item.id,
          text: item.name
        };
        return data;
      });


      if (labelInitial) {
        dataList.unshift({
          id: '',
          text: labelInitial
        });
      }


      if (filterBehavior != "GetDataListCustomPaging") {

        params.page = params.page || 1;

        return {
          results: dataList,
          pagination: {
            more: (params.page * result.data.summary.pageSize) < result.data.summary.total
          }
        };

      }

      return {
        results: dataList,
      };


    };

    if (processResultsCustom)
      processResultsDefault = processResultsCustom

    return {
      url: url,
      dataType: 'json',
      headers: authConfig,
      data: function (params: any) {

        var filterComposite = Object.assign(filterNew || {}, {
          filterBehavior: filterBehavior,
        });

        filterComposite["ids"] = null;
        filterComposite[filterFieldName + ""] = params.term;
        filterComposite.pageIndex = params.page || 1;

        return toQueryString(filterComposite);

        function toQueryString(filters: any) {
          var queryString = "";
          if (filters != null) {


            for (const key in filters) {
              if (key.toLowerCase().startsWith("collection")) {
                if (filters[key]) {
                  let values = filters[key].toString().split(",");
                  var params = "";
                  for (let value in values) {
                    if (values[value])
                      queryString += key + "=" + values[value] + "&";
                  }
                }
              }
              else {
                if (filters[key])
                  queryString += key + "=" + filters[key] + "&";
              }
            }
          }
          return queryString;

        }
      },
      processResults: processResultsDefault

    };
  }


  private handleMapDataItem(result: any): any {
    if (result.result.data)
      return result.result.data;

    return result;
  }

  private handleHttpError(error: HttpErrorResponse): void {
    switch (error.status) {
      case 400:
        // Erro de validação
        this.fail(error.error?.ErrorList?.join(', ') || 'Dados inválidos');
        break;
      case 401:
        // Não autorizado - redirecionar para login
        window.location.href = '/login';
        break;
      case 403:
        // Proibido
        this.fail('Acesso negado');
        break;
      case 404:
        // Não encontrado
        this.fail('Recurso não encontrado');
        break;
      case 500:
        // Erro interno do servidor
        this.fail(error.error?.Exception || 'Erro interno do servidor');
        break;
      default:
        // Outros erros
        this.fail('Erro desconhecido');
        break;
    }
  }
}
