import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteButtonComponent } from './delete-button/delete-button.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PagingComponent } from './paging/paging.component';
import { PaginationComponent } from '../pages/master/share-component-master/pagination/pagination.component';

@NgModule({
  declarations: [DeleteButtonComponent, PagingComponent , PaginationComponent],
  exports: [DeleteButtonComponent, PagingComponent, PaginationComponent],
  imports: [
    FormsModule,

    ReactiveFormsModule,
    CommonModule,
    NgxPaginationModule,
  ],
})
export class SharedModule {}
