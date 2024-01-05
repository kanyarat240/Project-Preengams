import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject, Observable } from 'rxjs';
// import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  isLoadingSubject: BehaviorSubject<boolean>;
  isLoading$: Observable<boolean>;

  get currentUserValue(): boolean {
    return this.isLoadingSubject.value;
  }

  constructor(private spinner : NgxSpinnerService) {
    this.isLoadingSubject = new BehaviorSubject<boolean>(false);
    this.isLoading$ = this.isLoadingSubject.asObservable();
  }

  public startLoader(url: string = 'app') {
    //if (this.reqs.length === 0) {
      this.spinner.show();
      console.log('startLoader' , this.spinner);
      
      this. start();
     // localStorage.setItem('isLoading', 'true');
    //}
    //this.reqs.push(url);
  }
  public stopLoader(url: string = 'app') {
   // this.reqs = this.reqs.filter(x => !(x === url));
    //if (this.reqs.length === 0) {
      setTimeout(() => {
        this.stop()
        this.spinner.hide();

        //localStorage.removeItem('isLoading');
      }, 300);
    //}
  }

  start() {
    this.isLoadingSubject.next(true);
  }

  stop() {
    this.isLoadingSubject.next(false);
  }
}
