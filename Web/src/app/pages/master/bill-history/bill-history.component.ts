import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-bill-history',
  templateUrl: './bill-history.component.html',
  styleUrls: ['./bill-history.component.scss'],
})
export class BillHistoryComponent implements OnInit {


  constructor() {}


  ngOnInit(): void {
    
  }

  opened: boolean = false;
  public open(): void {
    this.opened = true;
  }

  public close(action: string) {
    this.opened = false;
    if (action === 'closes'){
    }
  }

}