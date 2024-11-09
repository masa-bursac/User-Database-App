import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObserveUsersComponent } from './observe-users.component';

describe('ObserveUsersComponent', () => {
  let component: ObserveUsersComponent;
  let fixture: ComponentFixture<ObserveUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ObserveUsersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ObserveUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
