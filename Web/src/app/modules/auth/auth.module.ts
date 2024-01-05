import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { LogoutComponent } from './components/logout/logout.component';
import { AuthComponent } from './auth.component';
import { TranslationModule } from '../i18n/translation.module';
import { WizardsRoutingModule } from '../wizards/wizards-routing.module';
import { InlineSVGModule } from 'ng-inline-svg-2';
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { DropDownsModule } from "@progress/kendo-angular-dropdowns";
import { UploadsModule } from "@progress/kendo-angular-upload";
import { LabelModule } from "@progress/kendo-angular-label";
import { DateInputsModule } from "@progress/kendo-angular-dateinputs";
import { InputsModule } from "@progress/kendo-angular-inputs";
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { MatIconModule } from '@angular/material/icon';
import { Step1Component } from './components/registration/steps/step1/step1.component';
import { Step2Component } from './components/registration/steps/step2/step2.component';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  declarations: [
    LoginComponent,
    RegistrationComponent,
    ForgotPasswordComponent,
    LogoutComponent,
    AuthComponent,
    Step1Component,
    Step2Component,

    
    

  ],
  imports: [
    MatIconModule,
    DateInputsModule,
    LabelModule,
    DropDownsModule ,
    UploadsModule,
    InputsModule,
    ButtonsModule,
    CommonModule,
    TranslationModule,
    AuthRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    WizardsRoutingModule,
    InlineSVGModule,
    NgbTooltipModule,
    MatDialogModule,
  ],
})
export class AuthModule {}
