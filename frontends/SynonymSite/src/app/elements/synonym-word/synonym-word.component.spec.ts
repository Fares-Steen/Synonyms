import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SynonymWordComponent } from './synonym-word.component';

describe('SynonymWordComponent', () => {
  let component: SynonymWordComponent;
  let fixture: ComponentFixture<SynonymWordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SynonymWordComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SynonymWordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
