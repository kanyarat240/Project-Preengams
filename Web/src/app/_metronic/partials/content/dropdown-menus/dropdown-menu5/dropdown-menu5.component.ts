import { Component, HostBinding, OnInit } from '@angular/core';

@Component({
  selector: 'app-dropdown-menu5',
  templateUrl: './dropdown-menu5.component.html',
  styleUrls: ['./dropdown-menu5.component.scss']
})
export class DropdownMenu5Component implements OnInit {
  
  @HostBinding('class') class =
    'menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-800 menu-state-bg-light-primary fw-bold w-100px py-3';
  @HostBinding('attr.data-kt-menu') dataKtMenu = 'true';

  constructor() {}

  ngOnInit(): void {}
}
