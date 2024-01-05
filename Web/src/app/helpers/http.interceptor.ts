import { Inject, Injectable } from "@angular/core";
import {
  HttpEvent,
  HttpHandler,
  HttpRequest,
  HttpInterceptor
} from "@angular/common/http";

import { catchError, finalize, Observable, of, throwError } from "rxjs";
import { environment } from "src/environments/environment";
//import { AppInjector } from "../services/injector.service";
//import { ToastrService } from "ngx-toastr";
import { LoaderService } from "../services/loader.service";

@Injectable()
export class HttpsInterceptor implements HttpInterceptor {
  //protected toastr: ToastrService;
  //protected loaderService: LoaderService;
  constructor(
    private loaderService :LoaderService
     
     ) {
    //const injector = AppInjector.getInjector();
    //this.toastr = injector.get(ToastrService);
   // this.loaderService = injector.get(LoaderService);
  // this.loaderService = _loaderService;
  }


  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {



    return next.handle(req).pipe(catchError(err => {
      let error = '';
       console.error('http error => ', err)
      // if (err.status === 401) {
      //   // auto logout if 401 response returned from api
      //   error = 'UnAuthorized'
      //   // this.authenticationService.logout();
      // } else if (err.status === 0) {
      //   error = 'Server error contact your server administrator'
      //   // this.authenticationService.logout();
      // } else if (req.responseType === 'blob') {
      //   error = "File not found";
      // } else {
      //   try {
      //     console.log('err => ', err)
      //     err = JSON.parse(err);
      //   } catch (e) {
      //   }
      //   let key = 'http-error';
      //   if (err["error"] && err.error["innererror"] && err.error.innererror["message"]) {
      //     let rawMessag = err.error.innererror.message;
      //     let errorMessage = err.error.innererror.message.toString().split("\n");
      //     error = errorMessage;
      //   } else if (err['error'] && err['error']['errors'] && err['error']['errors']['code']) {
      //     error = err['error']['errors']['code'];
      //     console.log(err);
      //   }
      //   else if (err["error"]) {
      //     if (err.error["Message"]) {
      //       error = err.error.Message;
      //     } else {
      //       error = err.error;
      //     }
      //   } else if (err['message']) {
      //     error = err.message
      //   } else if (err['modelState'] && err.modelState['errors']) {
      //     error = err.modelState.errors
      //   } else if (err['exceptionMessage']) {
      //     error = err.exceptionMessage
      //   } else if (err['statusText']) {
      //     error = err.statusText
      //   }
      //   else {
            error = err.message
            || err.error.error;
      //   }
      // }
      //this.toastr.error(error)
      return throwError(() => new Error(error));
    })).pipe(finalize(() => {
      this.loaderService.stopLoader(req.url)
      console.log('stop !!!!!!!!!!!!!1');
      
    }));
  }
}