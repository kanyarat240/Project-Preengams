import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { CompanyModel } from 'src/app/models/company.model';
import { LoaderService } from 'src/app/services/loader.service';


type Tabs = 'Facebook';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent {
  company: Observable<CompanyModel | undefined>;

  constructor(private loaderService : LoaderService){}
    ngOnInit(): void {
      this.loaderService.startLoader()
      this.loaderService.stopLoader()
    }

}
