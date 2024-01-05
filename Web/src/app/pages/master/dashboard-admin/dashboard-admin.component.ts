import { Component, OnInit } from '@angular/core';
import { SeriesLabelsAlignment, SeriesLabelsContentArgs } from '@progress/kendo-angular-charts';
import { Observable, Subscription } from 'rxjs';
import { UserProfileModel } from 'src/app/models/user-profile.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-dashboard-admin',
  templateUrl: './dashboard-admin.component.html',
  styleUrls: ['./dashboard-admin.component.scss'],
})
export class DashboardAdminComponent implements OnInit {

  user$: Observable<UserProfileModel | undefined>;

  constructor() {}


  ngOnInit(): void {
    
  }

  public labelAlign: SeriesLabelsAlignment = "column";
  public data = [
    {
      kind: "ค้างชำระ",
      share: 5,
    },
    {
      kind: "ชำระแล้ว",
      share: 16,
    },
    
  ];

  public labelContent(e: SeriesLabelsContentArgs): string {
    return e.category;
  }

}