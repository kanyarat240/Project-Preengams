import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MasterComponent } from './master.component';
import { ProfileComponent } from './profile/profile.component';
import { CompanyComponent } from './company/company.component';
import { BranchComponent } from './branch/branch.component';
import { SearchComponent } from './share-component-master/search/search.component';
import { PaginationComponent } from './share-component-master/pagination/pagination.component';
import { RegisterComponent } from './register/register.component';
import { BillUserComponent } from './bill-user/bill-user.component';
import { BillHistoryComponent } from './bill-history/bill-history.component';
import { BillAdminComponent } from './bill-admin/bill-admin.component';
import { MonthlyBillDetailsComponent } from './bill-admin/monthly-bill-details/monthly-bill-details.component';
import { DashboardUerComponent } from './dashboard-user/dashboard-user.component';
import { DashboardAdminComponent } from './dashboard-admin/dashboard-admin.component';
import { PasswordSettingComponent } from './password-setting/password-setting.component';
import { SettingComponent } from './profile/setting/setting.component';



const routes: Routes = [
  {
    path: '',
    component: MasterComponent,
    children: [
      {
        path: 'company',
        component: CompanyComponent,
        // children: [

        // ]
      },

      {
        path: 'branch/:id',
        component: BranchComponent,
        // children: [
          

        //   { path: '', redirectTo: 'overview', pathMatch: 'full' },

        // ]
      },
      {
        path: 'profile',
        component: ProfileComponent,
        // children: [
          
        // ],
      },
      {
        path: 'setting',
        component: SettingComponent,
      },
      {
        path: 'password-setting',
        component: PasswordSettingComponent,
        // children: [
          
        // ],
      },
      {
        path: 'register',
        component: RegisterComponent,
        // children: [
          
        // ],
      },

      {
        path: 'bill-user',
        component: BillUserComponent,
        // children: [
          
        // ],
      },
      {
        path: 'bill-admin',
        component: BillAdminComponent,
        // children: [
         
        // ],
      },
      {
        path: 'monthly-bill-details',
        component: MonthlyBillDetailsComponent,
        },
      {
        path: 'bill-history',
        component: BillHistoryComponent,
        // children: [
          
        // ],
      },
      {
        path: 'dashboard-user',
        component: DashboardUerComponent,
        // children: [
          
        // ],
      },
      {
        path: 'dashboard-admin',
        component: DashboardAdminComponent,
        // children: [
          
        // ],
      },
      
    ],
  },

  {
    path: 'search',
    component: SearchComponent,
    children: [
    ]
  },
  {
    path: 'pagination',
    component: PaginationComponent,
    children: [
    ]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MasterRoutingModule { }