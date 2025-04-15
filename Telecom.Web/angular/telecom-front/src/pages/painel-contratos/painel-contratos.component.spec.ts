import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelContratosComponent } from './painel-contratos.component';

describe('PainelContratosComponent', () => {
  let component: PainelContratosComponent;
  let fixture: ComponentFixture<PainelContratosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PainelContratosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PainelContratosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
