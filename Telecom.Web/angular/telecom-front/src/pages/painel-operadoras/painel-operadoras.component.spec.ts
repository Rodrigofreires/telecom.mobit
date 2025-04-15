import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelOperadorasComponent } from './painel-operadoras.component';

describe('PainelOperadorasComponent', () => {
  let component: PainelOperadorasComponent;
  let fixture: ComponentFixture<PainelOperadorasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PainelOperadorasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PainelOperadorasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
