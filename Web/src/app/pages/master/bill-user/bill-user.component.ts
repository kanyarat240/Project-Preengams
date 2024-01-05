import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { UserProfileModel } from 'src/app/models/user-profile.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-bill-user',
  templateUrl: './bill-user.component.html',
  styleUrls: ['./bill-user.component.scss'],
})
export class BillUserComponent implements OnInit {

  user$: Observable<UserProfileModel | undefined>;

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