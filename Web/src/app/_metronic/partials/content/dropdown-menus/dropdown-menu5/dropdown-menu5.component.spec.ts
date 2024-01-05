import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DropdownMenu5Component } from './dropdown-menu5.component';

describe('DropdownMenu5Component', () => {
  let component: DropdownMenu5Component;
  let fixture: ComponentFixture<DropdownMenu5Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DropdownMenu5Component ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DropdownMenu5Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
