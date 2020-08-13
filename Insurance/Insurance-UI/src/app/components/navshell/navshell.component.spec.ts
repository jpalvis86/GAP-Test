import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NavshellComponent } from './navshell.component';

describe('NavshellComponent', () => {
  let component: NavshellComponent;
  let fixture: ComponentFixture<NavshellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NavshellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavshellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
