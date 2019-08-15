import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CoffeeSelectComponent } from './coffee-select.component';

describe('CoffeeSelectComponent', () => {
  let component: CoffeeSelectComponent;
  let fixture: ComponentFixture<CoffeeSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CoffeeSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CoffeeSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
