import { Component, OnInit} from '@angular/core';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})

export class DashboardComponent implements OnInit {
 
  constructor(private loaderService : LoaderService){}
  ngOnInit(): void {
    this.loaderService.startLoader()
    this.loaderService.stopLoader()
  }

}
