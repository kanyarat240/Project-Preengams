import { Component, OnInit } from '@angular/core';
import { UserProfileModel } from 'src/app/models/user-profile.model';

@Component({
  selector: 'app-monthly-bill-details',
  templateUrl: './monthly-bill-details.component.html',
  styleUrls: ['./monthly-bill-details.component.scss'],
})
export class MonthlyBillDetailsComponent implements OnInit {


  constructor() {}


  ngOnInit(): void {
    
  }
  opened: boolean = false;
  public open(): void {
    this.opened = true;
  }


  newbill = false;
  togglenewbill() {
    this.newbill = !this.newbill;
  }

  editbill = false;
  toggleeditbill() {
    this.editbill = !this.editbill;
  }

  public close(action: string) {
    this.newbill = false;
    this.editbill = false;
    if (action === 'closes'){
    }
  }

}