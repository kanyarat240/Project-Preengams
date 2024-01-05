import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { components } from 'src/app/_metronic/kt';
import { Routing } from '../routing';
// import { PaginationsModule } from 'src/app/shared/delete-button/paginations/paginations.module';
import { PaginationControlsComponent } from 'ngx-pagination';


const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    // children: [
      
    // ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)
    // ,PaginationsModule
  ],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}
