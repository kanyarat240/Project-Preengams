import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subscription, catchError, map, of } from 'rxjs';
import { UserProfileModel } from 'src/app/models/user-profile.model';
import { AlertService } from 'src/app/services/alert.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.scss'],
})
export class SettingComponent implements OnInit {
    cloneUser : UserProfileModel;

  user$: Observable<UserProfileModel | undefined>;
  user : Observable<UserProfileModel | undefined>;
  isLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isLoading: boolean;

  saveDisable$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  saveDisable: boolean;
  private unsubscribe: Subscription[] = [];


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
    
    this.checkButton()
  }

  getProfile () {
    this.user = this.userService.currentUserSubject.asObservable();
  }

  LETTERS_AND_SPACES_PATTERN_TH = /^[ก-๏\s]*$/;
  PHONE = /^[0-9]{10}$/;


  checkButton() {
    if (this.cloneUser.firstname == '' || 
    this.cloneUser.lastname == '' || 
    this.cloneUser.phone == '' || 
    this.cloneUser.idCard == '' || 
    this.cloneUser.addressDescription == '' || 

    this.LETTERS_AND_SPACES_PATTERN_TH.test(this.cloneUser.firstname) != true ||
    this.LETTERS_AND_SPACES_PATTERN_TH.test(this.cloneUser.lastname) != true ||
   
    this.PHONE.test(this.cloneUser.phone) != true 
   

    ){
      this.saveDisable$.next(true);
    }
    else {
      this.saveDisable$.next(false);
    }
  }



  saveSettings(){ 
    debugger;
    this.isLoading$.next(true);
    const profile = {
      Id : this.cloneUser.id,
      FirstName:this.cloneUser.firstname,
      LastName:this.cloneUser.lastname,
      Phone:this.cloneUser.phone,
      IdCard:this.cloneUser.idCard,
      AddressDescription:this.cloneUser.addressDescription,
  
    }
      setTimeout(() => {
        this.isLoading$.next(false);
        this.cdr.detectChanges();
        this.userService.postUserProfile(profile).pipe(
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

  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }

}
