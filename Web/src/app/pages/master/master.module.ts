import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { InlineSVGModule } from 'ng-inline-svg-2';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MasterComponent } from "./master.component";
// import { DropdownMenusModule, WidgetsModule } from '../../_metronic/partials';
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { DropDownsModule } from "@progress/kendo-angular-dropdowns";
import { MatIconModule } from '@angular/material/icon';
import { TranslationModule } from "src/app/modules/i18n";
import { CardsModule, DropdownMenusModule, WidgetsModule } from "src/app/_metronic/partials";
import { ProfileComponent } from "./profile/profile.component";
import { LabelModule } from "@progress/kendo-angular-label";
import { DateInputsModule, MultiViewCalendarModule } from "@progress/kendo-angular-dateinputs";
import { InputsModule } from "@progress/kendo-angular-inputs";
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { UploadsModule } from "@progress/kendo-angular-upload";
import { routes } from "src/app/app-routing.module";
import { MasterRoutingModule } from "./master-routing.module";
import { FormsModule } from '@angular/forms';
import { CompanyComponent } from "./company/company.component";
import { DialogModule } from "@progress/kendo-angular-dialog";
import { BranchComponent } from "./branch/branch.component";
import { SchedulerModule } from "@progress/kendo-angular-scheduler";
import { PopupModule } from "@progress/kendo-angular-popup";
import { ChartsModule } from "@progress/kendo-angular-charts";
import { MatCardModule } from "@angular/material/card";
import { TruncatePipe } from "./truncate.pipe";
import { HttpClientModule } from "@angular/common/http";
import { NgxPaginationModule } from 'node_modules/ngx-pagination';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
// import {NgxPaginationModule} from 'ngx-pagination';
import { MatSelectModule } from '@angular/material/select';
import { PaginationComponent } from './share-component-master/pagination/pagination.component';
import { SearchComponent } from "./share-component-master/search/search.component";
import { DomEventsService, GridModule, PagerModule } from "@progress/kendo-angular-grid";
import { RemarkComponent } from "./share-component-master/remark/remark.component";
import { SharedModule } from "src/app/shared/shared.module";
import { PagingComponent } from "src/app/shared/paging/paging.component";
import { RegisterComponent } from "./register/register.component";
import { BillUserComponent } from "./bill-user/bill-user.component";
import { BillHistoryComponent } from "./bill-history/bill-history.component";
import { BillAdminComponent } from "./bill-admin/bill-admin.component";
import { MonthlyBillDetailsComponent } from "./bill-admin/monthly-bill-details/monthly-bill-details.component";
import { DashboardUerComponent } from "./dashboard-user/dashboard-user.component";
import { DashboardAdminComponent } from "./dashboard-admin/dashboard-admin.component";
import { PasswordSettingComponent } from "./password-setting/password-setting.component";
import { SettingComponent } from "./profile/setting/setting.component";


@NgModule({

    declarations: [
        CompanyComponent,
        MasterComponent,
        ProfileComponent,
        BranchComponent,
        TruncatePipe,
        SearchComponent,
        RemarkComponent,
        RegisterComponent,
        BillUserComponent,
        BillHistoryComponent,
        BillAdminComponent,
        MonthlyBillDetailsComponent,
        DashboardUerComponent,
        DashboardAdminComponent,
        PasswordSettingComponent,
        SettingComponent,
    ],
    imports: [
        ChartsModule,
        MatCardModule,
        RouterModule,
        DialogModule,
        CardsModule,
        FormsModule,
        InlineSVGModule,
        ReactiveFormsModule,
        TranslationModule,
        DropdownMenusModule,
        WidgetsModule,
        NgbTooltipModule,
        DropDownsModule,
        LabelModule,
        DateInputsModule,
        InputsModule,
        ButtonsModule,
        MatIconModule,
        UploadsModule,
        MasterRoutingModule,
        CommonModule,
        SchedulerModule,
        PopupModule,
        HttpClientModule, 
        CommonModule,
        NgxPaginationModule,
        Ng2SearchPipeModule,
        MatSelectModule,
        PagerModule,
        GridModule,
        MultiViewCalendarModule,
        SharedModule,
    ]
})
export class MasterModule {}
