import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { TextBoxComponent } from '@progress/kendo-angular-inputs';
import { BehaviorSubject, Observable, Subscription, catchError, map, of } from 'rxjs';
import { UserProfileModel } from 'src/app/models/user-profile.model';
import { AlertService } from 'src/app/services/alert.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-password-setting',
  templateUrl: './password-setting.component.html',
  styleUrls: ['./password-setting.component.scss'],
})
export class PasswordSettingComponent implements OnInit {

  newPro = new Item();
  isLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isLoading: boolean;

  saveDisable$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  saveDisable: boolean;
  private unsubscribe: Subscription[] = [];

  cloneUser : UserProfileModel;
  user$: Observable<UserProfileModel | undefined>;
  user : Observable<UserProfileModel | undefined>;

  @ViewChild("textbox") public textbox: TextBoxComponent;
  @ViewChild("textbox2") public textbox2: TextBoxComponent;

  constructor(private userService: UserService,
    private alertService: AlertService,
    private cdr: ChangeDetectorRef) {
      const loadingSubscr = this.isLoading$
      .asObservable()
      .subscribe((res) => (this.isLoading = res));
    this.unsubscribe.push(loadingSubscr);
    
    const disableSubscr = this.saveDisable$
            .asObservable()
            .subscribe((res) => (this.saveDisable = res));
    this.unsubscribe.push(disableSubscr);
    }


    ngOnInit(): void {
      this.user$ = this.userService.currentUserSubject.asObservable();
  
      this.getProfile()
      console.log(this.userService.currentUserValue)
      this.user.subscribe(user=>{
        this.cloneUser = Object.assign({},user)
      })
      

    }

    getProfile () {
      this.user = this.userService.currentUserSubject.asObservable();
    }

    _user: any = {}; // Assuming you have this variable defined
    showPassword: boolean = false;

    togglePasswordVisibility() {
        this.showPassword = !this.showPassword;
    }

  savePassword(){ 
    debugger;
    this.isLoading$.next(true);
    const profile = {
      Id : this.cloneUser.accountId,
      Password:this.newPro.password,
      ConfirmPassword:this.newPro.confirmPassword
    }
      setTimeout(() => {
        this.isLoading$.next(false);
        this.cdr.detectChanges();
        this.userService.udatePassword(profile).pipe(
          map(() => {
            
            this.alertService.onSuccess('Successfully edited the profile.', '/master/profile');
            return true;
          }),
          catchError((error: HttpErrorResponse) => {
            console.error('error', error);
            this.alertService.onErrorTEam();
            return of(false);
          })
        ).subscribe(
          (response) => {
            console.log(response);
          },
          (error) => {
            console.error(error,);
          }
        );
        this.cdr.detectChanges();
      }, 1500);
    }
}

export class Item {

  id : string;
  username: string;
  password : string;
  confirmPassword: string;
  

  constructor() {
    this.username = '',
    this.password = '',
    this.confirmPassword = ''
    
  }
}