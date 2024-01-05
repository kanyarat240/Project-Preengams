import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, Subscription, finalize } from 'rxjs';
import { LoaderService } from 'src/app/services/loader.service';
import { RoomDTO } from '../models/room.model';
import { ActivatedRoute } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class RoomService {
    private unsubscribe: Subscription[] = [];
    currentRoomSubject : BehaviorSubject<RoomDTO | undefined>;
    currentTeam$ : Observable<RoomDTO | undefined>;

    get currentTeamValue(): RoomDTO| undefined {
        return this.currentRoomSubject.value;
    }

    set currentTeamValue(team: RoomDTO | undefined) {
        this.currentRoomSubject.next(team);
    }

    constructor(private httpClient: HttpClient, private loaderService: LoaderService , private route: ActivatedRoute) {
        this.currentRoomSubject = new BehaviorSubject<RoomDTO | undefined>(undefined);
        this.currentTeam$ = this.currentRoomSubject.asObservable();
        
    }

   getRoom(): Observable<RoomDTO[]> {
    this.loaderService.start()
    return this.httpClient.get<RoomDTO[]>('/Room/getRoom').pipe(
      finalize(() => this.loaderService.stop())
    );
  }

}