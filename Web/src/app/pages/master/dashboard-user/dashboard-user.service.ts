import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, Subscription, finalize } from 'rxjs';
import { LoaderService } from 'src/app/services/loader.service';
import { ActivatedRoute } from '@angular/router';
import { ElectricityDTO, WaterDTO } from './dashboard-user.model';


@Injectable({
  providedIn: 'root'
})
export class WaterService {
    private unsubscribe: Subscription[] = [];
    currentWaterSubject : BehaviorSubject<WaterDTO | undefined>;
    currentWater$ : Observable<WaterDTO | undefined>;

    get currentTeamValue(): WaterDTO| undefined {
        return this.currentWaterSubject.value;
    }

    set currentWaterValue(water: WaterDTO | undefined) {
        this.currentWaterSubject.next(water);
    }

    constructor(private httpClient: HttpClient, private loaderService: LoaderService , private route: ActivatedRoute) {
        this.currentWaterSubject = new BehaviorSubject<WaterDTO | undefined>(undefined);
        this.currentWater$ = this.currentWaterSubject.asObservable();
        
    }

   getWater(): Observable<WaterDTO[] | []> {
    this.loaderService.start()
    return this.httpClient.get<WaterDTO[]>('/water/getWater').pipe(
      finalize(() => this.loaderService.stop())
    );
  }

  getElectricity(): Observable<ElectricityDTO[] | []> {
    this.loaderService.start()
    return this.httpClient.get<ElectricityDTO[]>('/water/getElectricity').pipe(
      finalize(() => this.loaderService.stop())
    );
  }

}