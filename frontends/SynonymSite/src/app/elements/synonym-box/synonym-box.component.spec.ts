import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SynonymBoxComponent } from './synonym-box.component';

describe('SynonymBoxComponent', () => {
  let component: SynonymBoxComponent;
  let fixture: ComponentFixture<SynonymBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SynonymBoxComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SynonymBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
