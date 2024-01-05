import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { TranslationService } from '../modules/i18n/translation.service';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root',
})
export class AlertService {
  constructor(
    private translate: TranslationService,
    private router: Router,
    private userService: UserService
  ) { }

  onSuccess(message: string, navigateTo?: string) {
    Swal.fire({
      title: 'Success!',
      text: 'Save completed',
      icon: 'success',
      confirmButtonText: 'Okay',
    }).then(() => {
      this.router.navigate([navigateTo]); //path
    });
  }

  onError(messageCode: string) {
    Swal.fire({
      title: 'Error!',
      text: this.translate.get(messageCode),
      icon: 'error',
      confirmButtonText: this.translate.get('Okay'),
    });
  }

  onChangeReject() {
    return new Promise((resolve) => {
      setTimeout(() => {
        Swal.fire({
          title: 'Are you Want to Reject?',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Reject',
          confirmButtonColor: '#F41018',
          cancelButtonText: 'Cancel',
          reverseButtons: true
        }).then((result) => {
          if (result.isDismissed && result.dismiss === Swal.DismissReason.cancel) {
            resolve(false); // กรณีผู้ใช้กด "Cancel" ให้คืนค่า true
          } else {
            resolve(true); // กรณีผู้ใช้กด "Okay" ให้คืนค่าตามผลลัพธ์จริง
          }
        });
      },
      );
    });
  }

  onSuccessNew() {
    const delayInMilliseconds = 1000;
    setTimeout(() => {
      Swal.fire({
        title: 'Success!',
        text: 'Save completed',
        icon: 'success',
        confirmButtonText: this.translate.get('Okay'),
        // timer: 5000, ให้pop-up ปิดหลังครบ 5 วินาที
      });
    }, delayInMilliseconds);
  }

  onErrorNew() {
    const delayInMilliseconds = 1000;
    setTimeout(() => {
      Swal.fire({
        title: 'Error!',
        text: this.translate.get('Save fail'),
        icon: 'error',
        confirmButtonText: this.translate.get('Okay'),
        // timer: 5000, ให้pop-up ปิดหลังครบ 5 วินาที
      });
    }, delayInMilliseconds);
  }

  onErrorTEam() {
    const delayInMilliseconds = 1000;
    setTimeout(() => {
      Swal.fire({
        title: 'Error!',
        text: this.translate.get('Team name already exists.'),
        icon: 'error',
        confirmButtonText: this.translate.get('Okay'),
        // timer: 5000, ให้pop-up ปิดหลังครบ 5 วินาที
      });
    }, delayInMilliseconds);
  }



  onSuccessPath(navigateTo?: string) {
    Swal.fire({
      title: 'Success!',
      text: 'Save completed',
      icon: 'success',
      confirmButtonText: 'Okay',
    }).then(() => {
      this.router.navigate([navigateTo]); //path
    });
  }



  onChangeStatus() {
    const delayInMilliseconds = 1000;
    return new Promise((resolve) => {
      setTimeout(() => {
        Swal.fire({
          title: 'Are you want to continue?',
          icon: 'question',
          confirmButtonText: 'Okay',
          showCancelButton: true,
          cancelButtonText: 'Cancel',
          reverseButtons: true,
        }).then((result) => {
          if (result.isDismissed && result.dismiss === Swal.DismissReason.cancel) {
            resolve(false); // กรณีผู้ใช้กด "Cancel" ให้คืนค่า true
          } else {
            resolve(true); // กรณีผู้ใช้กด "Okay" ให้คืนค่าตามผลลัพธ์จริง
          }
        });
      }, delayInMilliseconds);
    });
  }



  withOutTranslate = {
    onSuccess() {
      Swal.fire({
        title: 'Success!',
        text: 'Save completed',
        icon: 'success',
        confirmButtonText: 'Okay',
      });
    },

    onError(message: string) {
      Swal.fire({
        title: 'Error!',
        text: message,
        icon: 'error',
        confirmButtonText: 'Okay',
      });
    },

    onSuccessNew() {
      Swal.fire({
        title: 'Success!',
        text: 'Save completed',
        icon: 'success',
        confirmButtonText: 'Okay',
      });
    },

    onErrorNew() {
      Swal.fire({
        title: 'Error!',
        text: 'Save fail',
        icon: 'question',
        confirmButtonText: 'Okay',
      });
    },

    // onWarning(message: string) {},
  };
}
