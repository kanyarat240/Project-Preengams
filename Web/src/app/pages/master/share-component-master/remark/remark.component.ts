import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Subscription } from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { ProvinceService } from 'src/app/services/province.service';

@Component({
    selector: 'app-remark',
    templateUrl: './remark.component.html',
    styleUrls: ['./remark.component.scss'],

})

export class RemarkComponent {

  isLoading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isLoading: boolean;
  private unsubscribe: Subscription[] = [];
  saveDisable$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  saveDisable: boolean;
    
    constructor(private cdr: ChangeDetectorRef, private alertService: AlertService, private provinceService: ProvinceService, private route: ActivatedRoute) { }

    ngOnInit(): void {

      const loadingSubscr = this.isLoading$
      .asObservable()
      .subscribe((res) => (this.isLoading = res));
    this.unsubscribe.push(loadingSubscr);
  
    const disableSubscr = this.saveDisable$
      .asObservable()
      .subscribe((res) => (this.saveDisable = res));
    this.unsubscribe.push(disableSubscr);
    }

    @Input() remarkFromParent: string;
    @Output() CommentRemark: EventEmitter<string> = new EventEmitter<string>();
    remark: string = '';

    isRemarkPopupOpen = false;

    toggleRejectPopup() {
        this.isRemarkPopupOpen = !this.isRemarkPopupOpen;
    }

    @Output() closeDialog = new EventEmitter<string>();

    close() {
      // ทำสิ่งที่คุณต้องการก่อนปิด dialog แล้วส่ง event ออกไป
      this.closeDialog.emit('close');
    }

    
    SaveClick(remark: string) {
      if (remark === null || remark.trim() === '') {
        remark = ' ';
      }
      this.CommentRemark.emit(remark);
      this.isLoading$.next(true);
    
      // ใช้ setTimeout เพื่อหยุดการทำงานของ isLoading$.next(true); หลังจาก 1 วินาที
      setTimeout(() => {
        this.isLoading$.next(false); // หยุด isLoading หลังจาก 1 วินาที

      }, 1000); // 1000 มิลลิวินาทีเท่ากับ 1 วินาที
    }
    
}


