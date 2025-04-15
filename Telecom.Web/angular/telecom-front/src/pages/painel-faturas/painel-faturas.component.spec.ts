import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelFaturasComponent } from './painel-faturas.component';

describe('PainelFaturasComponent', () => {
  let component: PainelFaturasComponent;
  let fixture: ComponentFixture<PainelFaturasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PainelFaturasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PainelFaturasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
