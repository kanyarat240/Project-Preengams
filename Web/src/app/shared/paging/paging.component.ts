import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-paging',
  templateUrl: './paging.component.html',
  styleUrls: ['./paging.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PagingComponent),
      multi: true,
    }
  ]
})
export class PagingComponent implements OnChanges {  @Input() currentPage = 1;
  @Input() recordCount: number = 0;
  @Input() itemsPerPage: number = 5;
  @Input() customMessage: string = '';
  @Output() pageChange = new EventEmitter();

  ngOnChanges(changes: SimpleChanges) {
    if (changes.recordCount || changes.calculateEndIndex) {
      this.currentPage = 1;
      // Emit the pageChange event to notify the parent component to reload data.
      this.pageChange.emit(this.currentPage);
    }
  }

  ngOnInit() { }

  pageChanged(event: any): void {
    this.currentPage = event;
    console.log('pageChanged ', event);
    this.pageChange.emit(this.currentPage);
  }

  calculateStartIndex(): number {
    return ((this.currentPage ?? 0) - 1) * this.itemsPerPage + 1;
  }

  calculateEndIndex(): number {
    return Math.min((this.currentPage ?? 0) * this.itemsPerPage, (this.recordCount ?? 0));
  }
}
