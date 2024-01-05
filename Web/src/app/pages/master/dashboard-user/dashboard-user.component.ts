import { Component, OnInit, SimpleChanges } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { UserProfileModel } from 'src/app/models/user-profile.model';
import { UserService } from 'src/app/services/user.service';
import { WaterDTO } from './dashboard-user.model';
import { WaterService } from './dashboard-user.service';

@Component({
  selector: 'app-dashboard-user',
  templateUrl: './dashboard-user.component.html',
  styleUrls: ['./dashboard-user.component.scss'],
})
export class DashboardUerComponent implements OnInit {

  water$: Observable<WaterDTO | undefined>;
  water: WaterDTO | undefined;
  cloneWater: WaterDTO;
  chartData: number[] = [];
  chartEle: number[] = [];

  constructor(private waterService: WaterService) {}

  ngOnInit() {
    // ดำเนินการดึงข้อมูลจาก WaterService
    this.waterService.getWater().subscribe(data => {
      // นำข้อมูลมาใช้ใน Kendo Chart
      this.chartData = data.map(item => item.unitUsed); // แบ่งแยกรายการที่ต้องการ
    });

    this.waterService.getElectricity().subscribe(ele => {
      // นำข้อมูลมาใช้ใน Kendo Chart
      this.chartEle = ele.map(item => item.unitUsed); // แบ่งแยกรายการที่ต้องการ
    });
    
  }

  
}