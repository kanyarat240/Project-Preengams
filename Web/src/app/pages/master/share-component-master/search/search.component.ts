import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
  })
  
export class SearchComponent {

  @Input() searchQuery: string;
  @Input() placeholder: string;
  @Input() backgroundColor: string;
  @Output() searchQueryChange = new EventEmitter<string>();

  private delayTimer: any;

  onInput() {
    // รีเซ็ต timer ก่อนหน้า (ถ้ามี)
    if (this.delayTimer) {
      clearTimeout(this.delayTimer);
    }

    // เริ่มต้น timer ใหม่
    this.delayTimer = setTimeout(() => {
      // ส่งค่า searchQuery ไปยังอีเวนต์ผ่าน EventEmitter หลังจาก delay 3 วินาที
      this.searchQueryChange.emit(this.searchQuery);
    }, 3000); 
  }

  onEnter() {
    // รีเซ็ต timer ก่อนหน้า (ถ้ามี)
    if (this.delayTimer) {
      clearTimeout(this.delayTimer);
    }

    // ส่งค่า searchQuery ไปยังอีเวนต์ผ่าน EventEmitter ทันที
    this.searchQueryChange.emit(this.searchQuery);
  }

}