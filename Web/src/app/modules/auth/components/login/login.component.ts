import { LoaderService } from './../../../../services/loader.service';
import { UserProfileModel } from './../../../../models/user-profile.model';
import { UserService } from '../../../../services/user.service';
import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { ActivatedRoute, NavigationCancel, NavigationEnd, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { MenuComponent } from 'src/app/_metronic/kt/components';
import { LayoutService } from 'src/app/_metronic/layout';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit, OnDestroy {
  // KeenThemes mock, change it to:
  defaultAuth: any = {
    email: '',
    password: '',
  };
  loginForm: FormGroup;
  hasError: boolean;
  returnUrl: string;
  isLoading$: Observable<boolean>;

  // private fields
  private unsubscribe: Subscription[] = []; // Read more: => https://brianflove.com/2016/12/11/anguar-2-unsubscribe-observables/

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private layout: LayoutService,
    private loaderService:LoaderService
  ) {
    // redirect to home if already logged in
    if (this.userService.currentUserValue) {
      this.router.navigate(['/']);
    }
    this.isLoading$ = this.loaderService.isLoading$
    this.routingChanges();
  }

  ngOnInit(): void {
    this.initForm();
    // get return url from route parameters or default to '/'
    this.returnUrl =
      this.route.snapshot.queryParams['returnUrl'.toString()] || '/';

      this.headerContainerCssClasses = this.layout.getStringCSSClasses('headerContainer');
      this.asideDisplay = this.layout.getProp('aside.display') as boolean;
      this.headerLeft = this.layout.getProp('header.left') as string;
      this.pageTitleCssClasses = this.layout.getStringCSSClasses('pageTitle');
      this.pageTitleAttributes = this.layout.getHTMLAttributes('pageTitle');
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.loginForm.controls;
  }

  initForm() {
    this.loginForm = this.fb.group({
      email: [
        this.defaultAuth.email,
        Validators.compose([
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(320),
          // ลบ Validators.email เพื่อปิดการตรวจสอบรูปแบบของอีเมล
        ]),
      ],
      password: [
        this.defaultAuth.password,
        Validators.compose([
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ]),
      ],
    });
  }
  

  submit() {
    this.hasError = false;
    const loginSubscr = this.authService
      .loginByEmail(this.f.email.value, this.f.password.value)
      .pipe(first())
      .subscribe((user: UserProfileModel | undefined) => {
        if (user) {
          this.router.navigate([this.returnUrl]);
        } else {
          this.hasError = true;
        }
      });
    this.unsubscribe.push(loginSubscr);
  }

  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }

  headerContainerCssClasses: string = '';
  asideDisplay: boolean = true;
  headerLeft: string = 'menu';
  pageTitleCssClasses: string = '';
  pageTitleAttributes: {
    [attrName: string]: string | boolean;
  };
  @ViewChild('ktPageTitle', {static: true}) ktPageTitle: ElementRef;


  ngAfterViewInit() {
    if (this.ktPageTitle) {
      for (const key in this.pageTitleAttributes) {
        if (this.pageTitleAttributes.hasOwnProperty(key)) {
          this.ktPageTitle.nativeElement.attributes[key] =
            this.pageTitleAttributes[key];
        }
      }
    }
  }

  routingChanges() {
    const routerSubscription = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd || event instanceof NavigationCancel) {
        MenuComponent.reinitialization();
      }
    });
    this.unsubscribe.push(routerSubscription);
  }
}
