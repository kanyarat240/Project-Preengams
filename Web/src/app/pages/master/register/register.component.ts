import { ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { LoaderService } from "src/app/services/loader.service";
import { RoomDTO } from "src/app/models/room.model";
import { BehaviorSubject, catchError, map, of } from "rxjs";
import { AlertService } from "src/app/services/alert.service";
import { HttpErrorResponse } from "@angular/common/http";
import { RegisterService } from "src/app/services/register.service";
import { RoomService } from "src/app/services/room.service";

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
  })
  export class RegisterComponent implements OnInit {

    isLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    isLoading: boolean;
    saveDisable$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    saveDisable: boolean;
    isInvalid: boolean;

    constructor(
      private loaderService : LoaderService,
      private cdr: ChangeDetectorRef,
      private alertService: AlertService,
      private roomService: RoomService,
      private  registerService : RegisterService,
      ){}
    ngOnInit(): void {
      this.loaderService.startLoader()
      this.loaderService.stopLoader()
    }

    dropdownDTO: any[] = [];
    Room:RoomDTO;

    getDropDown() {
      this.roomService.getRoom().subscribe({
        next: (res: RoomDTO[]) => {
          this.dropdownDTO = res;
          console.log("DropDown!!!", this.dropdownDTO);
          //this.selectedLeaderName = this.selectedLeaderName;
        },
        error: () => {},
      });
    }

    newRegis = new Item();

    register(){ 
      debugger;
      this.isLoading$.next(true);
      const addRegister = {
        Username:this.newRegis.username,
        Password:this.newRegis.password,
        ConfirmPassword:this.newRegis.confirmPassword,
        FirstName:this.newRegis.firstName,
        LastName:this.newRegis.lastName,
        Phone:this.newRegis.phone,
        IdCard:this.newRegis.idCard,
        Address:this.newRegis.address,
        RoomId:this.newRegis.roomId
    
      }
        setTimeout(() => {
          this.isLoading$.next(false);
          this.cdr.detectChanges();
          this.registerService.registration(addRegister).pipe(
            map(() => {
              this.alertService.onSuccessNew();
              
              return true;
            }),
            catchError((error: HttpErrorResponse) => {
              console.error('error', error);
              this.alertService.onErrorTEam();
              return of(false);
            })
          ).subscribe(
            (response) => {
              console.log(response);
            },
            (error) => {
              console.error(error,);
            }
          );
          this.cdr.detectChanges();
        }, 1500);
      }
  
  }

  export class Item {

    id : string;
    username: string;
    password : string;
    confirmPassword: string;
    firstName : string;
    lastName : string;
    phone: string;
    idCard : string;
    address : string;
    roomId : string;
  
    constructor() {
      this.username = '',
      this.password = '',
      this.confirmPassword = '',
      this.firstName = '',
      this.lastName = '',
      this.phone = '',
      this.idCard = '',
      this.address = '',
      this.roomId = ''
    }
  }