import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StartStreamComponent } from './start-stream.component';

describe('StartStreamComponent', () => {
  let component: StartStreamComponent;
  let fixture: ComponentFixture<StartStreamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StartStreamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StartStreamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
