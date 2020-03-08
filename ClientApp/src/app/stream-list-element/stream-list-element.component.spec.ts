import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StreamListElementComponent } from './stream-list-element.component';

describe('StreamListElementComponent', () => {
  let component: StreamListElementComponent;
  let fixture: ComponentFixture<StreamListElementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StreamListElementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StreamListElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
